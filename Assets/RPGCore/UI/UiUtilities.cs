using UnityEngine;

namespace RPGCore.UI
{
	public static class UiUtilities
	{
		public static Vector3 AnchoredPosition(RectTransform origin,
											   RectTransform target,
											   RectAnchor pos,
											   float offset)
		{
			var originCorners = new Vector3[4];
			origin.GetWorldCorners(originCorners);

			var retVal = new Vector3(0, 0, 0);
			switch (pos)
			{
				case RectAnchor.Top:
					retVal = ((originCorners[1] + originCorners[2]) * 0.5f) +
							 new Vector3(0, target.rect.height / 2f + offset, 0);
					break;
				case RectAnchor.Bottom:
					retVal = ((originCorners[0] + originCorners[3]) * 0.5f) -
							 new Vector3(0, target.rect.height / 2f + offset, 0);
					break;
				case RectAnchor.Left:
					retVal = ((originCorners[0] + originCorners[1]) * 0.5f) -
							 new Vector3(target.rect.width / 2f + offset, 0, 0);
					break;
				case RectAnchor.Right:
					retVal = ((originCorners[2] + originCorners[3]) * 0.5f) +
							 new Vector3(target.rect.width / 2f + offset, 0, 0);
					break;
			}

			return retVal;
		}
	}
}