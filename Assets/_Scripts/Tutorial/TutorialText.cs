using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Gameplay.Tutorial
{
    public class TutorialText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        [Header("Shake")]
        [SerializeField] private float _shakeStrength = 15f;
        [SerializeField] private float _shakeDuration = 0.45f;
        [SerializeField] private float _shakeInterval = 2f;

        [Header("Fade")]
        [SerializeField] private float _fadeDuration = 0.3f;

        private Sequence _shakeSequence;

        public void Show(string message)
        {
            if (_text.text == message && _text.color.a > 0.99f)
                return;

            _text.text = message;

            _text.DOKill();
            _shakeSequence?.Kill();

            _text.DOFade(1f, _fadeDuration);

            StartShake();
        }

        public void Hide()
        {
            _text.DOKill();
            _shakeSequence?.Kill();

            _text.DOFade(0f, _fadeDuration);
        }

        private void Awake()
        {
            HideInstant();
        }

        private void HideInstant()
        {
            Color c = _text.color;
            c.a = 0;
            _text.color = c;
        }

        private void StartShake()
        {
            _shakeSequence = DOTween.Sequence()
                .AppendInterval(_shakeInterval)
                .Append(_text.rectTransform.DOShakeAnchorPos(
                    _shakeDuration,
                    _shakeStrength,
                    vibrato: 20,
                    randomness: 45,
                    snapping: false,
                    fadeOut: true))
                .SetLoops(-1);
        }

        private void OnDestroy()
        {
            _text.DOKill();
            _shakeSequence?.Kill();
        }
    }
}

