using UnityEngine;

namespace CodeBase.Logic
{
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private KeyType _requiredKeyType;
        [field: SerializeField] public bool Interactable { get; set; } = true;
        [field: SerializeField] public string InteractionMessage { get; set; }
        
        public void Interact(GameObject interactor)
        {
            if (!Interactable) return;
            
            if (interactor.TryGetComponent(out Inventory inventory))
            {
                if (inventory.CollectedKeys.ContainsValue(_requiredKeyType))
                {
                    print("Door interacted with.");
                    RotateDoor();
                }
                else if (_requiredKeyType == KeyType.None)
                {
                    RotateDoor();
                }
            }
        }

        private void RotateDoor()
        {
            bool isOpen = transform.rotation.eulerAngles.y > 0;
            float targetAngle = isOpen ? 0f : 90f;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = targetRotation;
        }
    }
}