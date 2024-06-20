using UnityEngine;

namespace AC
{

	public class ArrangingPuzzleManager : MonoBehaviour
	{

		#region Variables

		[SerializeField] private Hotspot[] slots = new Hotspot[0];
		[SerializeField] private ArrangingPuzzlePiece[] pieces = new ArrangingPuzzlePiece[0];
		[SerializeField] private ActionList actionListOnWin = null;
		[SerializeField] private SelectedPiecePosition selectedPiecePosition = SelectedPiecePosition.Hidden;

		private enum SelectedPiecePosition { Hidden, RemainInPlace, FollowCursor };

		#endregion


		#region PublicFunctions

		public void CheckForWin ()
		{
			ArrangingPuzzlePiece[] allPieces = GameObject.FindObjectsOfType <ArrangingPuzzlePiece>();
			foreach (ArrangingPuzzlePiece piece in allPieces)
			{
				if (!piece.IsInCorrectSlot ())
				{
					return;
				}
			}

			if (actionListOnWin != null)
			{
				actionListOnWin.Interact ();
			}
		}


		public bool HotspotIsSlot (Hotspot hotspot)
		{
			foreach (Hotspot slot in slots)
			{
				if (slot == hotspot) return true;
			}
			return false;
		}


		public ArrangingPuzzlePiece GetPiece (int itemID)
		{
			foreach (ArrangingPuzzlePiece piece in pieces)
			{
				if (piece.HasAssociatedID (itemID)) return piece;
			}
			return null;
		}


		public void UpdateSelectedPiecePosition (Transform pieceTransform, float cameraDepth)
		{
			switch (selectedPiecePosition)
			{
				case SelectedPiecePosition.Hidden:
					pieceTransform.position = new Vector2(1000f, 0f);
					break;

				case SelectedPiecePosition.FollowCursor:
					Vector3 screenPosition = new Vector3 (KickStarter.playerInput.GetMousePosition ().x, KickStarter.playerInput.GetMousePosition ().y, cameraDepth);
					pieceTransform.position = Camera.main.ScreenToWorldPoint(screenPosition);
					break;

				default:
					break;
			}
		}

		#endregion


		#region Instance

		private static ArrangingPuzzleManager instance;
		public static ArrangingPuzzleManager Instance
		{
			get
			{
				if (instance == null)
				{
					instance = GameObject.FindObjectOfType <ArrangingPuzzleManager>();
				}
				return instance;
			}
		}

		#endregion

	}

}