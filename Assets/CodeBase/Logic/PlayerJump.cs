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

        private Rigidbody _rigidbody;
        private bool _isGrounded;
        private IInputService _inputService;
        private InputSystem_Actions _inputActions;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

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
            Vector3 origin = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y * .5f), transform.position.z);
            Vector3 direction = transform.TransformDirection(Vector3.down);
            float distance = .75f;

            if (Physics.Raycast(origin, direction, out RaycastHit hit, distance))
            {
                Debug.DrawRay(origin, direction * distance, Color.red);
                _isGrounded = true;
            }
            else
            {
                _isGrounded = false;
            }
        }

        private void Jump()
        {
            if (_isGrounded)
            {
                _rigidbody.AddForce(0f, _jumpPower, 0f, ForceMode.Impulse);
                _isGrounded = false;
            }
        }

        public bool IsGrounded()
        {
            return _isGrounded;
        }

        public void ForceJump()
        {
            Jump();
        }
    }
}

