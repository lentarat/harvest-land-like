using Cysharp.Threading.Tasks;
using DG.Tweening;
using Gameplay.Level;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class XPBarFiller : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private LevelController _levelController;
        [SerializeField] private Slider _xpBarSlider;
        [SerializeField] private RectTransform _handleRectTransform;

        [Header("Bar Filling")]
        [SerializeField] private float _barFillDelay;
        [SerializeField] private float _barFillDuration;

        [Header("Handle Bouncy")]
        [SerializeField] private float _handleBouncyScale;
        [SerializeField] private float _handleBouncyDuration;

        [Header("Text")]
        [SerializeField] private TextMeshProUGUI _levelText;

        private CancellationTokenSource _cts;
        private Tween _bounceTween;
        private int _animationId;

        private void Awake()
        {
            _levelController.OnLevelChanged += HandleLevelChanged;
            _levelController.OnCurrentXPChanged += HandleCurrentXPChanged;
        }

        private void HandleLevelChanged(int curretLevel)
        {
            _levelText.text = curretLevel.ToString();
        }

        private void HandleCurrentXPChanged(float currentXP, float previousXP, float requiredXP)
        {
            float targetValue = (currentXP - previousXP) / (requiredXP - previousXP);

            _cts?.Cancel();
            _cts = new CancellationTokenSource();

            AnimateFillAsync(targetValue, _cts.Token).Forget();
            AnimateBouncyAsync(_cts.Token).Forget();
        }

        private async UniTask AnimateFillAsync(float targetValue, CancellationToken ct)
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_barFillDelay), cancellationToken: ct);

                float start = _xpBarSlider.value;
                float t = 0f;

                while (t < 1f)
                {
                    ct.ThrowIfCancellationRequested();

                    t += Time.deltaTime / _barFillDuration;
                    _xpBarSlider.value = Mathf.Lerp(start, targetValue, t);

                    await UniTask.Yield(PlayerLoopTiming.Update, ct);
                }

                _xpBarSlider.value = targetValue;
            }
            catch (OperationCanceledException e)
            {
                Debug.Log(e);
            }
        }

        private async UniTask AnimateBouncyAsync(CancellationToken ct)
        {
            int myId = ++_animationId;

            if (myId != _animationId)
                return;

            await UniTask.Delay(TimeSpan.FromSeconds(_barFillDelay), cancellationToken: ct);
            AnimateBouncyHandleEffect();
        }

        private void AnimateBouncyHandleEffect()
        {
            _bounceTween?.Kill();

            _bounceTween = _handleRectTransform.DOScale(_handleBouncyScale, _handleBouncyDuration)
                .SetLoops(2, LoopType.Yoyo);
        }

        private void OnDestroy()
        {
            _levelController.OnCurrentXPChanged -= HandleCurrentXPChanged;
            _levelController.OnLevelChanged -= HandleLevelChanged;
        }
    }
}
