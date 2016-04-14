using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace DT.InputManagement {

	// Attach this script to a button gameobject in the gui. Here you can define the action type that this button
	// allow to set, by choosing the action code through its inspector.
	// When you click on this button, then the settings gui will wait for a key down to bind the action code.
	// Take a look the the 'InputManager' script to see the allowed keycode, in 'LoadAllowedKeys' method.
	// If you try to bind a key code which is not in the list, then nothing will happen. Escape is considered
	// as a special key even if it is present in the list of allowed keys - it is used to show/hide the seggings gui.
	[RequireComponent(typeof(Button))]
	public class Bindable : MonoBehaviour {

		// =================================================================================================
		// Instance variables

		[SerializeField] private ActionCode actionType;

		// =================================================================================================
		// Instance properties

		public ActionCode ActionType {
			get {
				return actionType;
			}
		}

		// =================================================================================================
	}
}

