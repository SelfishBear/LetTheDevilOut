using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerSprint _playerSprint;
        [SerializeField] private PlayerCrouch _playerCrouch;

        [SerializeField] private bool _playerCanMove = true;
        [SerializeField] private float _walkSpeed = 5f;
        [SerializeField] private float _maxVelocityChange = 10f;

        private Rigidbody _rigidbody;
        private IInputService _inputService;
        private bool _isWalking;
        private float _currentSpeed;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _currentSpeed = _walkSpeed;
        }

        private void Start()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            if (_playerSprint != null && _playerSprint.CanSprint())
            {
                float speedRatio = _playerSprint.GetSprintSpeed() / _playerSprint.GetWalkSpeed();
                Move(speedRatio);
                _playerSprint.SetSprinting(true);

                if (_playerCrouch != null && _playerCrouch.IsCrouched())
                {
                    _playerCrouch.Uncrouch();
                }
            }
            else
            {
                Move();
                if (_playerSprint != null)
                {
                    _playerSprint.SetSprinting(false);
                }
            }
        }

        public void Move(float speedMultiplier = 1f)
        {
            if (!_playerCanMove) return;

            Vector3 targetVelocity = new Vector3(_inputService.MoveDirection.x, 0, _inputService.MoveDirection.y);

            _isWalking = (targetVelocity.x != 0 || targetVelocity.z != 0);

            _currentSpeed = _walkSpeed * speedMultiplier;
            targetVelocity = transform.TransformDirection(targetVelocity) * _currentSpeed;

            Vector3 velocity = _rigidbody.linearVelocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -_maxVelocityChange, _maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -_maxVelocityChange, _maxVelocityChange);
            velocityChange.y = 0;

            _rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
        }

        public bool IsWalking()
        {
            return _isWalking;
        }

        public float GetWalkSpeed()
        {
            return _walkSpeed;
        }

        public void SetWalkSpeed(float speed)
        {
            _walkSpeed = speed;
        }
    }
}