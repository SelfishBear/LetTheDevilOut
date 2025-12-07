using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeBase.Logic
{
    public class PlayerJump : MonoBehaviour
    {
        [SerializeField] private bool _enableJump = true;
        [SerializeField] private float _jumpPower = 5f;
        [SerializeField] private float _gravity = -9.81f;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private PlayerMovement _playerMovement;

        private bool _isGrounded;
        private IInputService _inputService;
        private InputSystem_Actions _inputActions;
        
        private void Start()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _inputActions = _inputService.GetPlayerInputActions();
            _inputActions.Player.Jump.performed += OnJumpPerformed;
        }

        private void OnDestroy()
        {
            if (_inputActions != null)
            {
                _inputActions.Player.Jump.performed -= OnJumpPerformed;
            }
        }

        private void Update()
        {
            CheckGround();
        }

        private void OnJumpPerformed(InputAction.CallbackContext context)
        {
            if (_enableJump && _isGrounded)
            {
                Jump();
            }
        }

        private void CheckGround()
        {
            _isGrounded = _characterController.isGrounded;
        }

        private void Jump()
        {
            print("Jump");
            if (_isGrounded)
            {
                print("IsGrounded");
                float jumpVelocity = Mathf.Sqrt(_jumpPower * -2f * _gravity);
                _playerMovement.SetVerticalVelocity(jumpVelocity);
            }
        }

        public bool IsGrounded()
        {
            return _isGrounded;
        }
    }
}
