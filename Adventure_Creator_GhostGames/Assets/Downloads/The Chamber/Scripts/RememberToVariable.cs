using UnityEngine;

namespace AC.TheChamber
{

	public class RememberToVariable : MonoBehaviour
	{

		#region Variables

		[SerializeField] private string linkedVariableName;
		[SerializeField] private Remember rememberComponent = null;

		#endregion


		#region UnityStandards

		private void OnEnable ()
		{
			GVar linkedVariable = GlobalVariables.GetVariable (linkedVariableName);
			if (linkedVariable != null && !string.IsNullOrEmpty (linkedVariable.TextValue) && rememberComponent)
			{
				rememberComponent.LoadData (linkedVariable.TextValue);
			}
		}


		private void OnDisable ()
		{
			GVar linkedVariable = GlobalVariables.GetVariable (linkedVariableName);
			if (linkedVariable != null && rememberComponent)
			{
				linkedVariable.TextValue = rememberComponent.SaveData ();
			}
		}

		#endregion

	}

}