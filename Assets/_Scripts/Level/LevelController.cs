using Cysharp.Threading.Tasks.Triggers;
using Gameplay.Harvestable;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Level
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private HarvestableSection[] _harvestableSections;
        [SerializeField] private HarvestSystem _harvestSystem;
        [SerializeField] private LevelData[] _levelsDatas;

        public event Action<int> OnLevelChanged;
        public event Action<float, float, float> OnCurrentXPChanged;

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
            for (int i = 0; i < _harvestableSections.Length; i++)
            {
                if (i <= _currentLevel)
                {
                    _harvestableSections[i].Unlock();
                }
                else
                {
                    _harvestableSections[i].Lock();
                }
            }
        }


        private void Awake()
        {
            _harvestSystem.OnTileHarvested += HandleTileHarvested;
        }

        private void HandleTileHarvested(HarvestableTile harvestableTile)
        {
            CurrentXP += harvestableTile.XP;
        }

        private void OnDestroy()
        {
            _harvestSystem.OnTileHarvested -= HandleTileHarvested;
        }

        [System.Serializable]
        private struct LevelData
        {
            public float XPRequired;
        }
    }
}