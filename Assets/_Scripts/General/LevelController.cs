using Cysharp.Threading.Tasks.Triggers;
using Gameplay.General;
using Gameplay.Harvestable;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Level
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private GameFlowController _gameFlowController;
        [SerializeField] private HarvestableSection[] _harvestableSections;
        [SerializeField] private HarvestSystem _harvestSystem;
        [SerializeField] private LevelData[] _levelsDatas;
        [SerializeField] private int _storeRedirectionLevel = 2;
        
        public event Action<int> OnLevelChanged;
        public event Action<float, float, float> OnCurrentXPChanged;
        public event Action OnStoryRedirect;
        public event Action OnPostStoryShown;

        private bool _isStoryTriggered;
        private int _currentLevel;
        
        private float _currentXP;
        private float CurrentXP 
        { 
            get => _currentXP;
            set
            { 
                _currentXP = value;

                CheckLevelUp();

                float previousXP = _currentLevel > 0 ?
                    _levelsDatas[_currentLevel - 1].XPRequired : 0;
                float requiredXP = _levelsDatas[_currentLevel].XPRequired;
                OnCurrentXPChanged?.Invoke(_currentXP, previousXP, requiredXP - 1);
            }
        }

        public void PostStoryImage()
        {
            _gameFlowController.EnterPostStory();
            OnPostStoryShown?.Invoke();
        }

        private void CheckLevelUp()
        {
            while (_currentLevel + 1 < _levelsDatas.Length &&
                CurrentXP >= _levelsDatas[_currentLevel].XPRequired)
            {
                LevelUp();
                UpdateSections();
            }
        }

        private void LevelUp()
        {
            _currentLevel++;
            OnLevelChanged?.Invoke(_currentLevel + 1);
        }

        private void UpdateSections()
        {
            foreach (int index in _levelsDatas[_currentLevel].UnlockedSections)
            {
                _harvestableSections[index].Unlock();
            }
        }

        private void Awake()
        {
            _harvestSystem.OnTileHarvested += HandleTileHarvested;

            LockAllSections();
            UpdateSections();
        }

        private void LockAllSections()
        {
            foreach (HarvestableSection section in _harvestableSections)
            {
                section.Lock();
            }
        }

        private void HandleTileHarvested(HarvestableTile harvestableTile)
        {
            CurrentXP += harvestableTile.XP;
            HandleStoryRedirect();
        }

        private void HandleStoryRedirect()
        { 
            if(_currentLevel == _storeRedirectionLevel && _isStoryTriggered == false)
            {
                _isStoryTriggered = true;
                OnStoryRedirect?.Invoke();
            }
        }

        private void OnDestroy()
        {
            _harvestSystem.OnTileHarvested -= HandleTileHarvested;
        }

        [System.Serializable]
        private struct LevelData
        {
            public float XPRequired;
            public int[] UnlockedSections;
        }
    }
}