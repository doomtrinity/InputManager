using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using DT.InputManagement;

namespace DT.GUI {

	// A simple script to manage the behaviour of the settings gui, which is attached to the gui gameobject, 
	// in the hierarchy. This is what it does: 
	// - check if a bindable button is being clicked, then wait for key down to bind the action code to that key.
	// - find all the 'Bindable' gameobjects, which are buttons, and we put them in a list, 
	// that is used to update the text of these buttons - the ToString conversion of the 'KeyCode'.
	// Note that you must define the 'OnClick' event for each bindable button, through the inspector, 
	// so it will fire the 'ButtonClick' method when we click on the button.
	// This could also be done through Unity 'OnGUI', which should avoid to add this method call manually,
	// but I wanted to implement this stuff without these workhorse functions for frame updates.
	public class SettingsGUI : MonoBehaviour {

		// =================================================================================================
		// Instance variables

		private bool isBindingKey = false;
		private Bindable guiButtonReference = null;
		private List<Bindable> buttons = null;

		// =================================================================================================
		// Awake

		private void Awake() {
			InputManager.Initialize ();

			buttons = new List<Bindable>(FindObjectsOfType<Bindable> ());
			UpdateGUIButtonText();
			this.gameObject.SetActive (false);
		}

		// =================================================================================================
		// ButtonClick

		// called on button 'OnClick()' event. Call this method on 'bindable' buttons.
		public void ButtonClick() {
			if(!isBindingKey && InputManager.IsEscaping) {

				isBindingKey = true;
				guiButtonReference = EventSystem.current.currentSelectedGameObject.GetComponent<Bindable> ();
				if (guiButtonReference != null) {
					UpdateGUIButtonText (guiButtonReference, "press a key...");
					StartCoroutine (CheckBinding());
				}

				// Debug.Log ("SettingsGUI.ButtonClick");
			}
		}

		// =================================================================================================
		// CheckBinding

		private IEnumerator CheckBinding() {
			yield return null; // wait next frame to update anykeydown state

			while (isBindingKey && !Input.anyKeyDown) {
				// Debug.Log ("SettingsGUI.CheckBinding");
				yield return null;						
			}
			SetKey ();
			yield return new WaitForSeconds (0.2f);
			isBindingKey = false;


		}

		// =================================================================================================
		// SetKey

		public void SetKey() {
			if(guiButtonReference != null) {
				ActionCode actionCode = guiButtonReference.ActionType;
				if ( InputManager.SetKey (actionCode) ) {
					UpdateGUIButtonText();
					// Debug.Log ("SettingsGUI.SetKey");
				}
			}
		}

		// =================================================================================================
		// UpdateGUIButtonText

		private void UpdateGUIButtonText(Bindable button, string text) {
			if (button != null) {
				Text btnText = button.GetComponentInChildren<Text> ();
				if (btnText != null) {
					btnText.text = text;
				}
			}
		}

		// =================================================================================================
		// UpdateGUIButtonText

		private void UpdateGUIButtonText() {
			if (buttons != null) {
				foreach (Bindable button in buttons) {
					Text btnText = button.GetComponentInChildren<Text> ();
					if (btnText != null) {
						btnText.text = InputManager.GetActionKey (button.ActionType).ToString();
					}
				}
			}
		}

		// =================================================================================================
		// LoadDefaultKeys

		public  void LoadDefaultKeys() {
			InputManager.LoadDefaultBinds ();
			UpdateGUIButtonText ();
		}

		// =================================================================================================
		// UnbindAllKeys

		public  void UnbindAllKeys() {
			InputManager.UnbindAll ();
			UpdateGUIButtonText ();
		}

		// =================================================================================================
		// OnEnable

		private void OnEnable() {
			UpdateGUIButtonText ();
		}

		// =================================================================================================
		// OnDisable

		private void OnDisable() {
			StopAllCoroutines ();
			isBindingKey = false;
		}

		// =================================================================================================
	}
}
