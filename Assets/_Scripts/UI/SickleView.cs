using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.UI
{
    public class SickleView : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;

        public Vector3 GetPosition()
        {
            Vector3 result = _rectTransform.position;
            return result;
        }

        public void SetPosition(Vector3 screenPosition)
        {
            _rectTransform.position = screenPosition;
        }

        public Vector2 GetPivotPosition()
        {
            Vector2 result = _rectTransform.pivot;
            return result;
        }

        public void SetPivotPosition(Vector2 pivot)
        {
            _rectTransform.pivot = pivot;
        }
    }
}