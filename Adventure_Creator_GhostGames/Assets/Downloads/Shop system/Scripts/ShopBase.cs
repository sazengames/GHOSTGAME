using UnityEngine;

namespace AC
{

	public abstract class ShopBase : MonoBehaviour
	{

		#region Variables

		[SerializeField] protected string shopItemsElementName = "ShopItems";
		[SerializeField] protected string transferItemsElementName = "TransferItems";
		[SerializeField] protected string playerMoneyInteger = "Shop/Player money";
		[SerializeField] protected string totalCostInteger = "Shop/Total cost";

		[SerializeField] protected int moneyItemID = 3;
		[SerializeField] protected Container transferContainer = null;
		[SerializeField] protected int valuePropertyID = 0;

		protected int totalCost;
		protected Menu thisMenu;
		protected GVar playerMoneyVar, totalCostVar;
		protected MenuInventoryBox transferInventoryBox;

		#endregion


		#region UnityStandards

		private void OnEnable ()
		{
			EventManager.OnMenuTurnOn += OnMenuTurnOn;
			EventManager.OnMenuTurnOff += OnMenuTurnOff;
			EventManager.OnRequestMenuElementHotspotLabel += OnRequestMenuElementHotspotLabel;
		}


		private void OnDisable ()
		{
			EventManager.OnMenuTurnOn -= OnMenuTurnOn;
			EventManager.OnMenuTurnOff -= OnMenuTurnOff;
			EventManager.OnRequestMenuElementHotspotLabel -= OnRequestMenuElementHotspotLabel;
		}

		#endregion


		#region CustomEvents

		private void OnMenuTurnOn (Menu menu, bool isInstant)
		{
			if (thisMenu == null) thisMenu = KickStarter.playerMenus.GetMenuWithCanvas (GetComponent<Canvas> ());
			if (thisMenu == null || menu != thisMenu) return;

			// When this menu is turned on, cache variables

			transferInventoryBox = menu.GetElementWithName (transferItemsElementName) as MenuInventoryBox;
			transferInventoryBox.OverrideContainer = transferContainer;

			playerMoneyVar = GlobalVariables.GetVariable (playerMoneyInteger);
			playerMoneyVar.IntegerValue = GetPlayerMoney ();

			totalCostVar = GlobalVariables.GetVariable (totalCostInteger);

			OnMenuTurnOn (menu);

			transferInventoryBox = menu.GetElementWithName (transferItemsElementName) as MenuInventoryBox;
			transferInventoryBox.OverrideContainer = transferContainer;

			playerMoneyVar = GlobalVariables.GetVariable (playerMoneyInteger);
			playerMoneyVar.IntegerValue = GetPlayerMoney ();

			totalCostVar = GlobalVariables.GetVariable (totalCostInteger);
		}


		private void OnMenuTurnOff (Menu menu, bool isInstant)
		{
			if (thisMenu == null || menu != thisMenu) return;

			// When this menu is turned off, reset everything

			Reset ();
		}


		private string OnRequestMenuElementHotspotLabel (Menu menu, MenuElement element, int slot, int language)
		{
			if (thisMenu == null || menu != thisMenu) return string.Empty;

			// When over items, override the default Hotspot label with the value

			if (element == transferInventoryBox)
			{
				InvInstance invInstance = transferInventoryBox.GetInstance (slot);

				if (InvInstance.IsValid (invInstance))
				{
					return invInstance.InvItem.GetLabel (language) + "\nValue: " + invInstance.GetProperty (0).IntegerValue + "g";
				}
			}

			return OnRequestMenuElementHotspotLabel (element, slot, language);
		}

		#endregion


		#region ProtectedFunctions

		protected virtual void OnMenuTurnOn (Menu menu)
		{ }


		protected virtual void Reset ()
		{
			// Move all items in the "transfer" Container back to the "buy" Container, and reset the variables

			totalCost = 0;

			playerMoneyVar.IntegerValue = GetPlayerMoney ();
			totalCostVar.IntegerValue = totalCost;
		}


		protected virtual string OnRequestMenuElementHotspotLabel (MenuElement element, int slot, int language)
		{
			return string.Empty;
		}


		protected int GetPlayerMoney ()
		{
			return KickStarter.runtimeInventory.PlayerInvCollection.GetCount (moneyItemID);
		}

		#endregion

	}

}