using Gameplay.Story;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Gameplay.UI
{
    public class RedirectButton : PulsatingButton
    {
        [SerializeField] private StoryRedirectController _storyRedirectController;
        
        private void Awake()
        {
            base.Init();
            Button.onClick.AddListener(() => { Redirect(); });
        }

        private void Redirect()
        {
            _storyRedirectController.Redirect();
        }
    } 
}
