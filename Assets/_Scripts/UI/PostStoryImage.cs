using Gameplay.Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostStoryImage : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private LevelController _levelController;
    [SerializeField] private CanvasGroup _gameplayCanvasGroup;

    private void Awake()
    {
        _levelController.OnPostStoryShown += HandlePostStoryShown;
    }

    private void HandlePostStoryShown()
    {
        HideGameplayUI();
        ShowStory();
    }

    private void HideGameplayUI()
    {
        _gameplayCanvasGroup.alpha = 0f;
        _gameplayCanvasGroup.interactable = false;
        _gameplayCanvasGroup.blocksRaycasts = true;
    }

    private void ShowStory()
    {
        _image.enabled = true;
    }

    private void OnDestroy()
    {
        _levelController.OnPostStoryShown -= HandlePostStoryShown;
    }
}
