using UnityEngine;

namespace CodeBase.Logic
{
    public class PlayerJump : MonoBehaviour
    {
        [Header("Jump Settings")]
        public bool enableJump = true;
        public KeyCode jumpKey = KeyCode.Space;
        public float jumpPower = 5f;

        private Rigidbody rb;
        private bool isGrounded = false;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            CheckGround();

            if (enableJump && Input.GetKeyDown(jumpKey) && isGrounded)
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
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }

        private void Jump()
        {
            if (isGrounded)
            {
                rb.AddForce(0f, jumpPower, 0f, ForceMode.Impulse);
                isGrounded = false;
            }
        }

        public bool IsGrounded()
        {
            return isGrounded;
        }

        public void ForceJump()
        {
            Jump();
        }
    }
}

