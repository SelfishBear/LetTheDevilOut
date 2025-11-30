using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.Logic
{
    public class PlayerCrouch : MonoBehaviour
    {
        [SerializeField] private PlayerJump playerJump;

        [Header("Crouch Settings")] public bool enableCrouch = true;

        public bool holdToCrouch = true;
        public KeyCode crouchKey = KeyCode.LeftControl;
        public float crouchHeight = .75f;
        public float speedReduction = .5f;

        private bool isCrouched = false;
        private Vector3 originalScale;
        private PlayerMovement playerMovement;

        private void Awake()
        {
            originalScale = transform.localScale;
            playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            if (!enableCrouch) return;

            HandleUncrouching();

            HandleCrouchInput();
        }

        public bool IsCrouched()
        {
            return isCrouched;
        }

        public void Uncrouch()
        {
            if (isCrouched)
            {
                ToggleCrouch();
            }
        }

        private void HandleUncrouching()
        {
            if (playerJump != null && !holdToCrouch)
            {
                if (Input.GetKeyDown(playerJump.jumpKey) && playerJump.IsGrounded())
                {
                    Uncrouch();
                }
            }
        }

        private void HandleCrouchInput()
        {
            if (Input.GetKeyDown(crouchKey) && !holdToCrouch)
            {
                ToggleCrouch();
            }

            if (holdToCrouch)
            {
                if (Input.GetKeyDown(crouchKey))
                {
                    isCrouched = false;
                    ToggleCrouch();
                }
                else if (Input.GetKeyUp(crouchKey))
                {
                    isCrouched = true;
                    ToggleCrouch();
                }
            }
        }

        private void ToggleCrouch()
        {
            if (isCrouched)
            {
                // Stand up
                transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
                playerMovement.SetWalkSpeed(playerMovement.GetWalkSpeed() / speedReduction);
                isCrouched = false;
            }
            else
            {
                // Crouch down
                transform.localScale = new Vector3(originalScale.x, crouchHeight, originalScale.z);
                playerMovement.SetWalkSpeed(playerMovement.GetWalkSpeed() * speedReduction);
                isCrouched = true;
            }
        }
    }
}