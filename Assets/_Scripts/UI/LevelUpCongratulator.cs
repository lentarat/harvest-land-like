using DG.Tweening;
using Gameplay.Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpCongratulator : MonoBehaviour
{
    [SerializeField] private LevelController _levelController;
    [SerializeField] private Image _levelUpImage;
    [SerializeField] private float _fadeDuration;
    [SerializeField] private float _scaleDuration;
    [SerializeField] private float _holdDuration;
    [SerializeField] private float _maxScale;

    private Sequence _sequence;

    private void Awake()
    {
        _levelController.OnLevelChanged += HandleLevelChanged;
    }

    private void HandleLevelChanged(int level)
    {
        Show();
    }

    private void Show()
    {
        _sequence?.Kill();

        SetAlpha(0f);

        _sequence = DOTween.Sequence();

        _sequence.Append(_levelUpImage.DOFade(1f, _fadeDuration));
        _sequence.Append(_levelUpImage.transform.DOScale(_maxScale, _scaleDuration));
        _sequence.AppendInterval(_holdDuration);
        _sequence.Append(_levelUpImage.transform.DOScale(1f, _scaleDuration));
        _sequence.Append(_levelUpImage.DOFade(0f, _fadeDuration));
    }

    private void SetAlpha(float a)
    {
        Color color = _levelUpImage.color;
        color.a = a;
        _levelUpImage.color = color;
    }

    private void OnDestroy()
    {
        _levelController.OnLevelChanged -= HandleLevelChanged;
    }
}
