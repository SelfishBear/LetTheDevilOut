using System;
using UnityEngine;

namespace CodeBase.Logic
{
    public class PlayerCamera : MonoBehaviour
    {
        [Header("Camera Settings")]
        public Camera playerCamera;
        public float fov = 60f;
        public bool invertCamera = false;
        public bool cameraCanMove = true;
        public float mouseSensitivity = 2f;
        public float maxLookAngle = 50f;

        [Header("Cursor Settings")]
        public bool lockCursor = true;

        private float yaw = 0.0f;
        private float pitch = 0.0f;
        [SerializeField] private Transform _playerTransform;

        private void Awake()
        {
            playerCamera.fieldOfView = fov;
        }

        private void Start()
        {
            if (lockCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        private void Update()
        {
            HandleCameraRotation();
        }

        public void HandleCameraRotation()
        {
            if (!cameraCanMove) return;

            yaw = _playerTransform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;

            if (!invertCamera)
            {
                pitch -= mouseSensitivity * Input.GetAxis("Mouse Y");
            }
            else
            {
                pitch += mouseSensitivity * Input.GetAxis("Mouse Y");
            }

            pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);

            _playerTransform.localEulerAngles = new Vector3(0, yaw, 0);
            playerCamera.transform.localEulerAngles = new Vector3(pitch, 0, 0);
        }

        public void SetFOV(float targetFOV, float stepTime)
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFOV, stepTime * Time.deltaTime);
        }

        public float GetCurrentFOV()
        {
            return playerCamera.fieldOfView;
        }
    }
}

