using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Logic
{
    public class Crosshair : MonoBehaviour
    {
        [SerializeField] private bool _showCrosshair = true;
        [SerializeField] private Sprite _crosshairImage;
        [SerializeField] private Image _crosshairObject;
        [SerializeField] private Color _crosshairColor = Color.white;

        private void Start()
        {
            if (_showCrosshair)
            {
                _crosshairObject.sprite = _crosshairImage;
                _crosshairObject.color = _crosshairColor;
            }
            else
            {
                _crosshairObject.gameObject.SetActive(false);
            }
        }
        
        public void Show()
        {
            _crosshairObject.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _crosshairObject.gameObject.SetActive(false);
        }
    }
}