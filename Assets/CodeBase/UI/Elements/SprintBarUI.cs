using System;
using CodeBase.Logic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Elements
{
    public class SprintBarUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _sprintBarCg;
        [SerializeField] private Image _sprintBarBg;
        [SerializeField] private Image _sprintBar;

        [SerializeField] private float _sprintBarWidthPercent = .3f;
        [SerializeField] private float _sprintBarHeightPercent = .015f;

        [SerializeField] private bool _useSprintBar = true;
        [SerializeField] private bool _hideBarWhenFull = true;

        private PlayerSprint _playerSprint;

        private float _sprintBarWidth;
        private float _sprintBarHeight;

        public void Construct(PlayerSprint playerSprint)
        {
            _playerSprint = playerSprint;
        }

        private void Start()
        {
            SetupSprintBar();
        }

        private void Update()
        {
            UpdateSprintBar();
        }

        private void SetupSprintBar()
        {
            _sprintBarCg = GetComponentInChildren<CanvasGroup>();

            if (_useSprintBar)
            {
                _sprintBarBg.gameObject.SetActive(true);
                _sprintBar.gameObject.SetActive(true);

                float screenWidth = Screen.width;
                float screenHeight = Screen.height;

                _sprintBarWidth = screenWidth * _sprintBarWidthPercent;
                _sprintBarHeight = screenHeight * _sprintBarHeightPercent;

                _sprintBarBg.rectTransform.sizeDelta = new Vector3(_sprintBarWidth, _sprintBarHeight, 0f);
                _sprintBar.rectTransform.sizeDelta = new Vector3(_sprintBarWidth - 2, _sprintBarHeight - 2, 0f);

                if (_hideBarWhenFull)
                {
                    _sprintBarCg.alpha = 0;
                }
            }
            else
            {
                _sprintBarBg.gameObject.SetActive(false);
                _sprintBar.gameObject.SetActive(false);
            }
        }

        private void UpdateSprintBar()
        {
            if (_useSprintBar && !_playerSprint.UnlimitedSprint)
            {
                float sprintRemainingPercent = _playerSprint.SprintRemaining / _playerSprint.SprintDuration;
                _sprintBar.transform.localScale = new Vector3(sprintRemainingPercent, 1f, 1f);

                if (_hideBarWhenFull)
                {
                    if (_playerSprint.IsSprinting())
                    {
                        _sprintBarCg.alpha += 5 * Time.deltaTime;
                    }
                    else if (_playerSprint.IsSprintLeft())
                    {
                        _sprintBarCg.alpha -= 3 * Time.deltaTime;
                    }
                }
            }
        }
    }
}