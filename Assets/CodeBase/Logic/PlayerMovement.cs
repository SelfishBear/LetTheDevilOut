using System;
using CodeBase.Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerSprint playerSprint;
        [SerializeField] private PlayerCrouch playerCrouch;

        [Header("Movement Settings")] public bool playerCanMove = true;
        public float walkSpeed = 5f;
        public float maxVelocityChange = 10f;

        private Rigidbody rb;
        private bool isWalking = false;
        private float currentSpeed;
        private IInputService _inputService;


        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            currentSpeed = walkSpeed;
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            if (playerSprint != null && playerSprint.CanSprint())
            {
                float speedRatio = playerSprint.GetSprintSpeed() / playerSprint.GetWalkSpeed();
                Move(speedRatio);
                playerSprint.SetSprinting(true);

                if (playerCrouch != null && playerCrouch.IsCrouched())
                {
                    playerCrouch.Uncrouch();
                }
            }
            else
            {
                Move();
                if (playerSprint != null)
                {
                    playerSprint.SetSprinting(false);
                }
            }
        }

        public void Move(float speedMultiplier = 1f)
        {
            if (!playerCanMove) return;

            Vector3 targetVelocity = new Vector3(_inputService.MoveDirection.x, 0, _inputService.MoveDirection.y);

            isWalking = (targetVelocity.x != 0 || targetVelocity.z != 0);

            currentSpeed = walkSpeed * speedMultiplier;
            targetVelocity = transform.TransformDirection(targetVelocity) * currentSpeed;

            Vector3 velocity = rb.linearVelocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;

            rb.AddForce(velocityChange, ForceMode.VelocityChange);
        }

        public bool IsWalking()
        {
            return isWalking;
        }

        public float GetWalkSpeed()
        {
            return walkSpeed;
        }

        public void SetWalkSpeed(float speed)
        {
            walkSpeed = speed;
        }
    }
}