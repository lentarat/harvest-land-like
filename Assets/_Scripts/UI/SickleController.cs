using Gameplay.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

namespace Gameplay.UI
{
    public class SickleController : MonoBehaviour
    {
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private Transform _sickleTransform;
        [SerializeField] private Transform _idleSickleTransform;
        [SerializeField] private float _returnSpeed;

        public event System.Action<Vector3> OnDragWorld;
        public event System.Action OnRelease;

        private bool _dragging;

        private void Update()
        {
            if (_inputHandler.IsPressed)
            {
                if (!_dragging)
                    _dragging = true;

                Vector3 worldPosition = _inputHandler.GetWorldPosition();
                _sickleTransform.position = worldPosition;

                OnDragWorld?.Invoke(worldPosition);
            }
            else
            {
                if (_dragging)
                {
                    _dragging = false;
                    OnRelease?.Invoke();
                }

                ReturnToSlot();
            }
        }

        private void ReturnToSlot()
        {
            _sickleTransform.position = Vector3.Lerp(
                _sickleTransform.position,
                _idleSickleTransform.position,
                Time.deltaTime * _returnSpeed
            );
        }
    }
}