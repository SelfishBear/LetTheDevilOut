using System;
using System.Collections;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class TypeWriterText : MonoBehaviour
    {
        [SerializeField] private string _fullText;
        [SerializeField] private float _typeSpeed = 0.05f;
        [SerializeField] private float _fadeOutDuration;
        [SerializeField] private float _timeBeforeFadeOut;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Text _monologueText;
        private IGameStateMachine _gameStateMachine;

        private void Start()
        {
            _gameStateMachine = AllServices.Container.Single<IGameStateMachine>();
            AnimateText();
        }

        private void AnimateText()
        {
            _monologueText.text = string.Empty;
            _monologueText.DOText(_fullText, _fullText.Length * _typeSpeed).SetEase(Ease.Linear).OnComplete(() =>
            {
                StartCoroutine(FadeCoroutine());
            });
        }

        public IEnumerator FadeCoroutine()
        {
            yield return new WaitForSeconds(_timeBeforeFadeOut);
            _canvasGroup.DOFade(0f, _fadeOutDuration).OnComplete(() =>
            {
                _gameStateMachine.Enter<LoadProgressState>();
            });
        }
    }
}