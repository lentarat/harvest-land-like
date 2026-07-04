using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Harvestable
{
    public class HarvestableSection : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private HarvestableTile _harvestableTile;
        [SerializeField] private Grid _grid;
        [SerializeField] private Transform _harverstablesParent;

        [Header("Settings")]
        [SerializeField] private int _edgeCount = 8;

        [Header("Randomness Factor")]
        [SerializeField, Range(0, 0.1f)] private float _randomOffsetRange;
        [SerializeField] private bool _randomizeFlip = true;

        public Grid Grid => _grid;

        private readonly Dictionary<Vector3Int, HarvestableTile> _vectorToTileMap = new();
        private Vector3 _tileCentreOffset;

        private void Awake()
        {
            if (_randomizeFlip)
            {
                SetTileCentreOffset();
            }

            FillSection();
        }

        private void SetTileCentreOffset()
        {
            _tileCentreOffset = new Vector3(0f, _grid.cellSize.y * 0.25f, 0f);
        }

        private void FillSection()
        {
            _vectorToTileMap.Clear();

            float halfEdgeCount = _edgeCount * 0.5f;
            int floorHalfEdgeCount = Mathf.FloorToInt(halfEdgeCount);

            for (int i = -floorHalfEdgeCount; i < halfEdgeCount; i++)
            {
                for (int j = -floorHalfEdgeCount; j < halfEdgeCount; j++)
                {
                    Vector3Int cell = new Vector3Int(i, j, 0);

                    HarvestableTile tile = Instantiate(_harvestableTile);
                    tile.transform.parent = _harverstablesParent;

                    Vector3 randomOffset = new Vector3(
                        Random.Range(-_randomOffsetRange, _randomOffsetRange),
                        Random.Range(-_randomOffsetRange, _randomOffsetRange),
                        0f);
                    Vector3 worldPosition = _grid.CellToWorld(new Vector3Int(i, j, 0))
                        + _tileCentreOffset
                        + randomOffset;

                    tile.transform.position = worldPosition;

                    _vectorToTileMap.Add(cell, tile);
                }
            }
        }

        public bool TryGetTile(Vector3Int cell, out HarvestableTile tile)
        {
            bool result = _vectorToTileMap.TryGetValue(cell, out tile);
            return result;
        }
    }
}