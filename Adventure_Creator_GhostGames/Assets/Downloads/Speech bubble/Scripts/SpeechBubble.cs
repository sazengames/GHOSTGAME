using UnityEngine;

namespace AC
{

	public class SpeechBubble : MonoBehaviour
	{

		#if TextMeshProIsPresent

		[SerializeField] private RectTransform panelRect = null;
		[SerializeField] private RectTransform containerRect = null;
		[SerializeField] private RectTransform arrowRect = null;
		[SerializeField] private TMPro.TextMeshProUGUI textBox = null;
		[SerializeField] private Canvas canvas = null;
		[SerializeField] private float maxWidth = 350f;
		[SerializeField] private float minWidth = 50f;
		
		private const float arrowRelativePosition = 0.5f;


		private void LateUpdate ()
		{
			float preferredWidth = textBox.GetPreferredValues (textBox.text).x;
			float panelWidth = Mathf.Min (preferredWidth, maxWidth);
			panelWidth = Mathf.Max (panelWidth, minWidth);

			if (arrowRect)
			{
				arrowRect.localPosition = containerRect.localPosition + panelRect.localPosition + new Vector3 (containerRect.sizeDelta.x * 0.5f * arrowRelativePosition, -containerRect.sizeDelta.y * 0.5f, 0f);
			}

			if (panelRect.sizeDelta.x != panelWidth)
			{
				panelRect.sizeDelta = new Vector2 (panelWidth, panelRect.sizeDelta.y);
				
				Menu subsMenu = KickStarter.playerMenus.GetMenuWithCanvas (canvas);
				if (subsMenu != null)
				{
					KickStarter.playerMenus.UpdateMenuPosition (subsMenu, Vector2.zero);
				}
			}
		}

		#endif

	}

}