using UnityEngine;

namespace AC
{

	public class ArrangingPuzzlePiece : MonoBehaviour
	{

		#region Variables

		[SerializeField] private Hotspot correctSlot = null;
		[SerializeField] private Hotspot initialSlot = null;
		[SerializeField] private bool lockWhenCorrect = false;
		[SerializeField] private bool onlyCorrectAccepted = false;
		[SerializeField] private bool revertPositionWhenLetGo = true;
		[SerializeField] private int associatedItemID = 0;

		private Hotspot currentSlot;
		private Hotspot ownHotspot;
		private Vector3 originalPosition;
		private bool isSelected;
		private float cameraDepth;

		#endregion


		#region UnityStandards

		private void OnEnable ()
		{
			ownHotspot = GetComponent <Hotspot>();

			// Create event hooks
			EventManager.OnHotspotInteract += OnHotspotInteract;
			EventManager.OnInventorySelect += OnInventorySelect;
			EventManager.OnInventoryDeselect += OnInventoryDeselect;

			// Record the current position
			BackupPosition ();

			// Place in it's initial slot, if set
			if (initialSlot != null)
			{
				SetCurrentSlot (initialSlot);
			}
		}


		private void OnDisable ()
		{
			// Clear event hooks
			EventManager.OnHotspotInteract -= OnHotspotInteract;
			EventManager.OnInventorySelect -= OnInventorySelect;
			EventManager.OnInventoryDeselect -= OnInventoryDeselect;
		}


		private void Update ()
		{
			// Follow the cursor if it should
			if (isSelected)
			{
				ArrangingPuzzleManager.Instance.UpdateSelectedPiecePosition (transform, cameraDepth);
			}
		}

		#endregion


		#region CustomEvents

		private void OnHotspotInteract (Hotspot hotspot, AC.Button button)
		{
			// Get the inventory item selected when the Hotspot was used
			InvItem selectedItem = KickStarter.runtimeInventory.SelectedItem;
			if (selectedItem == null) return;

			// Only interested in unhandled inventory interactions
			if (button == null || hotspot.GetButtonInteractionType (button) == HotspotInteractionType.UnhandledInventory)
			{
				if (ArrangingPuzzleManager.Instance.HotspotIsSlot (hotspot) && selectedItem.id == associatedItemID)
				{
					// Used this piece's associated inventory item on a slot Hotspot, so place it over it and check for the win state
					if (onlyCorrectAccepted)
					{
						if (correctSlot && hotspot != correctSlot)
						{
							return;
						}
					}
					
					SetCurrentSlot (hotspot);
					ArrangingPuzzleManager.Instance.CheckForWin ();
				}
				else if (hotspot == ownHotspot)
				{
					ArrangingPuzzlePiece piece = ArrangingPuzzleManager.Instance.GetPiece (selectedItem.id);
					if (piece != null)
					{
						if (piece.onlyCorrectAccepted)
						{
							if (piece.correctSlot && ownHotspot && ownHotspot != piece.correctSlot)
							{
								return;
							}
						}

						// Another item was used on this GameObject's own Hotspot, so swap the two around
						Vector3 tempPosition = originalPosition;
						originalPosition = piece.originalPosition;
						piece.originalPosition = tempPosition;
						
						Hotspot tempSlot = currentSlot;
						SetCurrentSlot (piece.currentSlot);
						piece.SetCurrentSlot (tempSlot);

						ArrangingPuzzleManager.Instance.CheckForWin ();
					}
				}
			}
		}


		private void OnInventorySelect (InvItem item)
		{
			if (item.id == associatedItemID)
			{
				// If this piece's associated Inventory item is selected, backup the position, turn on it\s current slot's slot Hotspot, and hide if appropriate

				BackupPosition ();

				if (currentSlot != null)
				{
					currentSlot.TurnOn ();
				}

				isSelected = true;
				ownHotspot.TurnOff ();
			}
		}

		
		private void OnInventoryDeselect (InvItem item)
		{
			if (item.id == associatedItemID)
			{
				// If this piece's associated Inventory item is deselected, update it's position and remove from the player's inventory
				OnInventoryDeselect ();
				KickStarter.runtimeInventory.Remove (associatedItemID);
			}
		}
		
		#endregion


		#region PublicFunctions

		public bool HasAssociatedID (int itemID)
		{
			return associatedItemID == itemID;
		}


		public bool IsInCorrectSlot ()
		{
			return (correctSlot != null && currentSlot == correctSlot);
		}


		public ArrangingPuzzlePieceData SaveData (ArrangingPuzzlePieceData data)
		{
			data.currentSlot = (currentSlot != null) ? Serializer.GetConstantID (currentSlot.gameObject) : 0;

			data.originalPositionX = originalPosition.x;
			data.originalPositionY = originalPosition.y;
			data.originalPositionZ = originalPosition.z;

			return data;
		}


		public void LoadData (ArrangingPuzzlePieceData data)
		{
			originalPosition = new Vector3 (data.originalPositionX, data.originalPositionY, data.originalPositionZ);
			SetCurrentSlot ((data.currentSlot != 0) ? Serializer.returnComponent <Hotspot> (data.currentSlot) : null);
		}

		#endregion


		#region PrivateFunctions

		private void OnInventoryDeselect ()
		{
			if (currentSlot != null)
			{
				transform.position = currentSlot.transform.position;
				currentSlot.TurnOff ();
			}
			else if (revertPositionWhenLetGo)
			{
				transform.position = originalPosition;
			}
			else
			{
				Vector3 mousePos = KickStarter.playerInput.GetMousePosition ();
    			mousePos.z = transform.position.z - Camera.main.transform.position.z;
    			transform.position = Camera.main.ScreenToWorldPoint(mousePos);

			}

			isSelected = false;

			if (currentSlot != null && currentSlot == correctSlot && lockWhenCorrect)
			{
				// Don't turn on
			}
			else
			{
				ownHotspot.TurnOn ();
			}
		}


		private void BackupPosition ()
		{
			originalPosition = transform.position;
			cameraDepth = Vector3.Dot (transform.position - Camera.main.transform.position, Camera.main.transform.forward);
		}


		private void SetCurrentSlot (Hotspot slotHotspot)
		{
			currentSlot = slotHotspot;
			OnInventoryDeselect ();

			if (currentSlot != null && currentSlot == correctSlot && lockWhenCorrect)
			{
				ownHotspot.TurnOff ();
			}
		}

		#endregion

	}

}