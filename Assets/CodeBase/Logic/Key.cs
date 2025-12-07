using UnityEngine;

namespace CodeBase.Logic
{
    public class Key : MonoBehaviour, IInteractable
    {
        [SerializeField] private KeyType _keyType;
        [field: SerializeField] public bool Interactable { get; set; } = true;
        [field: SerializeField] public string InteractionMessage { get; set; }

        public void Interact(GameObject interactor)
        {
            if (interactor.TryGetComponent(out Inventory inventory))
            {
                inventory.AddKey(_keyType);
                Destroy(gameObject);
            }
        }
    }
}