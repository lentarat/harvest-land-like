using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Harvestable 
{ 
    public class HarvestableTile : MonoBehaviour
    {
        [SerializeField] private HarvestableGrowthType _harvestableGrowthType;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private HarvestableTileData[] _harvestableTileData;
        [SerializeField] private float _xp = 5f;

        public void Harvest()
        {
            if (_harvestableGrowthType != HarvestableGrowthType.Mature)
            {
                return;
            }

            _harvestableGrowthType = HarvestableGrowthType.Absent;
            
        }

        //private IEnumerator Regrow()
        //{ 
            
        //}

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
            public HarvestableGrowthType HarvestableGrowthType;
            public Sprite Sprite;
        }
    }
}
