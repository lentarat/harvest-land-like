using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Gameplay.Tutorial
{
    public class HandWithSickle : MonoBehaviour
    {
        [SerializeField] private RectTransform _handRectTransform;
        [SerializeField] private RectTransform[] _pointsRectTransforms;
        [SerializeField] private float _duration = 1.2f;

        private float _lastX;
        private float _lastSign;

        private void Start()
        {
            StartAnimation();
        }

        private void StartAnimation()
        {
            Vector3[] path = new Vector3[_pointsRectTransforms.Length];

            for (int i = 0; i < _pointsRectTransforms.Length; i++)
            {
                path[i] = _pointsRectTransforms[i].anchoredPosition;
            }

            _handRectTransform.DOAnchorPos(path[0], 0f);

            _handRectTransform
                .DOLocalPath(path, _duration, PathType.CatmullRom)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo)
                .OnUpdate(SetFacing);
        }

        private void SetFacing()
        {
            if (_lastSign != Mathf.Sign(_handRectTransform.anchoredPosition.x - _lastX))
            {
                _handRectTransform.Rotate(new Vector3(0f, 180f, 0f));
            }
            _lastSign = Mathf.Sign(_handRectTransform.anchoredPosition.x - _lastX);
            _lastX = _handRectTransform.anchoredPosition.x;
        }
    }
}
