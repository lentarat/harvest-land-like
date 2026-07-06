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
        [SerializeField] private GameFlowController _gameFlowController;
        [SerializeField] private LevelController _levelController;
        [SerializeField] private int _inputBanDelayMS = 500;
        [SerializeField] private string _storyLink = "https://en.wikipedia.org/wiki/Cat";

        private void Awake()
        {
            _levelController.OnStoryRedirect += HandleRedirect;
        }

        private void HandleRedirect()
        {
            _gameFlowController.EnterRedirect();
            RedirectRoutine().Forget();
        }

        private async UniTask RedirectRoutine()
        {
            await UniTask.Delay(_inputBanDelayMS);

            Application.OpenURL(_storyLink);

            await UniTask.Delay(_inputBanDelayMS);

            _levelController.PostStoryImage();
        }

        private void OnDestroy()
        {
            _levelController.OnStoryRedirect -= HandleRedirect;
        }
    }
}
