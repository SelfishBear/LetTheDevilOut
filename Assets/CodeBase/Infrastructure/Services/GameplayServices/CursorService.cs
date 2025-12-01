using UnityEngine;

namespace CodeBase.Infrastructure.Services.GameplayServices
{
    public class CursorService : ICursorService
    {
        public void ChangeCursorState(bool isVisible, bool isLocked)
        {
            Cursor.visible = isVisible;
            Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
            Debug.Log("Cursor State: " + Cursor.visible + " | Cursor Lock: " + Cursor.lockState);
        }
    }
}