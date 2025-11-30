using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Logic
{
    public class PlayerSprint : MonoBehaviour
    {
        [Header("Sprint Settings")]
        public bool enableSprint = true;
        public bool unlimitedSprint = false;
        public KeyCode sprintKey = KeyCode.LeftShift;
        public float sprintSpeed = 7f;
        public float sprintDuration = 5f;
        public float sprintCooldown = .5f;
        public float sprintFOV = 80f;
        public float sprintFOVStepTime = 10f;

        [Header("Sprint Bar")]
        public bool useSprintBar = true;
        public bool hideBarWhenFull = true;
        public Image sprintBarBG;
        public Image sprintBar;
        public float sprintBarWidthPercent = .3f;
        public float sprintBarHeightPercent = .015f;

        private CanvasGroup sprintBarCG;
        private bool isSprinting = false;
        private float sprintRemaining;
        private float sprintBarWidth;
        private float sprintBarHeight;
        private bool isSprintCooldown = false;
        private float sprintCooldownReset;
        private PlayerMovement playerMovement;
        private PlayerCamera playerCamera;

        private void Awake()
        {
            playerMovement = GetComponent<PlayerMovement>();
            playerCamera = GetComponentInChildren<PlayerCamera>();

            if (!unlimitedSprint)
            {
                sprintRemaining = sprintDuration;
                sprintCooldownReset = sprintCooldown;
            }
        }

        private void Start()
        {
            SetupSprintBar();
        }

        private void SetupSprintBar()
        {
            sprintBarCG = GetComponentInChildren<CanvasGroup>();

            if (useSprintBar)
            {
                sprintBarBG.gameObject.SetActive(true);
                sprintBar.gameObject.SetActive(true);

                float screenWidth = Screen.width;
                float screenHeight = Screen.height;

                sprintBarWidth = screenWidth * sprintBarWidthPercent;
                sprintBarHeight = screenHeight * sprintBarHeightPercent;

                sprintBarBG.rectTransform.sizeDelta = new Vector3(sprintBarWidth, sprintBarHeight, 0f);
                sprintBar.rectTransform.sizeDelta = new Vector3(sprintBarWidth - 2, sprintBarHeight - 2, 0f);

                if (hideBarWhenFull)
                {
                    sprintBarCG.alpha = 0;
                }
            }
            else
            {
                sprintBarBG.gameObject.SetActive(false);
                sprintBar.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (!enableSprint) return;

            HandleSprint();
            UpdateSprintBar();
        }

        private void HandleSprint()
        {
            if (isSprinting)
            {
                playerCamera.SetFOV(sprintFOV, sprintFOVStepTime);

                if (!unlimitedSprint)
                {
                    sprintRemaining -= 1 * Time.deltaTime;
                    if (sprintRemaining <= 0)
                    {
                        isSprinting = false;
                        isSprintCooldown = true;
                    }
                }
            }
            else
            {
                sprintRemaining = Mathf.Clamp(sprintRemaining + 1 * Time.deltaTime, 0, sprintDuration);
            }

            if (isSprintCooldown)
            {
                sprintCooldown -= 1 * Time.deltaTime;
                if (sprintCooldown <= 0)
                {
                    isSprintCooldown = false;
                }
            }
            else
            {
                sprintCooldown = sprintCooldownReset;
            }
        }

        private void UpdateSprintBar()
        {
            if (useSprintBar && !unlimitedSprint)
            {
                float sprintRemainingPercent = sprintRemaining / sprintDuration;
                sprintBar.transform.localScale = new Vector3(sprintRemainingPercent, 1f, 1f);

                if (hideBarWhenFull)
                {
                    if (isSprinting)
                    {
                        sprintBarCG.alpha += 5 * Time.deltaTime;
                    }
                    else if (sprintRemaining >= sprintDuration)
                    {
                        sprintBarCG.alpha -= 3 * Time.deltaTime;
                    }
                }
            }
        }

        public bool IsSprinting()
        {
            return isSprinting;
        }

        public bool CanSprint()
        {
            return enableSprint && Input.GetKey(sprintKey) && sprintRemaining > 0f && !isSprintCooldown;
        }

        public void SetSprinting(bool sprinting)
        {
            isSprinting = sprinting;
        }

        public float GetSprintSpeed()
        {
            return sprintSpeed;
        }

        public float GetWalkSpeed()
        {
            return playerMovement.GetWalkSpeed();
        }
    }
}

