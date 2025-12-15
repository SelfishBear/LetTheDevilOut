using TMPro;
using UnityEngine;

namespace CodeBase.UI.HUD
{
    public class InteractionUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _interactionText;
        [SerializeField] private CanvasGroup _canvasGroup;
		
        public void Show(string interactionMessage)
        {
            _interactionText.text = $"{interactionMessage} - INTERACT";
            _canvasGroup.alpha = 1;
            _canvasGroup.blocksRaycasts = true;
        }
		
        public void Hide()
        {
            _interactionText.text = "";
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}