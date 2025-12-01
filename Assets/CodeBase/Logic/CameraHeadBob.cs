using UnityEngine;

namespace CodeBase.Logic
{
    public class CameraHeadBob : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerSprint _playerSprint;
        [SerializeField] private PlayerCrouch _playerCrouch;
        [SerializeField] private bool _enableHeadBob = true;
        [SerializeField] private Transform _joint;
        [SerializeField] private float _bobSpeed = 10f;
        [SerializeField] private Vector3 _bobAmount = new Vector3(.15f, .05f, 0f);

        private Vector3 _jointOriginalPos;
        private float _timer;

        private void Awake()
        {
            if (_joint != null)
            {
                _jointOriginalPos = _joint.localPosition;
            }
        }

        private void Update()
        {
            if (!_enableHeadBob || _joint == null) return;

            ApplyHeadBob();
        }

        private void ApplyHeadBob()
        {
            if (_playerMovement.IsWalking())
            {
                float currentBobSpeed = _bobSpeed;

                if (_playerSprint != null && _playerSprint.IsSprinting())
                {
                    currentBobSpeed = _bobSpeed + _playerSprint.GetSprintSpeed();
                }
                else if (_playerCrouch != null && _playerCrouch.IsCrouched())
                {
                    currentBobSpeed = _bobSpeed * _playerCrouch.SpeedReduction;
                }

                _timer += Time.deltaTime * currentBobSpeed;
                _joint.localPosition = new Vector3(
                    _jointOriginalPos.x + Mathf.Sin(_timer) * _bobAmount.x,
                    _jointOriginalPos.y + Mathf.Sin(_timer) * _bobAmount.y,
                    _jointOriginalPos.z + Mathf.Sin(_timer) * _bobAmount.z
                );
            }
            else
            {
                _timer = 0;
                _joint.localPosition = new Vector3(
                    Mathf.Lerp(_joint.localPosition.x, _jointOriginalPos.x, Time.deltaTime * _bobSpeed),
                    Mathf.Lerp(_joint.localPosition.y, _jointOriginalPos.y, Time.deltaTime * _bobSpeed),
                    Mathf.Lerp(_joint.localPosition.z, _jointOriginalPos.z, Time.deltaTime * _bobSpeed)
                );
            }
        }
    }
}