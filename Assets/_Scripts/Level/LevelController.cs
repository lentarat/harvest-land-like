using Cysharp.Threading.Tasks.Triggers;
using Gameplay.Harvestable;
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

        private int _currentLevel;
        private float _currentXP;

        private void Awake()
        {
            SubscribeToTileHarvested();
        }

        private void SubscribeToTileHarvested()
        {
            _harvestSystem.OnTileHarvested += HandleTileHarvested;
        }

        private void HandleTileHarvested(HarvestableTile harvestableTile)
        {
            _currentXP += harvestableTile.XP;
            CheckLevelUp();
        }

        private void CheckLevelUp()
        {
            while (_currentLevel + 1 < _levelsDatas.Length &&
                _currentXP >= _levelsDatas[_currentLevel].XPRequired)
            {
                _currentLevel++;
                UpdateSections();
            }
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

        private void OnDestroy()
        {
            UnsubscribeToTileHarvested();
        }

        private void UnsubscribeToTileHarvested()
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