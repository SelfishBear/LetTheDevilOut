using UnityEngine;
using UnityEngine.Splines;

namespace CodeBase.Logic
{
    public class CameraSplineMover : MonoBehaviour
    {
        [Header("Spline Settings")]
        [SerializeField] private SplineContainer _splineContainer;
        [SerializeField] private int _splineIndex = 0;
        
        [Header("Movement Settings")]
        [SerializeField] private bool _autoMove = true;
        [SerializeField] private float _speed = 1f;
        [SerializeField] private bool _loop = false;
        [SerializeField] private bool _pingPong = false;
        
        [Header("Rotation Settings")]
        [SerializeField] private bool _rotateWithSpline = true;
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private Vector3 _rotationOffset = Vector3.zero;
        
        [Header("Look At Settings")]
        [SerializeField] private bool _useLookAt = false;
        [SerializeField] private Transform _lookAtTarget;
        
        private float _currentProgress = 0f;
        private bool _movingForward = true;
        private Spline _spline;

        private void Start()
        {
            if (_splineContainer != null && _splineContainer.Splines.Count > _splineIndex)
            {
                _spline = _splineContainer.Splines[_splineIndex];
            }
            else
            {
                Debug.LogError("SplineContainer is not assigned or spline index is out of range!");
            }
        }

        private void Update()
        {
            if (_spline == null || !_autoMove) return;

            UpdateMovement();
            UpdateRotation();
        }

        private void UpdateMovement()
        {
            float progressDelta = (_speed / _spline.GetLength()) * Time.deltaTime;

            if (_movingForward)
            {
                _currentProgress += progressDelta;
                
                if (_currentProgress >= 1f)
                {
                    if (_loop)
                    {
                        _currentProgress = 0f;
                    }
                    else if (_pingPong)
                    {
                        _currentProgress = 1f;
                        _movingForward = false;
                    }
                    else
                    {
                        _currentProgress = 1f;
                        _autoMove = false;
                    }
                }
            }
            else
            {
                _currentProgress -= progressDelta;
                
                if (_currentProgress <= 0f)
                {
                    _currentProgress = 0f;
                    _movingForward = true;
                }
            }

            Vector3 position = _spline.EvaluatePosition(_currentProgress);
            transform.position = _splineContainer.transform.TransformPoint(position);
        }

        private void UpdateRotation()
        {
            if (_useLookAt && _lookAtTarget != null)
            {
                Vector3 direction = _lookAtTarget.position - transform.position;
                if (direction != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
                }
            }
            else if (_rotateWithSpline)
            {
                Vector3 tangent = _spline.EvaluateTangent(_currentProgress);
                Vector3 up = _spline.EvaluateUpVector(_currentProgress);
                
                tangent = _splineContainer.transform.TransformDirection(tangent);
                up = _splineContainer.transform.TransformDirection(up);

                if (tangent != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(tangent, up);
                    targetRotation *= Quaternion.Euler(_rotationOffset);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
                }
            }
        }

        public void Play()
        {
            _autoMove = true;
            _movingForward = true;
        }

        public void Pause()
        {
            _autoMove = false;
        }

        public void Stop()
        {
            _autoMove = false;
            _currentProgress = 0f;
        }

        public void SetProgress(float progress)
        {
            _currentProgress = Mathf.Clamp01(progress);
            UpdateMovement();
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

        public float GetProgress()
        {
            return _currentProgress;
        }

        public void Reverse()
        {
            _movingForward = !_movingForward;
        }

        private void OnDrawGizmos()
        {
            if (_splineContainer != null && _splineContainer.Splines.Count > _splineIndex)
            {
                // Показываем текущую позицию на сплайне
                Gizmos.color = Color.green;
                Vector3 position = _splineContainer.Splines[_splineIndex].EvaluatePosition(_currentProgress);
                position = _splineContainer.transform.TransformPoint(position);
                Gizmos.DrawWireSphere(position, 0.5f);
            }
        }
    }
}