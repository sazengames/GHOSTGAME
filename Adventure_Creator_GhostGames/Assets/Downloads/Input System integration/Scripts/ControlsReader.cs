using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AC
{

	public class ControlsReader : UnityEngine.InputSystem.PlayerInput
	{

		#region Variables

		[SerializeField] private string globalStringVariable = "InputRebindings";
		[SerializeField] private InputActionReference[] inputActionReferences = new InputActionReference [0];
		private readonly Dictionary<string, InputAction> inputActionsDictionary = new Dictionary<string, InputAction> ();

		#endregion


		#region UnityStandards

		private void Start ()
		{
			CreateInputActionsDictionary ();

			// Mouse delegates
			KickStarter.playerInput.InputMousePositionDelegate = Custom_MousePosition;
			KickStarter.playerInput.InputGetMouseButtonDelegate = Custom_GetMouseButton;
			KickStarter.playerInput.InputGetMouseButtonDownDelegate = Custom_GetMouseButtonDown;

			// Keyboard / controller delegates
			KickStarter.playerInput.InputGetAxisDelegate = Custom_GetAxis;
			KickStarter.playerInput.InputGetButtonDelegate = Custom_GetButton;
			KickStarter.playerInput.InputGetButtonDownDelegate = Custom_GetButtonDown;
			KickStarter.playerInput.InputGetButtonUpDelegate = Custom_GetButtonUp;

			// Touch delegates
			KickStarter.playerInput.InputTouchCountDelegate = Custom_TouchCount;
			KickStarter.playerInput.InputTouchPositionDelegate = Custom_TouchPosition;
			KickStarter.playerInput.InputTouchDeltaPositionDelegate = Custom_TouchDeltaPosition;
			KickStarter.playerInput.InputGetTouchPhaseDelegate = Custom_TouchPhase;
			#if !UNITY_EDITOR
			if (KickStarter.settingsManager.inputMethod == InputMethod.TouchScreen)
			{
				UnityEngine.InputSystem.EnhancedTouch.EnhancedTouchSupport.Enable ();
			}
			#endif
		}


		private void OnEnable ()
		{
			actions.Enable ();
			EventManager.OnInitialiseScene += OnInitialiseScene;
			EventManager.OnSwitchProfile += OnSwitchProfile;
		}

		private void OnDisable ()
		{
			actions.Disable ();
			EventManager.OnSwitchProfile -= OnSwitchProfile;
			EventManager.OnInitialiseScene -= OnInitialiseScene;
		}

		#endregion


		#region MouseInput

		private Vector2 Custom_MousePosition (bool cursorIsLocked)
		{
			if (cursorIsLocked)
				return new Vector2 (Screen.width / 2f, Screen.height / 2f);
			
			return Mouse.current.position.ReadValue ();
		}


		private bool Custom_GetMouseButton (int button)
		{
			switch (button)
			{
				case 0:
					return Mouse.current.leftButton.isPressed;

				case 1:
					return Mouse.current.rightButton.isPressed;

				default:
					return false;
			}
		}


		private bool Custom_GetMouseButtonDown (int button)
		{
			switch (button)
			{
				case 0:
					return Mouse.current.leftButton.wasPressedThisFrame;

				case 1:
					return Mouse.current.rightButton.wasPressedThisFrame;

				default:
					return false;
			}
		}

		#endregion


		#region KeyboardControllerInput

		private float Custom_GetAxis (string axisName)
		{
			InputAction inputAction = GetInputAction (axisName);
			if (inputAction == null)
			{
				return 0f;
			}

			return inputAction.ReadValue<float> ();
		}


		private bool Custom_GetButton (string axisName)
		{
			InputAction inputAction = GetInputAction (axisName);
			if (inputAction == null)
			{
				return false;
			}

			return inputAction.IsPressed ();
		}


		private bool Custom_GetButtonDown (string axisName)
		{
			InputAction inputAction = GetInputAction (axisName);
			if (inputAction == null)
			{
				return false;
			}

			return inputAction.WasPerformedThisFrame ();
		}


		private bool Custom_GetButtonUp (string axisName)
		{
			InputAction inputAction = GetInputAction (axisName);
			if (inputAction == null)
			{
				return false;
			}

			return inputAction.WasReleasedThisFrame ();
		}

		#endregion


		#region PublicFunctions

		public void ClearRebindings ()
		{
			GVar saveVariable = GlobalVariables.GetVariable (globalStringVariable);
			if (saveVariable != null && saveVariable.type == VariableType.String)
			{
				saveVariable.TextValue = string.Empty;
			}

			LoadRebindings ();
		}


		public void SaveRebindings ()
		{
			GVar saveVariable = GlobalVariables.GetVariable (globalStringVariable);
			if (saveVariable != null && saveVariable.type == VariableType.String)
			{
				saveVariable.TextValue = actions.SaveBindingOverridesAsJson ();
				Options.SavePrefs ();
			}
			else
			{
				ACDebug.LogWarning ("Cannot find a Global String Variable to store input rebinding data in", this);
			}
		}

		#endregion


		#region CustomEvents

		private void OnInitialiseScene ()
		{
			LoadRebindings ();
		}


		private void OnSwitchProfile (int profileID)
		{
			LoadRebindings ();
		}

		#endregion


		#region PrivateFunctions

		private void LoadRebindings ()
		{
			GVar saveVariable = GlobalVariables.GetVariable (globalStringVariable);
			if (saveVariable != null && saveVariable.type == VariableType.String)
			{
				if (!string.IsNullOrEmpty (saveVariable.TextValue))
				{
					actions.LoadBindingOverridesFromJson (saveVariable.TextValue);
				}
			}
			else
			{
				ACDebug.LogWarning ("Cannot find a Global String Variable to store input rebinding data in", this);
			}
		}



		private InputAction GetInputAction (string axisName)
		{
			if (inputActionsDictionary.TryGetValue (axisName, out InputAction inputAction))
			{
				return inputAction;
			}

			Debug.LogWarning ("Cannot find input action with key: "+ axisName);
			return null;
		}


		private void CreateInputActionsDictionary ()
		{
			inputActionsDictionary.Clear ();

			foreach (InputActionReference inputActionReference in inputActionReferences)
			{
				inputActionsDictionary.Add (inputActionReference.action.name, inputActionReference);
			}
		}

		#endregion


		#region TouchInput

		private int Custom_TouchCount ()
		{
			return UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count;
		}


		private Vector2 Custom_TouchPosition (int index)
		{
			return UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[index].screenPosition;
		}


		private Vector2 Custom_TouchDeltaPosition (int index)
		{
			return UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[index].delta;
		}


		private UnityEngine.TouchPhase Custom_TouchPhase (int index)
		{
			UnityEngine.InputSystem.TouchPhase touchPhase = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[index].phase;
			switch (touchPhase)
			{
				case UnityEngine.InputSystem.TouchPhase.Began:
					return UnityEngine.TouchPhase.Began;

				case UnityEngine.InputSystem.TouchPhase.Canceled:
					return UnityEngine.TouchPhase.Canceled;

				case UnityEngine.InputSystem.TouchPhase.Ended:
					return UnityEngine.TouchPhase.Ended;

				case UnityEngine.InputSystem.TouchPhase.Moved:
					return UnityEngine.TouchPhase.Moved;

				case UnityEngine.InputSystem.TouchPhase.Stationary:
					return UnityEngine.TouchPhase.Stationary;

				default:
					return UnityEngine.TouchPhase.Canceled;
			}
		}

		#endregion


		#region GetSet

		public static ControlsReader Instance
		{
			get
			{
				ControlsReader controlsReader = FindObjectOfType<ControlsReader> ();
				if (controlsReader == null)
				{
					ACDebug.LogWarning ("Cannot find ControlsReader component");
				}
				return controlsReader;
			}
		}

		#endregion

	}

}