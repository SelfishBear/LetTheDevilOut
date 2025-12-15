using System;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.UI
{
    public class SelfInvokeCanvasGroup : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private void Start()
        {
            _canvasGroup.DOFade(1f, 2f);
        }
    }
}