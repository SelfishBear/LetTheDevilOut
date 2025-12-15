using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class QuitButton : MonoBehaviour
    {
        [SerializeField] private Button _quitButton;

        private void Start()
        {
            _quitButton.onClick.AddListener(OnQuitButtonClicked);
        }

        private void OnDestroy()
        {
            _quitButton.onClick.RemoveListener(OnQuitButtonClicked);
        }

        private void OnQuitButtonClicked()
        {
            Application.Quit();
        }
    }
}