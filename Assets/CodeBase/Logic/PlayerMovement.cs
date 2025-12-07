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
        [SerializeField] private CharacterController _characterController;

        [SerializeField] private bool _playerCanMove = true;
        [SerializeField] private float _walkSpeed = 5f;
        [SerializeField] private float _maxVelocityChange = 10f;

        private IInputService _inputService;
        private bool _isWalking;
        private float _currentSpeed;
        private Vector3 _velocity;
        private float _gravity = -9.81f;
        
        public Vector3 Velocity => _velocity;

        private void Awake()
        {
            _currentSpeed = _walkSpeed;
        }

        private void Start()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Update()
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

        private void Move(float speedMultiplier = 1f)
        {
            if (!_playerCanMove) return;

            Vector3 inputDirection = new Vector3(_inputService.MoveDirection.x, 0, _inputService.MoveDirection.y);

            _isWalking = (inputDirection.x != 0 || inputDirection.z != 0);

            _currentSpeed = _walkSpeed * speedMultiplier;
            Vector3 moveDirection = transform.TransformDirection(inputDirection) * _currentSpeed;

            if (_characterController.isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            _velocity.y += _gravity * Time.deltaTime;

            Vector3 finalMovement = moveDirection + new Vector3(0, _velocity.y, 0);
            _characterController.Move(finalMovement * Time.deltaTime);
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

        public void SetVerticalVelocity(float velocity)
        {
            _velocity.y = velocity;
        }
    }
}