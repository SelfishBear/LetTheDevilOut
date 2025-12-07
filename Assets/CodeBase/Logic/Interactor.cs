using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.UI.HUD;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeBase.Logic
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _interactionDistance;
        [SerializeField] private LayerMask _interactionLayer;
        private HUDPrefab _hudPrefab;
        private IInputService _inputService;
        private InputSystem_Actions _inputActions;


        public void Construct(HUDPrefab hudPrefab)
        {
            _hudPrefab = hudPrefab;
        }

        private void Start()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _inputActions = _inputService.GetPlayerInputActions();
            _inputActions.Player.Interact.performed += TryInteract;
        }

        private void OnDestroy()
        {
            if (_inputActions != null)
            {
                _inputActions.Player.Interact.performed -= TryInteract;
            }
        }

        private void OnDrawGizmos()
        {
            Debug.DrawRay(_camera.transform.position, _camera.transform.forward * _interactionDistance, Color.green);
        }

        private void Update()
        {
            CheckInteraction();
        }

        private void CheckInteraction()
        {
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hitInfo,
                    _interactionDistance, _interactionLayer))
            {
                if (hitInfo.transform.TryGetComponent(out IInteractable interactable))
                {
                    string keyName = _inputActions.Player.Interact.GetBindingDisplayString();
                    _hudPrefab.InteractionUI.Show(keyName);
                }
            }
            else
            {
                _hudPrefab.InteractionUI.Hide();
            }
        }

        private void TryInteract(InputAction.CallbackContext obj)
        {
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hitInfo,
                    _interactionDistance, _interactionLayer))
            {
                if (hitInfo.transform.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact(gameObject);
                }
            }
        }
    }
}