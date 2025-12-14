using System;
using UnityEngine;

namespace CodeBase.Logic
{
    public class Key : MonoBehaviour, IInteractable
    {
        [SerializeField] private KeyType _keyType;
        [field: SerializeField] public bool Interactable { get; set; } = true;
        [field: SerializeField] public string InteractionMessage { get; set; }
        
        [SerializeField] private GameObject _mesh;
        
        public event Action OnInteraction;

        public void Interact(GameObject interactor)
        {
            if (interactor.TryGetComponent(out Inventory inventory))
            {
                OnInteraction?.Invoke();
                inventory.AddKey(_keyType);
                _mesh.SetActive(false);
                Interactable = false;
            }
        }
    }
}