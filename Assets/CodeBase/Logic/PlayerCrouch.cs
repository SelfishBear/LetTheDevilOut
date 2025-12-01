using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeBase.Logic
{
    public class PlayerCrouch : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerJump _playerJump;

        [SerializeField] private bool _enableCrouch = true;
        [SerializeField] private bool _holdToCrouch = true;
        [SerializeField] private float _crouchHeight = .75f;
        [SerializeField] private float _speedReduction = .5f;

        private bool _isCrouched;
        private Vector3 _originalScale;
        private IInputService _inputService;
        private InputSystem_Actions _inputActions;

        public float SpeedReduction => _speedReduction;

        private void Awake()
        {
            _originalScale = transform.localScale;
        }

        private void Start()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _inputActions = _inputService.GetPlayerInputActions();

            HoldToCrouchSubscription();
            UncrouchSubscription();
        }

        private void OnDestroy()
        {
            if (_inputActions != null)
            {
                HoldToCrouchUnsubscription();
                UncrouchUnsubscription();
            }
        }

        private void UncrouchUnsubscription()
        {
            if (_playerJump != null && !_holdToCrouch)
            {
                _inputActions.Player.Jump.performed -= OnJumpPressed;
            }
        }

        private void HoldToCrouchUnsubscription()
        {
            if (_holdToCrouch)
            {
                _inputActions.Player.Crouch.started -= OnCrouchStarted;
                _inputActions.Player.Crouch.canceled -= OnCrouchCanceled;
            }
            else
            {
                _inputActions.Player.Crouch.performed -= OnCrouchToggle;
            }
        }

        private void UncrouchSubscription()
        {
            if (_playerJump != null && !_holdToCrouch)
            {
                _inputActions.Player.Jump.performed += OnJumpPressed;
            }
        }

        private void HoldToCrouchSubscription()
        {
            if (_holdToCrouch)
            {
                _inputActions.Player.Crouch.started += OnCrouchStarted;
                _inputActions.Player.Crouch.canceled += OnCrouchCanceled;
            }
            else
            {
                _inputActions.Player.Crouch.performed += OnCrouchToggle;
            }
        }

        private void OnCrouchStarted(InputAction.CallbackContext context)
        {
            if (!_enableCrouch) return;

            if (!_isCrouched)
            {
                Crouch();
            }
        }

        private void OnCrouchCanceled(InputAction.CallbackContext context)
        {
            if (!_enableCrouch) return;

            if (_isCrouched)
            {
                Uncrouch();
            }
        }

        private void OnCrouchToggle(InputAction.CallbackContext context)
        {
            if (!_enableCrouch) return;

            ToggleCrouch();
        }

        private void OnJumpPressed(InputAction.CallbackContext context)
        {
            if (_playerJump != null && _playerJump.IsGrounded() && _isCrouched)
            {
                Uncrouch();
            }
        }

        public bool IsCrouched()
        {
            return _isCrouched;
        }

        public void Uncrouch()
        {
            if (!_isCrouched) return;

            transform.localScale = new Vector3(_originalScale.x, _originalScale.y, _originalScale.z);
            _playerMovement.SetWalkSpeed(_playerMovement.GetWalkSpeed() / _speedReduction);
            _isCrouched = false;
        }

        private void Crouch()
        {
            if (_isCrouched) return;

            transform.localScale = new Vector3(_originalScale.x, _crouchHeight, _originalScale.z);
            _playerMovement.SetWalkSpeed(_playerMovement.GetWalkSpeed() * _speedReduction);
            _isCrouched = true;
        }

        private void ToggleCrouch()
        {
            if (_isCrouched)
            {
                transform.localScale = new Vector3(_originalScale.x, _originalScale.y, _originalScale.z);
                _playerMovement.SetWalkSpeed(_playerMovement.GetWalkSpeed() / _speedReduction);
                _isCrouched = false;
            }
            else
            {
                transform.localScale = new Vector3(_originalScale.x, _crouchHeight, _originalScale.z);
                _playerMovement.SetWalkSpeed(_playerMovement.GetWalkSpeed() * _speedReduction);
                _isCrouched = true;
            }
        }
    }
}