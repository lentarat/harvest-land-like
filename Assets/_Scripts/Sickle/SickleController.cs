using Gameplay.General;
using Gameplay.Harvestable;
using Gameplay.Input;
using Gameplay.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

namespace Gameplay
{
    public class SickleController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Camera _camera;
        [SerializeField] private SickleView _sickleView;
        [SerializeField] private HarvestSystem _harvestSystem;
        [SerializeField] private Collider2D _sickleCollider;
        [SerializeField] private Transform _sickleSlotTransform;
        [SerializeField] private GameFlowController _gameFlowController;
     
        [Header("Settings")]
        [SerializeField] private Vector2 _dragPivotPosition;
        [SerializeField] private float _returnSpeed;
        public bool IsDragging => _isDragging;

        private float _minSqrMagnitudeToIdle = 0.01f;
        private bool _isDragging;
        private bool _isIdle = true;
        private Vector2 _idlePivotPosition;

        private void Awake()
        {
            _idlePivotPosition = _sickleView.GetPivotPosition();
        }

        private void Update()
        {
            if (_gameFlowController.IsGameplay == false)
            {
                return;
            }

            HandleInput();
        }

        private void HandleInput()
        {
            if (InputHandler.IsPressed)
            {
                Vector2 screenPosition = InputHandler.GetPointerScreenPosition();

                if (_isDragging == false)
                {
                    if (IsPointerOnSickle(screenPosition))
                    {
                        _isIdle = false;
                        _isDragging = true;
                        _sickleView.SetPivotPosition(_dragPivotPosition);
                    }
                }

                if (_isDragging)
                {
                    Vector3 worldPosition = ScreenToWorld(screenPosition);

                    _sickleView.SetPosition(screenPosition);
                    _harvestSystem.TryHarvest(worldPosition);
                }
            }
            else
            {
                if (_isDragging)
                {
                    _isDragging = false;
                    _sickleView.SetPivotPosition(_idlePivotPosition);
                }
            }

            if (_isIdle == false && _isDragging == false)
            {
                ReturnToSlot();
            }
        }

        private void ReturnToSlot()
        {
            Vector3 position = Vector3.Lerp(
                _sickleView.GetPosition(),
                _sickleSlotTransform.position,
                Time.deltaTime * _returnSpeed);

            _sickleView.SetPosition(position);

            float sqrMagnitude = Vector3.SqrMagnitude(_sickleSlotTransform.position - _sickleView.GetPosition());
            if (sqrMagnitude < _minSqrMagnitudeToIdle)
            {
                _isIdle = true;
            }
        }

        private bool IsPointerOnSickle(Vector2 screenPosition)
        {
            bool result = _sickleCollider.OverlapPoint(screenPosition);
            return result;
        }

        private Vector3 ScreenToWorld(Vector2 screenPosition)
        {
            Vector3 worldPosition = _camera.ScreenToWorldPoint(screenPosition);
            worldPosition.z = 0;
            return worldPosition;
        }
    }
}