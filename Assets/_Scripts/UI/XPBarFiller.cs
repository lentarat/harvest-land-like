using Cysharp.Threading.Tasks;
using Gameplay.Level;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class XPBarFiller : MonoBehaviour
    {
        [SerializeField] private Slider _xpBar;
        [SerializeField] private LevelController _levelController;
        [SerializeField] private float _delay;
        [SerializeField] private float _fillDuration;

        private float _lastRequiredXP;
        private CancellationTokenSource _cts;

        private void Awake()
        {
            _levelController.OnCurrentXPChanged += HandleCurrentXPChanged;
        }

        private void HandleCurrentXPChanged(float currentXP, float previousXP, float requiredXP)
        {
            float targetValue = (currentXP - previousXP) / (requiredXP - previousXP);

            _cts?.Cancel();
            _cts = new CancellationTokenSource();

            AnimateFillAsync(targetValue, _cts.Token).Forget();
        }
        private async UniTaskVoid AnimateFillAsync(float targetValue, CancellationToken ct)
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_delay), cancellationToken: ct);

                float start = _xpBar.value;
                float t = 0f;

                while (t < 1f)
                {
                    ct.ThrowIfCancellationRequested();

                    t += Time.deltaTime / _fillDuration;
                    _xpBar.value = Mathf.Lerp(start, targetValue, t);

                    await UniTask.Yield(PlayerLoopTiming.Update, ct);
                }

                _xpBar.value = targetValue;
            }
            catch (OperationCanceledException)
            {
            }
        }

        private void OnDestroy()
        {
            _levelController.OnCurrentXPChanged -= HandleCurrentXPChanged;
        }
    }
}
