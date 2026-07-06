using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.General
{
    public class GameFlowController : MonoBehaviour
    {
        public GameFlowState State => _state;
        public event Action<GameFlowState> OnStateChanged;

        private GameFlowState _state = GameFlowState.Gameplay;

        public bool IsGameplay => _state == GameFlowState.Gameplay;
        public bool IsRedirecting => _state == GameFlowState.RedirectingToStore;
        public bool IsPostStory => _state == GameFlowState.PostStory;

        public void EnterGameplay()
        {
            SetState(GameFlowState.Gameplay);
        }

        public void EnterRedirect()
        {
            SetState(GameFlowState.RedirectingToStore);
        }

        public void EnterPostStory()
        {
            SetState(GameFlowState.PostStory);
        }

        private void SetState(GameFlowState newState)
        {
            if (_state == newState)
                return;

            _state = newState;
            OnStateChanged?.Invoke(_state);
        }
    }
}
