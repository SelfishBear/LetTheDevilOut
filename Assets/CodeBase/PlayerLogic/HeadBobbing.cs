using System;
using System.Collections;
// using DG.Tweening;
using UnityEngine;

namespace CodeBase.PlayerLogic
{

	[DisallowMultipleComponent]
	public class HeadBobbing : MonoBehaviour
	{
		// 	public event Action OnStepDown;
		//
		// 	[Header("Walk Settings")]
		// 	[SerializeField] private float _walkBobVerticalFrequency = 14f;
		// 	[SerializeField] private float _walkBobVerticalAmplitude = 0.05f;
		// 	[SerializeField] private float _walkBobHorizontalFrequency = 10f;
		// 	[SerializeField] private float _walkBobHorizontalAmplitude = 0.05f;
		//
		// 	[Header("Sprint Settings")]
		// 	[SerializeField] private float _sprintBobVerticalFrequency = 18f;
		// 	[SerializeField] private float _sprintBobVerticalAmplitude = 0.1f;
		// 	[SerializeField] private float _sprintBobHorizontalFrequency = 14f;
		// 	[SerializeField] private float _sprintBobHorizontalAmplitude = 0.1f;
		//
		// 	[Header("Climbing Settings")]
		// 	[SerializeField] private float _climbUpAmplitude = 0.3f;
		// 	[SerializeField] private float _climbDownAmplitude = 0.1f;
		// 	[SerializeField] private float _climbUpDuration = 0.15f;
		// 	[SerializeField] private float _climbDownDuration = 0.1f;
		//
		// 	[Header("General Settings")]
		// 	[SerializeField] private float _stopBobbingTimeInSeconds = .25f;
		// 	[SerializeField] private float _bobbingSmoothTime = 12.5f;
		// 	[SerializeField] private float _bobStartLerpSpeed = 5f;
		// 	[SerializeField] private float _bobTransitionSpeed = 5f;
		//
		// 	[Space(10)]
		// 	[SerializeField] private bool _headBobbingEnabled = true;
		//
		// 	[SerializeField] private Transform _cameraTransform;
		// 	[SerializeField] private FirstPersonController _firstPersonController;
		// 	[SerializeField] private PlayerDeath _playerDeath;
		// 	[SerializeField] private bool _canBobbing;
		//
		// 	private float _defaultYPosition;
		// 	private float _defaultXPosition;
		// 	private float _timerX;
		// 	private float _timerY;
		// 	private bool _isMoving;
		// 	private bool _isSprinting;
		// 	private float _bobLerpFactor;
		//
		// 	private float _targetFrequencyY;
		// 	private float _targetFrequencyX;
		// 	private float _targetAmplitudeY;
		// 	private float _targetAmplitudeX;
		// 	private float _lastSinY;
		//
		//
		// 	private float BobbingFrequencyY => Mathf.Lerp(_targetFrequencyY,
		// 		_isSprinting ? _sprintBobVerticalFrequency : _walkBobVerticalFrequency,
		// 		Time.deltaTime * _bobTransitionSpeed);
		//
		// 	private float BobbingFrequencyX => Mathf.Lerp(_targetFrequencyX,
		// 		_isSprinting ? _sprintBobHorizontalFrequency : _walkBobHorizontalFrequency,
		// 		Time.deltaTime * _bobTransitionSpeed);
		//
		// 	private float BobbingAmplitudeY => Mathf.Lerp(_targetAmplitudeY,
		// 		_isSprinting ? _sprintBobVerticalAmplitude : _walkBobVerticalAmplitude,
		// 		Time.deltaTime * _bobTransitionSpeed);
		//
		// 	private float BobbingAmplitudeX => Mathf.Lerp(_targetAmplitudeX,
		// 		_isSprinting ? _sprintBobHorizontalAmplitude : _walkBobHorizontalAmplitude,
		// 		Time.deltaTime * _bobTransitionSpeed);
		//
		// 	private Coroutine _bobbingRoutine;
		// 	private Tween _resetCameraPositionTween;
		//
		// 	private void Awake()
		// 	{
		// 		_defaultYPosition = _cameraTransform.localPosition.y;
		// 		_defaultXPosition = _cameraTransform.localPosition.x;
		// 	}
		//
		// 	private void Start()
		// 	{
		// 		_firstPersonController.OnPlayerStateChanged += MoveStateChanged;
		// 		_playerDeath.DeathHappened += OnDeathStateChanged;
		// 	}
		//
		// 	private void OnDestroy()
		// 	{
		// 		_firstPersonController.OnPlayerStateChanged -= MoveStateChanged;
		// 		_playerDeath.DeathHappened -= OnDeathStateChanged;
		// 	}
		//
		// 	public void Stop() =>
		// 		_canBobbing = false;
		//
		// 	public void Go() =>
		// 		_canBobbing = true;
		//
		// 	private void SetMoving(bool isMoving, bool isSprinting = false)
		// 	{
		// 		if (_isMoving == isMoving && _isSprinting == isSprinting)
		// 			return;
		//
		// 		_isMoving = isMoving;
		// 		_isSprinting = isSprinting;
		//
		// 		_targetFrequencyY = _isSprinting ? _sprintBobVerticalFrequency : _walkBobVerticalFrequency;
		// 		_targetFrequencyX = _isSprinting ? _sprintBobHorizontalFrequency : _walkBobHorizontalFrequency;
		// 		_targetAmplitudeY = _isSprinting ? _sprintBobVerticalAmplitude : _walkBobVerticalAmplitude;
		// 		_targetAmplitudeX = _isSprinting ? _sprintBobHorizontalAmplitude : _walkBobHorizontalAmplitude;
		//
		// 		if (!isMoving)
		// 		{
		// 			ResetCameraPosition();
		// 			StopBobbing();
		// 		}
		// 		else
		// 		{
		// 			TryBreakCameraPositionTween();
		// 			StopBobbing();
		// 			_bobLerpFactor = 0f;
		// 			_bobbingRoutine ??= StartCoroutine(HeadBobRoutine());
		// 		}
		// 	}
		//
		// 	private void ResetCameraPosition()
		// 	{
		// 		_resetCameraPositionTween ??= _cameraTransform.DOLocalMoveY(_defaultYPosition, _stopBobbingTimeInSeconds)
		// 			.SetEase(Ease.InOutSine).OnComplete(() => _resetCameraPositionTween = null);
		// 	}
		//
		// 	private void TryBreakCameraPositionTween()
		// 	{
		// 		if (_resetCameraPositionTween == null) return;
		// 		_resetCameraPositionTween?.Kill();
		// 		_resetCameraPositionTween = null;
		// 	}
		//
		// 	public void StopBobbing()
		// 	{
		// 		if (_bobbingRoutine == null) return;
		// 		StopCoroutine(_bobbingRoutine);
		// 		_bobbingRoutine = null;
		// 	}
		//
		// 	private void HandleHeadBob()
		// 	{
		// 		if (!_headBobbingEnabled || !_isMoving || !_canBobbing)
		// 			return;
		//
		// 		_bobLerpFactor = Mathf.MoveTowards(_bobLerpFactor, 1f, Time.deltaTime * _bobStartLerpSpeed);
		//
		// 		_timerX += Time.deltaTime * BobbingFrequencyX;
		// 		_timerY += Time.deltaTime * BobbingFrequencyY;
		// 		float sinY = Mathf.Sin(_timerY);
		// 		Vector3 originalPosition = _cameraTransform.localPosition;
		// 		Vector3 targetPosition = originalPosition;
		// 		targetPosition.y = _defaultYPosition + Mathf.Sin(_timerY) * BobbingAmplitudeY;
		// 		targetPosition.x = _defaultXPosition + Mathf.Cos(_timerX / 2) * BobbingAmplitudeX;
		//
		// 		_cameraTransform.localPosition = Vector3.MoveTowards(
		// 			_cameraTransform.localPosition, targetPosition, Time.deltaTime * _bobbingSmoothTime);
		//
		// 		if (_lastSinY > -0.99f && sinY <= -0.99f)
		// 			OnStepDown?.Invoke();
		//
		// 		_lastSinY = sinY;
		// 	}
		//
		// 	private IEnumerator HeadBobRoutine()
		// 	{
		// 		while (_isMoving)
		// 		{
		// 			HandleHeadBob();
		// 			yield return null;
		// 		}
		// 	}
		//
		// 	private void MoveStateChanged(bool isMoving, bool isSprinting)
		// 	{
		// 		SetMoving(isMoving, isSprinting);
		// 	}
		//
		// 	private void OnDeathStateChanged(bool isMoving, bool isSprinting)
		// 		=> SetMoving(isMoving, isSprinting);
		//
		// }
	}
}