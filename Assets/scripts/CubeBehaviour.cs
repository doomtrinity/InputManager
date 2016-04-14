using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DT.InputManagement;
using DT.GUI;

namespace DT.Cube {

	// This is a test class which you can consider as a 'player' class. Here we basically control the behaviour
	// of a cube gameobject, which is actually its movement stuff only. The thing we do is to read the input
	// to check a possible keydown, and perform a specific action depending on the key being pressed.
	// Check for escape button to toggle settings gui, and 4 keys for movement ( up - down - left - right ).
	// Note how we use our custom 'InputManager' class: it is very similar to the Unity Input class.
	public class CubeBehaviour : MonoBehaviour {
		
		// =================================================================================================
		// Instance variables

		private SettingsGUI settingsGUI;

		// =================================================================================================
		// Awake

		private void Awake() {
			settingsGUI = FindObjectOfType<SettingsGUI> ();
		}

		// =================================================================================================
		// Update

		public void Update() {
						
			if(!InputManager.IsEscaping) {

				if (InputManager.GetKeyDown (ActionCode.Escape)) {
					EnableGUI (true); // show settings gui
				}

				Move (); // Move cube.

			} else {
				if (InputManager.GetKeyDown (ActionCode.Escape)) {
					EnableGUI (false); // hide settings gui
				}
			}
		}

		// =================================================================================================
		// Move

		private void Move() {
			
			if(InputManager.GetKey(ActionCode.MoveDown)){
				transform.Translate (Vector3.down * Time.deltaTime * 3);
			}
			if(InputManager.GetKey(ActionCode.MoveUp)){
				transform.Translate (Vector3.up * Time.deltaTime * 3);
			}
			if(InputManager.GetKey(ActionCode.MoveLeft)){
				transform.Translate (Vector3.left * Time.deltaTime * 3);
			}
			if(InputManager.GetKey(ActionCode.MoveRight)){
				transform.Translate (Vector3.right * Time.deltaTime * 3);
			}
		}

		// =================================================================================================
		// EnableGUI

		private bool EnableGUI(bool enable) {
			if (settingsGUI != null) {
				settingsGUI.gameObject.SetActive (enable);
				return true;
			}
			return false;
		}

		// =================================================================================================
	}
}
