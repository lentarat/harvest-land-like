using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Harvestable
{
    public class HarvestSystem : MonoBehaviour
    {
        [SerializeField] private HarvestableSection[] _harvestableSections;

        public event Action<HarvestableTile> OnTileHarvested;

        public void TryHarvest(Vector3 worldPosition)
        {
            foreach (HarvestableSection section in _harvestableSections)
            {
                if (section.IsUnlocked == false)
                {
                    continue;
                }

                Vector3Int cell = section.Grid.WorldToCell(worldPosition);

                if (section.TryGetTile(cell, out HarvestableTile tile))
                {
                    bool hasHarvested = tile.TryHarvest();

                    if (hasHarvested)
                    {
                        OnTileHarvested?.Invoke(tile);
                    }

                    return;
                }
            }
        }
    }
}
