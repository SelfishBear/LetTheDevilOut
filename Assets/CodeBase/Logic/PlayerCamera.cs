using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.GameplayServices;
using UnityEngine;

namespace CodeBase.Logic
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Camera _playerCamera;

        [SerializeField] private float _fov = 60f;
        [SerializeField] private bool _invertCamera;
        [SerializeField] private bool _cameraCanMove = true;
        [SerializeField] private float _mouseSensitivity = 2f;
        [SerializeField] private float _maxLookAngle = 50f;
        [SerializeField] private bool _isCursorVisible = true;
        [SerializeField] private bool _isCursorLocked = true;

        private float _yaw;
        private float _pitch;
        private ICursorService _cursorService;

        public float FOV => _fov;

        private void Awake()
        {
            _playerCamera.fieldOfView = _fov;
        }

        private void Start()
        {
            _cursorService = AllServices.Container.Single<ICursorService>();
            _cursorService.ChangeCursorState(_isCursorVisible, _isCursorLocked);
            
        }

        private void Update()
        {
            HandleCameraRotation();
        }

        public void HandleCameraRotation()
        {
            if (!_cameraCanMove) return;

            _yaw = _playerTransform.localEulerAngles.y + Input.GetAxis("Mouse X") * _mouseSensitivity;

            if (!_invertCamera)
            {
                _pitch -= _mouseSensitivity * Input.GetAxis("Mouse Y");
            }
            else
            {
                _pitch += _mouseSensitivity * Input.GetAxis("Mouse Y");
            }

            _pitch = Mathf.Clamp(_pitch, -_maxLookAngle, _maxLookAngle);

            _playerTransform.localEulerAngles = new Vector3(0, _yaw, 0);
            _playerCamera.transform.localEulerAngles = new Vector3(_pitch, 0, 0);
        }

        public void SetFOV(float targetFOV, float stepTime)
        {
            _playerCamera.fieldOfView = Mathf.Lerp(_playerCamera.fieldOfView, targetFOV, stepTime * Time.deltaTime);
        }

        public float GetCurrentFOV()
        {
            return _playerCamera.fieldOfView;
        }
    }
    
}