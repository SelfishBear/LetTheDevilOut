using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeBase.Logic
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private PlayerSprint _playerSprint;

        [SerializeField] private bool _enableZoom = true;
        [SerializeField] private bool _holdToZoom = false;
        [SerializeField] private float _zoomFOV = 30f;
        [SerializeField] private float _zoomStepTime = 5f;

        private bool _isZoomed;
        private float _defaultFOV;
        private PlayerCamera _playerCamera;
        private IInputService _inputService;
        private InputSystem_Actions _inputActions;

        private void Awake()
        {
            _playerCamera = GetComponent<PlayerCamera>();
            _defaultFOV = _playerCamera.FOV;
        }

        private void Start()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _inputActions = _inputService.GetPlayerInputActions();
            
            if (_holdToZoom)
            {
                _inputActions.Player.Zoom.started += OnZoomStarted;
                _inputActions.Player.Zoom.canceled += OnZoomCanceled;
            }
            else
            {
                _inputActions.Player.Zoom.performed += OnZoomToggle;
            }
        }

        private void OnDestroy()
        {
            if (_inputActions != null)
            {
                if (_holdToZoom)
                {
                    _inputActions.Player.Zoom.started -= OnZoomStarted;
                    _inputActions.Player.Zoom.canceled -= OnZoomCanceled;
                }
                else
                {
                    _inputActions.Player.Zoom.performed -= OnZoomToggle;
                }
            }
        }

        private void Update()
        {
            if (!_enableZoom) return;
            
            if (_playerSprint.IsSprinting())
            {
                DisableZoom();
            }

            ApplyZoom();
        }

        private void OnZoomStarted(InputAction.CallbackContext context)
        {
            if (_enableZoom && !_playerSprint.IsSprinting())
            {
                _isZoomed = true;
            }
        }

        private void OnZoomCanceled(InputAction.CallbackContext context)
        {
            _isZoomed = false;
        }

        private void OnZoomToggle(InputAction.CallbackContext context)
        {
            if (_enableZoom && !_playerSprint.IsSprinting())
            {
                _isZoomed = !_isZoomed;
            }
        }

        public void DisableZoom()
        {
            _isZoomed = false;
        }

        public bool IsZoomed()
        {
            return _isZoomed;
        }


        private void ApplyZoom()
        {
            if (_isZoomed)
            {
                _playerCamera.SetFOV(_zoomFOV, _zoomStepTime);
            }
            else
            {
                _playerCamera.SetFOV(_defaultFOV, _zoomStepTime);
            }
        }
    }
}