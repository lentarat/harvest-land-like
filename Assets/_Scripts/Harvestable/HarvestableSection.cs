using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Harvestable
{
    public class HarvestableSection : MonoBehaviour
    {
        [SerializeField] private HarvestableTile _harvestableTile;
        [SerializeField] private Grid _grid;
        [SerializeField] private int _edgeCount = 8;
        [SerializeField, Range(0, 0.1f)] private float _randomOffsetRange;

        private Vector3 _tileCentreOffset;

        private void Awake()
        {
            SetTileCentreOffset();
            FillSection();
        }

        private void SetTileCentreOffset()
        {
            _tileCentreOffset = new Vector3(0f, _grid.cellSize.y * 0.25f, 0f);
        }

        private void FillSection()
        {
            float halfEdgeCount = _edgeCount * 0.5f;
            int floorHalfEdgeCount = Mathf.FloorToInt(halfEdgeCount);
            for (int i = -floorHalfEdgeCount; i < halfEdgeCount; i++)
            {
                for (int j = -floorHalfEdgeCount; j < halfEdgeCount; j++)
                {
                    HarvestableTile harvestableTile = Instantiate(_harvestableTile);

                    Vector3 randomOffset = new Vector3(
                        Random.Range(-_randomOffsetRange, _randomOffsetRange),
                        Random.Range(-_randomOffsetRange, _randomOffsetRange),
                        0f);
                    Vector3 worldPosition = _grid.CellToWorld(new Vector3Int(i, j, 0)) + _tileCentreOffset + randomOffset;
                    
                    harvestableTile.transform.position = worldPosition;
                }
            }
        }
    }
}