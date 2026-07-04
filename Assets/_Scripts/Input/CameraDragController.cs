using Gameplay.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Input
{
    public class CameraDragController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private SickleController _sickleController;

        [Header("Bounds")]
        [SerializeField] private float _minX;
        [SerializeField] private float _maxX;
        [SerializeField] private float _minY;
        [SerializeField] private float _maxY;

        private Vector2 _lastPointerPosition;

        private void Update()
        {
            if (InputHandler.IsPressed == false || _sickleController.IsDragging)
                return;

            Vector2 pointer = InputHandler.GetPointerScreenPosition();

            if (_lastPointerPosition == Vector2.zero)
            {
                _lastPointerPosition = pointer;
                return;
            }

            Vector3 worldCurrent = _camera.ScreenToWorldPoint(pointer);
            Vector3 worldPrevious = _camera.ScreenToWorldPoint(_lastPointerPosition);

            Vector3 delta = worldPrevious - worldCurrent;

            _camera.transform.position += delta;

            Vector3 position = _camera.transform.position;
            position.x = Mathf.Clamp(position.x, _minX, _maxX);
            position.y = Mathf.Clamp(position.y, _minY, _maxY);

            _camera.transform.position = position;

            _lastPointerPosition = pointer;
        }

        private void LateUpdate()
        {
            if (InputHandler.IsPressed == false)
            {
                _lastPointerPosition = Vector2.zero;
            }
        }
    } 
}