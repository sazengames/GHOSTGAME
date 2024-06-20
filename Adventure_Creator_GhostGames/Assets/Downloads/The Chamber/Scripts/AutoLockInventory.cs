using UnityEngine;

namespace AC.TheChamber
{

	public class AutoLockInventory : MonoBehaviour
	{

		#region Variables

		[SerializeField] private string menuName = "Inventory";

		#endregion


		#region UnityStandards

		private void OnEnable ()
		{
			EventManager.OnInventoryAdd += OnInventoryUpdate;
			EventManager.OnInventoryRemove += OnInventoryUpdate;
			EventManager.OnInitialiseScene += SetLockState;
		}

		private void OnDisable ()
		{
			EventManager.OnInventoryAdd -= OnInventoryUpdate;
			EventManager.OnInventoryRemove -= OnInventoryUpdate;
			EventManager.OnInitialiseScene -= SetLockState;
		}

		#endregion


		#region CustomEvents

		private void OnInventoryUpdate (InvItem invItem, int count)
		{
			SetLockState ();
		}


		private void SetLockState ()
		{
			PlayerMenus.GetMenuWithName (menuName).isLocked = (KickStarter.runtimeInventory.GetNumberOfItemsCarried () <= 0);
		}

		#endregion

	}

}