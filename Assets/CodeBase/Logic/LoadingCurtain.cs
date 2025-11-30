using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Logic
{
  public class LoadingCurtain : MonoBehaviour
  {
    public CanvasGroup Curtain;

    private void Awake()
    {
      DontDestroyOnLoad(this);
    }

    public void Show(Action onComplete = null)
    {
      gameObject.SetActive(true);
      StartCoroutine(DoFadeOut(onComplete));
    }

    private IEnumerator DoFadeOut(Action onComplete = null)
    {
      while (Curtain.alpha < 1)
      {
        Curtain.alpha += 0.03f;
        yield return new WaitForSeconds(0.015f);
      }
      
      onComplete?.Invoke();
    }

    public void Hide() => StartCoroutine(DoFadeIn());
    
    private IEnumerator DoFadeIn()
    {
      while (Curtain.alpha > 0)
      {
        Curtain.alpha -= 0.03f;
        yield return new WaitForSeconds(0.03f);
      }
    }
  }
}