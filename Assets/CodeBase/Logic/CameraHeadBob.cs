using UnityEngine;

namespace CodeBase.Logic
{
    public class CameraHeadBob : MonoBehaviour
    {
        [Header("Head Bob Settings")]
        public bool enableHeadBob = true;
        public Transform joint;
        public float bobSpeed = 10f;
        public Vector3 bobAmount = new Vector3(.15f, .05f, 0f);

        private Vector3 jointOriginalPos;
        private float timer = 0;
        private PlayerMovement playerMovement;
        private PlayerSprint playerSprint;
        private PlayerCrouch playerCrouch;

        private void Awake()
        {
            if (joint != null)
            {
                jointOriginalPos = joint.localPosition;
            }
        
            playerMovement = GetComponentInParent<PlayerMovement>();
            playerSprint = GetComponentInParent<PlayerSprint>();
            playerCrouch = GetComponentInParent<PlayerCrouch>();
        }

        private void Update()
        {
            if (!enableHeadBob || joint == null) return;

            ApplyHeadBob();
        }

        private void ApplyHeadBob()
        {
            if (playerMovement.IsWalking())
            {
                float currentBobSpeed = bobSpeed;

                if (playerSprint != null && playerSprint.IsSprinting())
                {
                    currentBobSpeed = bobSpeed + playerSprint.GetSprintSpeed();
                }
                else if (playerCrouch != null && playerCrouch.IsCrouched())
                {
                    currentBobSpeed = bobSpeed * playerCrouch.speedReduction;
                }

                timer += Time.deltaTime * currentBobSpeed;
                joint.localPosition = new Vector3(
                    jointOriginalPos.x + Mathf.Sin(timer) * bobAmount.x,
                    jointOriginalPos.y + Mathf.Sin(timer) * bobAmount.y,
                    jointOriginalPos.z + Mathf.Sin(timer) * bobAmount.z
                );
            }
            else
            {
                timer = 0;
                joint.localPosition = new Vector3(
                    Mathf.Lerp(joint.localPosition.x, jointOriginalPos.x, Time.deltaTime * bobSpeed),
                    Mathf.Lerp(joint.localPosition.y, jointOriginalPos.y, Time.deltaTime * bobSpeed),
                    Mathf.Lerp(joint.localPosition.z, jointOriginalPos.z, Time.deltaTime * bobSpeed)
                );
            }
        }
    }
}

