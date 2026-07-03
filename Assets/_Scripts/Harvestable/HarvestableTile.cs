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

        [Serializable]
        private struct HarvestableTileData
        {
            public HarvestableGrowthType HarvestableGrowthType;
            public Sprite Sprite;
        }

        private void Awake()
        {
            SetRandomXSpriteFlip();
        }

        private void SetRandomXSpriteFlip()
        {
            _spriteRenderer.flipX = UnityEngine.Random.value > 0.5f;
        }
    }
}
