using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Logic
{
    public class Crosshair : MonoBehaviour
    {
        [Header("Crosshair Settings")]
        public bool showCrosshair = true;
        public Sprite crosshairImage;
        public Color crosshairColor = Color.white;

        private Image crosshairObject;

        private void Awake()
        {
            crosshairObject = GetComponentInChildren<Image>();
        }

        private void Start()
        {
            if (showCrosshair)
            {
                crosshairObject.sprite = crosshairImage;
                crosshairObject.color = crosshairColor;
            }
            else
            {
                crosshairObject.gameObject.SetActive(false);
            }
        }

        public void Show()
        {
            crosshairObject.gameObject.SetActive(true);
        }

        public void Hide()
        {
            crosshairObject.gameObject.SetActive(false);
        }
    }
}

