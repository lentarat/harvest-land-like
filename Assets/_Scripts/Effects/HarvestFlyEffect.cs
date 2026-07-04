using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Gameplay.Effects
{
    public class HarvestFlyEffect : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void Play(Sprite sprite, Vector3 startPosition, Vector3 targetPosition, Action onComplete)
        {
            transform.position = startPosition;
            _spriteRenderer.sprite = sprite;

            Sequence sequence = DOTween.Sequence();

            sequence.Append(transform.DOJump(startPosition + Vector3.up * 0.5f, 0.5f, 1, 0.25f));

            sequence.Append(transform.DOMove(targetPosition, 0.4f).SetEase(Ease.InQuad));

            sequence.OnComplete(() =>
            {
                onComplete?.Invoke();
                Destroy(gameObject);
            });
        }
    }
}
