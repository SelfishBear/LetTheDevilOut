using UnityEngine;

namespace CodeBase.Logic
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private PlayerSprint playerSprint;

        [Header("Zoom Settings")] public bool enableZoom = true;

        public bool holdToZoom = false;
        public KeyCode zoomKey = KeyCode.Mouse1;
        public float zoomFOV = 30f;
        public float zoomStepTime = 5f;

        private bool isZoomed = false;
        private PlayerCamera playerCamera;
        private float defaultFOV;

        private void Awake()
        {
            playerCamera = GetComponent<PlayerCamera>();
            defaultFOV = playerCamera.fov;
        }

        private void Update()
        {
            if (!enableZoom) return;
            HandleZoomDisabling();

            HandleZoomInput();
            ApplyZoom();
        }

        public void DisableZoom()
        {
            isZoomed = false;
        }

        public bool IsZoomed()
        {
            return isZoomed;
        }

        private void HandleZoomDisabling()
        {
            if (playerSprint.IsSprinting())
            {
                DisableZoom();
            }
        }

        private void HandleZoomInput()
        {
            if (holdToZoom)
            {
                if (Input.GetKeyDown(zoomKey))
                {
                    isZoomed = true;
                }
                else if (Input.GetKeyUp(zoomKey))
                {
                    isZoomed = false;
                }
            }
            else
            {
                if (Input.GetKeyDown(zoomKey))
                {
                    isZoomed = !isZoomed;
                }
            }
        }

        private void ApplyZoom()
        {
            if (isZoomed)
            {
                playerCamera.SetFOV(zoomFOV, zoomStepTime);
            }
            else
            {
                playerCamera.SetFOV(defaultFOV, zoomStepTime);
            }
        }
    }
}