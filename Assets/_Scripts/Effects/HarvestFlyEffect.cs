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

        [Header("Jump")]
        [SerializeField] private float _jumpHeight = 0.5f;
        [SerializeField] private float _jumpPower = 0.5f;
        [SerializeField] private int _jumpCount = 1;
        [SerializeField] private float _jumpDuration = 0.25f;

        [Header("Fly")]
        [SerializeField] private float _flyDuration = 0.4f;
        [SerializeField] private Ease _flyEase = Ease.InQuad;

        public void Play(Sprite sprite, Vector3 startPosition, Vector3 targetPosition, Action onComplete)
        {
            transform.position = startPosition;
            _spriteRenderer.sprite = sprite;

            Sequence sequence = DOTween.Sequence();

            sequence.Append(
                transform.DOJump(
                    startPosition + Vector3.up * _jumpHeight,
                    _jumpPower,
                    _jumpCount,
                    _jumpDuration));

            sequence.Append(
                transform.DOMove(targetPosition, _flyDuration)
                .SetEase(_flyEase));

            sequence.OnComplete(() =>
            {
                onComplete?.Invoke();
                Destroy(gameObject);
            });
        }
    }
}
