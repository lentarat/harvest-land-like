using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Input
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        public bool IsPressed =>
            Pointer.current != null &&
            Pointer.current.press.isPressed;

        public Vector2 PointerPosition =>
            Pointer.current != null
                ? Pointer.current.position.ReadValue()
                : Vector2.zero;

        public Vector3 GetWorldPosition(float z = 10f)
        {
            Vector2 screenPosition = PointerPosition;
            Vector3 worldPosition = _camera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, z));
            return worldPosition;
        }
    }
}
