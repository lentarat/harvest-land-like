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

        public void Play(Vector3 startScreenPosition, Vector3 targetScreenPosition, Action onComplete)
        {
            _rectTransform.position = startScreenPosition;

            Sequence sequence = DOTween.Sequence();

            sequence.Append(_rectTransform.DOAnchorPos(_rectTransform.anchoredPosition + Vector2.up * 80f, 0.2f));

            sequence.Append(_rectTransform.DOMove(targetScreenPosition, 0.5f).SetEase(Ease.InCirc));

            sequence.OnComplete(() =>
            {
                onComplete?.Invoke();
                Destroy(gameObject);
            });
        }
    }
}
