using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
	public class HpBar : MonoBehaviour
	{
		public Image ImageCurrent;
		public Image ImageCurrentBackground;
		private float _baseHp;

		public void SetValue(float current, float max)
		{
			float scaleX = current / max;
			Vector3 scale = ImageCurrent.transform.localScale;
			scale.x = scaleX;
			ImageCurrent.transform.localScale = scale;
			if (_baseHp == 0)
				_baseHp = max;
		}

		public void SetMaxValue(float max)
		{
			float multiplier = max / _baseHp;

			Vector2 scale = ImageCurrentBackground.transform.localScale;
			scale.x = multiplier;
			ImageCurrentBackground.transform.localScale = scale;
		}
	}
}