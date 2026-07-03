using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Gameplay.Harvestable 
{ 
    public class HarvestableTile : MonoBehaviour
    {
        [SerializeField] private HarvestableGrowthState _harvestableGrowthState;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private HarvestableTileData[] _harvestableTileData;
        [SerializeField] private float _xp = 5f;
        [SerializeField] private int _growthStateUpdatePeriodMS = 2000;

        public void Harvest()
        {
            if (_harvestableGrowthState != HarvestableGrowthState.Mature)
            {
                return;
            }

            _harvestableGrowthState = HarvestableGrowthState.Absent;
            Regrow().Forget();
        }

        private async UniTask Regrow()
        {
            await UniTask.Delay(_growthStateUpdatePeriodMS);
            _harvestableGrowthState = HarvestableGrowthState.Sprout;

            await UniTask.Delay(_growthStateUpdatePeriodMS);
            _harvestableGrowthState = HarvestableGrowthState.Mature;
        }

        private void Awake()
        {
            SetRandomXSpriteFlip();
        }

        private void SetRandomXSpriteFlip()
        {
            _spriteRenderer.flipX = UnityEngine.Random.value > 0.5f;
        }

        [Serializable]
        private struct HarvestableTileData
        {
            public HarvestableGrowthState HarvestableGrowthState;
            public Sprite Sprite;
        }
    }
}
