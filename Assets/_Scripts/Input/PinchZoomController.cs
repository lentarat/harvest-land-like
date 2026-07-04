using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Input
{
    public class PinchZoomController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _zoomSpeed = 0.01f;
        [SerializeField] private float _minZoom = 3f;
        [SerializeField] private float _maxZoom = 8f;
        [SerializeField] private float _mouseZoomSpeed = 2f;

        private float _previousDistance;

        private void Update()
        {
            HandleMouseZoom();
        }

        private void LateUpdate()
        {
            if (UnityEngine.Input.touchCount < 2)
                _previousDistance = 0;
        }

#if UNITY_STANDALONE || UNITY_EDITOR

        private void HandleMouseZoom()
        {
            float scroll = UnityEngine.Input.mouseScrollDelta.y;

            if (Mathf.Abs(scroll) < 0.01f)
                return;

            float newSize = _camera.orthographicSize - scroll * _mouseZoomSpeed * Time.deltaTime;

            _camera.orthographicSize = Mathf.Clamp(newSize, _minZoom, _maxZoom);
        }

#endif

#if UNITY_IOS || UNITY_ANDROID
      
        private void HandleTouchZoom()
        {
            if (UnityEngine.Input.touchCount != 2)
            {
                _previousDistance = 0;
                return;
            }

            Touch t1 = UnityEngine.Input.GetTouch(0);
            Touch t2 = UnityEngine.Input.GetTouch(1);

            float distance = Vector2.Distance(t1.position, t2.position);

            if (_previousDistance == 0)
            {
                _previousDistance = distance;
                return;
            }

            float delta = distance - _previousDistance;

            float newSize = _camera.orthographicSize - delta * _zoomSpeed;
            _camera.orthographicSize = Mathf.Clamp(newSize, _minZoom, _maxZoom);

            _previousDistance = distance;
        }
#endif
    }
}