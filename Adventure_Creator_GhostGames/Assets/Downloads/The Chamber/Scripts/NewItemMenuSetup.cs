using UnityEngine;

namespace AC.TheChamber
{

	public class NewItemMenuSetup : MonoBehaviour
	{

		#region Variables

		private const string menuName = "NewItem";
		private const string inventoryMenuName = "Inventory";
		private const string nameElement = "ItemName";
		private const string graphicElement = "ItemGraphic";

		#endregion


		#region UnityStandards

		private void OnEnable ()
		{
			EventManager.OnInventoryAdd += OnInventoryAdd;
			EventManager.OnMenuTurnOn += OnMenuTurnOn;
			EventManager.OnMenuTurnOff += OnMenuTurnOff;
		}

		private void OnDisable ()
		{
			EventManager.OnInventoryAdd -= OnInventoryAdd;
			EventManager.OnMenuTurnOn -= OnMenuTurnOn;
			EventManager.OnMenuTurnOff -= OnMenuTurnOff;
		}

		#endregion


		#region CustomEvents

		private void OnInventoryAdd (InvItem invItem, int count)
		{
			Menu newItemMenu = PlayerMenus.GetMenuWithName (menuName);

			MenuLabel itemNameLabel = newItemMenu.GetElementWithName (nameElement) as MenuLabel;
			itemNameLabel.label = invItem.GetLabel (Options.GetLanguage ());
			itemNameLabel.UpdateLabelText ();

			MenuGraphic itemGraphic = newItemMenu.GetElementWithName (graphicElement) as MenuGraphic;
			itemGraphic.SetNormalGraphicTexture (invItem.tex);

			newItemMenu.TurnOn ();
		}


		private void OnMenuTurnOn (Menu menu, bool isInstant)
		{
			if (menu.title == menuName)
			{
				PlayerMenus.GetMenuWithName (inventoryMenuName).ignoreMouseClicks = true;
				KickStarter.playerInput.dragOverrideInput = "DragOverride";
				KickStarter.stateHandler.SetInteractionSystem (false);
			}
		}


		private void OnMenuTurnOff (Menu menu, bool isInstant)
		{
			if (menu.title == menuName)
			{
				PlayerMenus.GetMenuWithName (inventoryMenuName).ignoreMouseClicks = false;
				KickStarter.playerInput.dragOverrideInput = string.Empty;
				KickStarter.stateHandler.SetInteractionSystem (true);
			}
		}

		#endregion

	}

}