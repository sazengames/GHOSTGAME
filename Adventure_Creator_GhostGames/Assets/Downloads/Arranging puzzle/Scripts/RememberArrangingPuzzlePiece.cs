using UnityEngine;
using System.Collections.Generic;
using AC;

namespace AC
{

	[RequireComponent (typeof (ArrangingPuzzlePiece))]
	public class RememberArrangingPuzzlePiece : Remember
	{

		public override string SaveData ()
		{
			ArrangingPuzzlePieceData data = new ArrangingPuzzlePieceData ();
			data.objectID = constantID;
			data.savePrevented = savePrevented;

			data = GetComponent <ArrangingPuzzlePiece>().SaveData (data);

			return Serializer.SaveScriptData <ArrangingPuzzlePieceData> (data);
		}
		

		public override void LoadData (string stringData)
		{
			ArrangingPuzzlePieceData data = Serializer.LoadScriptData <ArrangingPuzzlePieceData> (stringData);

			if (data == null) return;
			SavePrevented = data.savePrevented; if (savePrevented) return;

			GetComponent <ArrangingPuzzlePiece>().LoadData (data);
		}

	}
	

	[System.Serializable]
	public class ArrangingPuzzlePieceData : RememberData
	{

		public int currentSlot;
		public float originalPositionX;
		public float originalPositionY;
		public float originalPositionZ;

		public ArrangingPuzzlePieceData () { }

	}
	
}