using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class PulsatingButton : MonoBehaviour
    {
        [SerializeField] private float _maxScale;
        [SerializeField] private float _duration;
        [SerializeField] private Button _button;

        protected Button Button => _button;

        private Sequence _sequence;

        protected void Init()
        {
            Animate();
        }

        private void Animate()
        {
            _sequence = DOTween.Sequence();

            _sequence.Append(transform.DOScale(_maxScale, _duration));
            _sequence.Append(transform.DOScale(1f, _duration));
            _sequence.SetLoops(-1);
        }
    }
}
