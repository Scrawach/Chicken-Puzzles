using System;
using System.Collections;
using UnityEngine;

namespace UI.Effects
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeElement : MonoBehaviour
    {
        [SerializeField, Range(0f, 2f)] private float _fadeTime;

        protected CanvasGroup Canvas;
        private Coroutine _fading;

        public event Action FadeInEnded;
        public event Action FadeOutEnded;

        protected virtual void Awake() => 
            Canvas = GetComponent<CanvasGroup>();

        public void Show()
        {
            Reset();
            _fading = StartCoroutine(FadeIn());
        }

        public void Hide()
        {
            Reset();
            _fading = StartCoroutine(FadeOut(Canvas.alpha));
        }

        public void HideWith(float alpha)
        {
            Reset();
            _fading = StartCoroutine(FadeOut(alpha));
        }

        private void Reset()
        {
            if (_fading != null)
                StopCoroutine(_fading);

            _fading = null;
        }

        private IEnumerator FadeOut(float startAlpha)
        {
            var speed = 1f / _fadeTime;
            yield return Smooth.Change(startAlpha, 0f, speed, Fading);
            FadeOutEnded?.Invoke();
        }

        private IEnumerator FadeIn()
        {
            var speed = 1f / _fadeTime;
            yield return Smooth.Change(0f, 1f, speed, Fading);
            FadeInEnded?.Invoke();
        }

        private void Fading(float start, float end, float t) => 
            Canvas.alpha = Mathf.Lerp(start, end, t);
    }
}