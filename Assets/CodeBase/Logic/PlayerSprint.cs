using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace CodeBase.Logic
{
    public class PlayerSprint : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerCamera _playerCamera;

        [SerializeField] private bool _enableSprint = true;
        [SerializeField] private bool _unlimitedSprint = false;
        [SerializeField] private float _sprintSpeed = 7f;
        [SerializeField] private float _sprintDuration = 5f;
        [SerializeField] private float _sprintCooldown = .5f;
        [SerializeField] private float _sprintFOV = 80f;
        [SerializeField] private float _sprintFOVStepTime = 10f;

        [SerializeField] private bool _useSprintBar = true;
        [SerializeField] private bool _hideBarWhenFull = true;
        [SerializeField] private Image _sprintBarBg;
        [SerializeField] private Image _sprintBar;
        [SerializeField] private float _sprintBarWidthPercent = .3f;
        [SerializeField] private float _sprintBarHeightPercent = .015f;

        private CanvasGroup _sprintBarCg;
        private bool _isSprinting;
        private bool _isSprintKeyPressed;
        private float _sprintRemaining;
        private float _sprintBarWidth;
        private float _sprintBarHeight;
        private bool _isSprintCooldown;
        private float _sprintCooldownReset;
        private IInputService _inputService;
        private InputSystem_Actions _inputActions;

        private void Awake()
        {
            if (!_unlimitedSprint)
            {
                _sprintRemaining = _sprintDuration;
                _sprintCooldownReset = _sprintCooldown;
            }
        }

        private void Start()
        {
            SetupSprintBar();
            
            _inputService = AllServices.Container.Single<IInputService>();
            _inputActions = _inputService.GetPlayerInputActions();
            _inputActions.Player.Sprint.started += OnSprintStarted;
            _inputActions.Player.Sprint.canceled += OnSprintCanceled;
        }

        private void OnDestroy()
        {
            if (_inputActions != null)
            {
                _inputActions.Player.Sprint.started -= OnSprintStarted;
                _inputActions.Player.Sprint.canceled -= OnSprintCanceled;
            }
        }

        private void OnSprintStarted(InputAction.CallbackContext context)
        {
            _isSprintKeyPressed = true;
        }

        private void OnSprintCanceled(InputAction.CallbackContext context)
        {
            _isSprintKeyPressed = false;
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

        private void Update()
        {
            if (!_enableSprint) return;

            HandleSprint();
            UpdateSprintBar();
        }

        private void HandleSprint()
        {
            if (_isSprinting)
            {
                _playerCamera.SetFOV(_sprintFOV, _sprintFOVStepTime);

                if (!_unlimitedSprint)
                {
                    _sprintRemaining -= 1 * Time.deltaTime;
                    if (_sprintRemaining <= 0)
                    {
                        _isSprinting = false;
                        _isSprintCooldown = true;
                    }
                }
            }
            else
            {
                _sprintRemaining = Mathf.Clamp(_sprintRemaining + 1 * Time.deltaTime, 0, _sprintDuration);
            }

            if (_isSprintCooldown)
            {
                _sprintCooldown -= 1 * Time.deltaTime;
                if (_sprintCooldown <= 0)
                {
                    _isSprintCooldown = false;
                }
            }
            else
            {
                _sprintCooldown = _sprintCooldownReset;
            }
        }

        private void UpdateSprintBar()
        {
            if (_useSprintBar && !_unlimitedSprint)
            {
                float sprintRemainingPercent = _sprintRemaining / _sprintDuration;
                _sprintBar.transform.localScale = new Vector3(sprintRemainingPercent, 1f, 1f);

                if (_hideBarWhenFull)
                {
                    if (_isSprinting)
                    {
                        _sprintBarCg.alpha += 5 * Time.deltaTime;
                    }
                    else if (_sprintRemaining >= _sprintDuration)
                    {
                        _sprintBarCg.alpha -= 3 * Time.deltaTime;
                    }
                }
            }
        }

        public bool IsSprinting()
        {
            return _isSprinting;
        }

        public bool CanSprint()
        {
            return _enableSprint && _isSprintKeyPressed && _sprintRemaining > 0f && !_isSprintCooldown;
        }

        public void SetSprinting(bool sprinting)
        {
            _isSprinting = sprinting;
        }

        public float GetSprintSpeed()
        {
            return _sprintSpeed;
        }

        public float GetWalkSpeed()
        {
            return _playerMovement.GetWalkSpeed();
        }
    }
}