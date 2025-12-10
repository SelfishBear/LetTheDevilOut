using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.StaticData;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;

namespace CodeBase.Logic
{
    public class PlayerFlashlight : MonoBehaviour
    {
        [Header("On Settings")] [SerializeField]
        private float _onRange = 28f;

        [SerializeField] private float _onSpotAngle = 40f;

        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private Light _spotLight;

        private bool _isOn;

        private float _baseRange;
        private float _baseSpotAngle;
        private float _stepTime;
        private IInputService _inputService;
        private InputSystem_Actions _inputActions;

        public void Construct(HeroStaticData heroStaticData)
        {
            if (heroStaticData == null) throw new ArgumentNullException(nameof(heroStaticData));
            _baseRange = heroStaticData.BaseRange;
            _baseSpotAngle = heroStaticData.BaseSpotAngle;
            _stepTime = heroStaticData.BaseStepTime;
        }

        private void Start()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _inputActions = _inputService.GetPlayerInputActions();
            _inputActions.Player.Flashlight.performed += FlashlightToggle;
        }

        private void OnDestroy()
        {
            if (_inputActions != null)
                _inputActions.Player.Flashlight.performed -= FlashlightToggle;
        }

        public void ZoomFlashlight(bool isZoomed)
        {
            if (!isZoomed)
            {
                _spotLight.range = Mathf.Lerp(_spotLight.range, _baseRange, Time.deltaTime * _stepTime);
                _spotLight.spotAngle = Mathf.Lerp(_spotLight.spotAngle, _baseSpotAngle, Time.deltaTime * _stepTime);
            }
            else
            {
                _spotLight.range = Mathf.Lerp(_spotLight.range, _onRange, Time.deltaTime * _stepTime);
                _spotLight.spotAngle = Mathf.Lerp(_spotLight.spotAngle, _onSpotAngle, Time.deltaTime * _stepTime);
            }
        }

        private void FlashlightToggle(InputAction.CallbackContext obj)
        {
            _isOn = !_isOn;
            _spotLight.enabled = _isOn;
        }
    }
}