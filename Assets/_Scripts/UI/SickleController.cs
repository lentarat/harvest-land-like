using Gameplay.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

namespace Gameplay.UI
{
    public class SickleController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private RectTransform _sickleTransform;
        [SerializeField] private Transform _idleSickleTransform;
        [SerializeField] private Camera _camera;
        [SerializeField] private Collider2D _sickleButtonCollider;

        [Header("Button")]
        [SerializeField] private float _returnSpeed;
        [SerializeField] private Vector2 _sicklePivotDragging = new Vector2(0.58f, 0.38f);

        //public event System.Action<Vector3> OnDragWorld;
        //public event System.Action OnRelease;
        
        private bool _dragging;
        private Vector2 _sicklePivotIdle;

        private void Awake()
        {
            _sicklePivotIdle = _sickleTransform.pivot;
        }

        private void Update()
        {
            CheckForSickleMovement();
        }

        private void CheckForSickleMovement()
        {
            if (IsScreenPressed() && IsSickleButtonPressed())
            {
                if (!_dragging)
                {
                    _dragging = true;
                    _sickleTransform.pivot = _sicklePivotDragging;
                }

                Vector3 worldPosition = InputHandler.GetPointerScreenPosition();
                _sickleTransform.position = worldPosition;

                //OnDragWorld?.Invoke(worldPosition);
            }
            else
            {
                if (_dragging)
                {
                    _dragging = false;
                    _sickleTransform.pivot = _sicklePivotIdle;
                    //OnRelease?.Invoke();
                }

                ReturnToSlot();
            }
        }

        private bool IsScreenPressed() => InputHandler.IsPressed;
        private bool IsSickleButtonPressed()
        {
            Vector3 screenPosition = InputHandler.GetPointerScreenPosition();
            bool result = _sickleButtonCollider.OverlapPoint(screenPosition);
            return result;
        }

        private void ReturnToSlot()
        {
            _sickleTransform.position = Vector3.Lerp(
                _sickleTransform.position,
                _idleSickleTransform.position,
                Time.deltaTime * _returnSpeed);
        }
    }
}