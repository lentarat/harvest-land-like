using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Gameplay.Effects
{
    public class XPFlyEffect : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;

        [Header("Jump")]
        [SerializeField] private float _jumpHeight = 80f;
        [SerializeField] private float _jumpDuration = 0.2f;

        [Header("Fly")]
        [SerializeField] private float _flyDuration = 0.5f;
        [SerializeField] private Ease _flyEase = Ease.InCirc;


        public void Play(Vector3 startScreenPosition, Vector3 targetScreenPosition, Action onComplete)
        {
            _rectTransform.position = startScreenPosition;

            Sequence sequence = DOTween.Sequence();

            sequence.Append(
                _rectTransform.DOAnchorPos(
                    _rectTransform.anchoredPosition + Vector2.up * _jumpHeight,
                    _jumpDuration));

            sequence.Append(
                _rectTransform.DOMove(targetScreenPosition, _flyDuration)
                    .SetEase(Ease.InCirc));

            sequence.OnComplete(() =>
            {
                onComplete?.Invoke();
                Destroy(gameObject);
            });
        }
    }
}
