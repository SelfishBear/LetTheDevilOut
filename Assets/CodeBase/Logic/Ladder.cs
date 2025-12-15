using System;
using System.Collections;
using CodeBase.Infrastructure.Services;
using CodeBase.UI.Services.Factory;
using UnityEngine;

namespace CodeBase.Logic
{
    public class Ladder : MonoBehaviour, IInteractable
    {
        [field: SerializeField] public bool Interactable { get; set; } = true;

        [field: SerializeField] public string InteractionMessage { get; set; }
        private IUIFactory _uiFactory;
        private EndGameUI _endGameUI;

        private IEnumerator Start()
        {
            _uiFactory = AllServices.Container.Single<IUIFactory>();
            yield return new WaitUntil(() => _uiFactory.EndGameUI != null);
            _endGameUI = _uiFactory.EndGameUI;
        }

        public void Interact(GameObject interactor)
        {
            if (!Interactable) return;

            _endGameUI.Show();
        }
    }
}