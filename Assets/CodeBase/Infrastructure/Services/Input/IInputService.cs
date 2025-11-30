using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeBase.Infrastructure.Services.Input
{
  public interface IInputService : IService
  {
    Vector2 MoveDirection { get; }
    Vector2 LookDirection { get; }
    void EnableActionMap(ActionMapType actionMap);
    void DisableActionMap(ActionMapType actionMap);
    void DisableAllActionMaps();
    InputSystem_Actions GetPlayerInputActions();
    InputActionMap GetActionMap(ActionMapType actionMap);
  }
}