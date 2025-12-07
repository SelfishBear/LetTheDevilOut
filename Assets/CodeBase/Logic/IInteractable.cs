using UnityEngine;

namespace CodeBase.Logic
{
    public interface IInteractable
    {
        bool Interactable { get; set; }
        void Interact(GameObject interactor);

        string InteractionMessage { get; set; }
    }
}