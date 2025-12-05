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
        [SerializeField] private bool _unlimitedSprint;
        [SerializeField] private float _sprintSpeed = 7f;
        [SerializeField] private float _sprintDuration = 5f;
        [SerializeField] private float _sprintCooldown = .5f;
        [SerializeField] private float _sprintFOV = 80f;
        [SerializeField] private float _sprintFOVStepTime = 10f;

        private bool _isSprinting;
        private bool _isSprintKeyPressed;

        private float _sprintRemaining;
        private bool _isSprintCooldown;
        private float _sprintCooldownReset;

        private IInputService _inputService;
        private InputSystem_Actions _inputActions;

        public bool UnlimitedSprint => _unlimitedSprint;
        public float SprintRemaining => _sprintRemaining;
        public float SprintDuration => _sprintDuration;


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

        private void Update()
        {
            if (!_enableSprint) return;

            HandleSprint();
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
        
        public bool IsSprinting()
        {
            return _isSprinting;
        }

        public bool IsSprintLeft()
        {
            return _sprintRemaining >= _sprintDuration;
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