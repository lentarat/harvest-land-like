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
        [SerializeField] private float _growthStateUpdatePeriodMS;
        [SerializeField, Range (0f, 0.8f)] private float _regrowPeriodDispersion;

        private Dictionary<HarvestableGrowthState, Sprite> _growthStateToSpriteMap;

        public void Harvest()
        {
            if (_harvestableGrowthState != HarvestableGrowthState.Mature)
            {
                return;
            }

            SetState(HarvestableGrowthState.Absent);
            Regrow().Forget();
        }

        private void SetState(HarvestableGrowthState state)
        {
            if (state == HarvestableGrowthState.Absent)
            {
                _spriteRenderer.sprite = null;
            }
            else
            {
                _spriteRenderer.sprite = _growthStateToSpriteMap[state];
            }

            _harvestableGrowthState = state;
        }

        private async UniTask Regrow()
        {
            int waitingTime = GetWaitingTime();
            await UniTask.Delay(waitingTime);
            SetState(HarvestableGrowthState.Sprout);

            waitingTime = GetWaitingTime();
            await UniTask.Delay(waitingTime);
            SetState(HarvestableGrowthState.Medium);

            waitingTime = GetWaitingTime();
            await UniTask.Delay(waitingTime);
            SetState(HarvestableGrowthState.Mature);
        }

        private int GetWaitingTime()
        {
            float randomWaitingMultiplier = UnityEngine.Random.Range(0f, _regrowPeriodDispersion);
            int waitingTime = (int)(_growthStateUpdatePeriodMS * randomWaitingMultiplier);
            int result = (int)_growthStateUpdatePeriodMS - waitingTime;
            return result;
        }

        private void Awake()
        {
            FillDictionary();
            SetRandomXSpriteFlip();
        }

        private void FillDictionary()
        {
            _growthStateToSpriteMap = new Dictionary<HarvestableGrowthState, Sprite>();
            foreach (HarvestableTileData data in _harvestableTileData)
            {
                _growthStateToSpriteMap[data.HarvestableGrowthState] = data.Sprite;
            }
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
