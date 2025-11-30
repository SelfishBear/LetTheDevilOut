using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeBase.Infrastructure.Services.Input
{
    public class InputService : IInputService, IDisposable
    {
        private readonly InputSystem_Actions _inputActions;
        private const float MinInputThreshold = 0.2f; 

        public Vector2 MoveDirection
        {
            get
            {
                Vector2 input = _inputActions.Player.Move.ReadValue<Vector2>();

                if (input.magnitude < MinInputThreshold)
                    return Vector2.zero;

                return input;
            }
        }
        public Vector2 LookDirection => _inputActions.Player.Look.ReadValue<Vector2>();

        public InputService()
        {
            _inputActions = new InputSystem_Actions();
            _inputActions.Player.Enable();
        }

        public void EnableActionMap(ActionMapType actionMap)
        {
            InputActionMap map = GetActionMap(actionMap);
            Debug.Log(map.ToString());
            if (map != null)
                map.Enable();
        }

        public void DisableActionMap(ActionMapType actionMap)
        {
            InputActionMap map = GetActionMap(actionMap);

            if (map != null)
                map.Disable();
        }

        public void DisableAllActionMaps() =>
            _inputActions.Disable();


        public InputSystem_Actions GetPlayerInputActions() =>
            _inputActions;

        public void Dispose()
        {
            _inputActions.Disable();
            _inputActions.Dispose();
        }

        public InputActionMap GetActionMap(ActionMapType actionMap) =>
            _inputActions.asset.FindActionMap(actionMap.ToString(), throwIfNotFound: false);
    }
}