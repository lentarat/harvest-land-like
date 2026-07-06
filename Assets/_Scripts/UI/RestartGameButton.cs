using Gameplay.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay.UI
{
    public class RestartGameButton : PulsatingButton
    {
        private void Awake()
        {
            base.Init();
            Button.onClick.AddListener(() => RestartGame());
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(0);
        }
    } 
}
