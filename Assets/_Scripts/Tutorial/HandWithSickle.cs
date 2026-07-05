using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Tutorial
{
    public class HandWithSickle : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Image _handWithSickleImage;
        [SerializeField] private Image _sickleButtonImage;
        [SerializeField] private RectTransform[] _pointsRectTransforms;
        [SerializeField] private SickleController _sickleController;
        [SerializeField] private TutorialText _tutorialText;

        [Header("Movement")]
        [SerializeField] private float _pathDuration = 1.2f;

        [Header("Fade")]
        [SerializeField] private float _fadeTime = 2f;
        [SerializeField] private float _idleTimeToShow = 5f;

        [Header("Button Highlight")]
        [SerializeField] private Color _highlightColor = new(0.7f, 1f, 0.7f, 1f);
        [SerializeField] private float _buttonPulseTime = 0.6f;

        private float _idleTimer;
        private bool _isVisible;
        private Tween _moveTween;
        private Tween _fadeTween;

        private Tween _buttonTween;
        private Color _defaultButtonColor;

        private float _lastX;
        private float _lastSign;

        private const float FacingEpsilon = 0.0001f;

        private void Awake()
        {
            _defaultButtonColor = _sickleButtonImage.color;
        }

        private void Start()
        {
            StartAnimation();
            HideInstant();
        }

        private void HideInstant()
        {
            _isVisible = false;

            _handWithSickleImage.color = new Color(
                _handWithSickleImage.color.r,
                _handWithSickleImage.color.g,
                _handWithSickleImage.color.b,
                0f);
        }

        private void StartAnimation()
        {
            Vector3[] path = new Vector3[_pointsRectTransforms.Length];

            for (int i = 0; i < _pointsRectTransforms.Length; i++)
            {
                path[i] = _pointsRectTransforms[i].anchoredPosition;
            }

            _handWithSickleImage.rectTransform.DOAnchorPos(path[0], 0f);

            _handWithSickleImage.rectTransform
                .DOLocalPath(path, _pathDuration, PathType.CatmullRom)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo)
                .OnUpdate(SetFacing);
        }

        private void SetFacing()
        {
            float deltaX = _handWithSickleImage.rectTransform.anchoredPosition.x - _lastX;
            float sign = Mathf.Abs(deltaX) < FacingEpsilon ? _lastSign : Mathf.Sign(deltaX);

            if (_lastSign != sign)
            {
                _handWithSickleImage.rectTransform.Rotate(new Vector3(0f, 180f, 0f));
            }
            _lastSign = sign;
            _lastX = _handWithSickleImage.rectTransform.anchoredPosition.x;
        }

        private void Update()
        {
            UpdateIdle();
        }

        private void UpdateIdle()
        {
            if (_sickleController.IsDragging)
            {
                ResetIdle();
                Hide();
                HideButtonHint();

                return;
            }

            _idleTimer += Time.deltaTime;

            if (_idleTimer >= _idleTimeToShow)
            {
                Show();
                ShowButtonHint();
            }
        }

        private void ResetIdle()
        {
            _idleTimer = 0f;
        }

        private void Hide()
        {
            if (!_isVisible)
                return;

            _isVisible = false;

            _fadeTween?.Kill();

            _fadeTween = _handWithSickleImage
                .DOFade(0f, _fadeTime);

            _tutorialText.Show("Harvest crops!");
        }

        private void Show()
        {
            if (_isVisible)
                return;

            _isVisible = true;

            _fadeTween?.Kill();

            _fadeTween = _handWithSickleImage
                .DOFade(1f, _fadeTime)
                .SetEase(Ease.InOutSine);

            _tutorialText.Show("Get the sickle!");
        }

        private void ShowButtonHint()
        {
            if (_buttonTween != null && _buttonTween.IsActive())
                return;

            _buttonTween = DOTween.Sequence()
                .Append(_sickleButtonImage.DOColor(_highlightColor, _buttonPulseTime))
                .Append(_sickleButtonImage.DOColor(_defaultButtonColor, _buttonPulseTime))
                .SetLoops(-1);
        }

        private void HideButtonHint()
        {
            _buttonTween?.Kill();
            _buttonTween = null;

            _sickleButtonImage.color = _defaultButtonColor;
        }

        private void OnDestroy()
        {
            _moveTween?.Kill();
            _fadeTween?.Kill();
            _buttonTween?.Kill();
        }
    }
}
