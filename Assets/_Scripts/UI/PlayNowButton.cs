using Gameplay.Story;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Gameplay.UI
{
    public class PlayNowButton : MonoBehaviour
    {
        [SerializeField] private StoryRedirectController _storyRedirectController;
        [SerializeField] private Button _button;
        [SerializeField] private float _maxScale;
        [SerializeField] private float _duration;
        
        private Sequence _sequence;

        private void Awake()
        {
            _button.onClick.AddListener(() => { Redirect(); });
            Animate();
        }

        private void Redirect()
        {
            _storyRedirectController.Redirect();
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
