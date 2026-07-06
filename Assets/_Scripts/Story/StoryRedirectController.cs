using Cysharp.Threading.Tasks;
using Gameplay.General;
using Gameplay.Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Story
{
    public class StoryRedirectController : MonoBehaviour
    {
        [SerializeField] private LevelController _levelController;
        [SerializeField] private GameFlowController _gameFlowController;
        [SerializeField] private int _levelForVictory = 2;
        [SerializeField] private string _storyLink = "https://en.wikipedia.org/wiki/Cat";

        public void Redirect()
        {
            RedirectAsync().Forget();
        }

        private void Awake()
        {
            _levelController.OnLevelChanged += HandleVictory;
        }

        private async UniTask RedirectAsync()
        {
            _gameFlowController.EnterRedirect();

            Application.OpenURL(_storyLink);

            _gameFlowController.EnterPostStory();
        }

        private void HandleVictory(int currentLevel)
        {
            if (currentLevel == _levelForVictory)
            {
                RedirectAsync().Forget();
            }
        }

        private void OnDestroy()
        {
            _levelController.OnLevelChanged -= HandleVictory;
        }
    }
}
