using Gameplay.General;
using Gameplay.Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Story
{
    public class PostStoryImage : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private GameFlowController _gameFlowController;
        [SerializeField] private CanvasGroup _gameplayCanvasGroup;
        [SerializeField] private CanvasGroup _postStoryCanvasGroup;
        [SerializeField] private LevelController _levelController;
        [SerializeField] private GameObject _congratulationsText;
        [SerializeField] private int _levelForVictory = 2;

        private void Awake()
        {
            _gameFlowController.OnStateChanged += HandleGameStateChanged;
            HandleCongratulationsText();
        }

        private void HandleCongratulationsText()
        {
            int currentLevel = _levelController.CurrentLevel;

            if (currentLevel == _levelForVictory)
            {
                _congratulationsText.SetActive(true);
            }
            else
            {
                _congratulationsText.SetActive(false);
            }
        }

        private void HandleGameStateChanged(GameFlowState state)
        {
            if (state != GameFlowState.PostStory)
            {
                return;
            }

            ShowPostStory();
            HandleCongratulationsText();
        }

        private void ShowPostStory()
        {
            HideGameplayCanvasGroup();
            ShowPostStoryCanvasGroup();
        }

        private void HideGameplayCanvasGroup()
        {
            _gameplayCanvasGroup.alpha = 0f;
            _gameplayCanvasGroup.interactable = false;
            _gameplayCanvasGroup.blocksRaycasts = false;
        }

        private void ShowPostStoryCanvasGroup()
        {
            _postStoryCanvasGroup.alpha = 1f;
            _postStoryCanvasGroup.interactable = true;
            _postStoryCanvasGroup.blocksRaycasts = true;
        }

        private void OnDestroy()
        {
            _gameFlowController.OnStateChanged -= HandleGameStateChanged;
        }
    }
}
