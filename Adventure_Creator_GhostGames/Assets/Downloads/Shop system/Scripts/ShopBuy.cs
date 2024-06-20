using UnityEngine;

namespace AC
{

	public class ShopBuy : ShopBase
	{

		#region Variables
		
		private InvCollection buyCollection;
		private MenuInventoryBox shopInventoryBox;
		
		#endregion


		#region PublicFunctions

		public void OnClickItemSlot (int slot)
		{
			// When an item from the "buy" Container is clicked, move it to the "transfer" Container

			InvInstance clickedInstance = shopInventoryBox.GetInstance (slot);
			if (!InvInstance.IsValid (clickedInstance))
			{
				return;
			}

			clickedInstance.TransferCount = 1;
			transferContainer.InvCollection.Add (clickedInstance);
			CalculateTotalCost ();
		}


		public void OnClickTransferSlot (int slot)
		{
			// When an item from the "transfer" Container is clicked, move it back to the "buy" Container

			InvInstance clickedInstance = transferInventoryBox.GetInstance (slot);
			if (!InvInstance.IsValid (clickedInstance))
			{
				return;
			}

			clickedInstance.TransferCount = 1;
			buyCollection.Add (clickedInstance);
			CalculateTotalCost ();
		}


		public void OnClickBuy ()
		{
			// If the player has enough money, move all items in the "transfer" Container to their Inventory, and deduct the funds

			if (totalCost > 0 && PlayerCanAfford ())
			{
				KickStarter.runtimeInventory.PlayerInvCollection.TransferAll (transferContainer.InvCollection);
				KickStarter.runtimeInventory.PlayerInvCollection.Delete (moneyItemID, totalCost);
				Reset ();
			}
		}

		#endregion


		#region CustomEvents

		protected override void OnMenuTurnOn (Menu menu)
		{
			shopInventoryBox = menu.GetElementWithName (shopItemsElementName) as MenuInventoryBox;
			buyCollection = shopInventoryBox.OverrideContainer.InvCollection;

		}


		protected override string OnRequestMenuElementHotspotLabel (MenuElement element, int slot, int language)
		{
			// When over items, override the default Hotspot label with the value

			if (element == shopInventoryBox)
			{
				InvInstance invInstance = shopInventoryBox.GetInstance (slot);
				if (InvInstance.IsValid (invInstance))
				{
					return invInstance.InvItem.GetLabel (language) + "\nValue: " + invInstance.GetProperty (0).IntegerValue + "g";
				}
			}
			
			return string.Empty;
		}

		#endregion


		#region PrivateFunctions

		protected override void Reset ()
		{
			if (transferContainer) buyCollection.TransferAll (transferContainer.InvCollection);
			base.Reset ();
		}


		private bool PlayerCanAfford ()
		{
			return GetPlayerMoney () >= totalCost;
		}


		private void CalculateTotalCost ()
		{
			// Go through each item in the "transfer" Container, and add up each in individual price

			int newTotalCost = 0;

			foreach (InvInstance transferInstance in transferContainer.InvCollection.InvInstances)
			{
				int singleCost = transferInstance.InvItem.GetProperty (valuePropertyID).IntegerValue;
				int instanceCost = singleCost * transferInstance.Count;
				newTotalCost += instanceCost;
			}

			totalCost = newTotalCost;
			totalCostVar.IntegerValue = totalCost;
		}

		#endregion

	}

}