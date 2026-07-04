using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Harvestable
{
    public class HarvestSystem : MonoBehaviour
    {
        [SerializeField] private HarvestableSection[] _harvestableSections;

        public void TryHarvest(Vector3 worldPosition)
        {
            foreach (HarvestableSection section in _harvestableSections)
            {
                Vector3Int cell = section.Grid.WorldToCell(worldPosition);

                if (section.TryGetTile(cell, out HarvestableTile tile))
                {
                    tile.Harvest();
                    return;
                }
            }
        }
    }
}
