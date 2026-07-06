using Gameplay.Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostStoryView : MonoBehaviour
{
    [SerializeField] private LevelController _levelController;
    [SerializeField] private CanvasGroup _gameplayCanvasGroup;

    private void Awake()
    {
        _levelController.OnPostStoryShown += HandlePostStoryShown;
        gameObject.SetActive(false);
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
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        _levelController.OnPostStoryShown -= HandlePostStoryShown;
    }
}
