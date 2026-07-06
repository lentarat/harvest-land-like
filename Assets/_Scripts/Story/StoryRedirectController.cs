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
        [SerializeField] private int _inputBanDelayMS = 500;
        [SerializeField] private string _storyLink = "https://en.wikipedia.org/wiki/Cat";

        public void Redirect()
        {
            RedirectAsync().Forget();
        }

        private void Awake()
        {
            _levelController.OnLevelChanged += HandleLevelChanged;
        }

        private async UniTask RedirectAsync()
        {
            _gameFlowController.EnterRedirect();

            Application.OpenURL(_storyLink);

            _gameFlowController.EnterPostStory();
        }

        private void HandleLevelChanged(int currentLevel)
        {
            if (currentLevel == 2)
            {
                RedirectAsync().Forget();
            }
        }

        private void OnDestroy()
        {
            _levelController.OnLevelChanged -= HandleLevelChanged;
        }
    }
}
