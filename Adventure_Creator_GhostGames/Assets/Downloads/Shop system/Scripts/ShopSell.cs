using UnityEngine;

namespace AC
{

	public class ShopSell : ShopBase
	{

		#region Variables

		[SerializeField] private string playerItemsElementName = "PlayerItems";
		[SerializeField] [Range (0f, 1f)] private float valueReductionFactor = 0.8f;
		
		private InvCollection shopCollection;
		private MenuInventoryBox playerInventoryBox;

		#endregion



		#region PublicFunctions

		public void OnClickItemSlot (int slot)
		{
			// When an item from the Player's inventory is clicked, move it to the "transfer" Container

			InvInstance clickedInstance = playerInventoryBox.GetInstance (slot);
			if (!InvInstance.IsValid (clickedInstance))
			{
				return;
			}

			clickedInstance.TransferCount = 1;
			transferContainer.InvCollection.Add (clickedInstance);
			CalculateTotalWorth ();
		}


		public void OnClickTransferSlot (int slot)
		{
			// When an item from the "transfer" Container is clicked, move it back to the Player's inventory

			InvInstance clickedInstance = transferInventoryBox.GetInstance (slot);
			if (!InvInstance.IsValid (clickedInstance))
			{
				return;
			}

			clickedInstance.TransferCount = 1;
			KickStarter.runtimeInventory.PlayerInvCollection.Add (clickedInstance);
			CalculateTotalWorth ();
		}


		public void OnClickSell ()
		{
			// Move all items in the "transfer" Container to the shop, and add the funds

			shopCollection.TransferAll (transferContainer.InvCollection);
			KickStarter.runtimeInventory.PlayerInvCollection.Add (new InvInstance (moneyItemID, totalCost));
			Reset ();
		}

		#endregion


		#region ProtectedFunctions

		protected override string OnRequestMenuElementHotspotLabel (MenuElement element, int slot, int language)
		{
			if (element == playerInventoryBox)
			{
				InvInstance invInstance = playerInventoryBox.GetInstance (slot);
				if (InvInstance.IsValid (invInstance))
				{
					int value = (int) ((float) invInstance.GetProperty (0).IntegerValue * valueReductionFactor);
					return invInstance.InvItem.GetLabel (language) + "\nValue: " + value.ToString () + "g";
				}
			}
			
			return string.Empty;
		}


		protected override void OnMenuTurnOn (Menu menu)
		{
			playerInventoryBox = menu.GetElementWithName (playerItemsElementName) as MenuInventoryBox;

			MenuInventoryBox shopInventoryBox = menu.GetElementWithName (shopItemsElementName) as MenuInventoryBox;
			shopCollection = shopInventoryBox.OverrideContainer.InvCollection;
		}

		#endregion


		#region PrivateFunctions

		protected override void Reset ()
		{
			if (KickStarter.runtimeInventory) KickStarter.runtimeInventory.PlayerInvCollection.TransferAll (transferContainer.InvCollection);
			base.Reset ();
		}


		private void CalculateTotalWorth ()
		{
			// Go through each item in the "transfer" Container, and add up each in individual price

			int newTotalWorth = 0;

			foreach (InvInstance transferInstance in transferContainer.InvCollection.InvInstances)
			{
				int singleCost = transferInstance.InvItem.GetProperty (valuePropertyID).IntegerValue;
				int instanceCost = singleCost * transferInstance.Count;

				newTotalWorth += (int) ((float) instanceCost * valueReductionFactor);
			}

			totalCost = newTotalWorth;
			totalCostVar.IntegerValue = totalCost;
		}

		#endregion

	}

}