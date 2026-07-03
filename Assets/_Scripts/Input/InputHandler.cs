using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Input
{
    public static class InputHandler
    {
        public static bool IsPressed =>
            Pointer.current != null &&
            Pointer.current.press.isPressed;

        public static Vector2 GetPointerScreenPosition()
        {
            Vector2 result; 
            if (Pointer.current != null)
            {
                result = Pointer.current.position.ReadValue();
            }
            else
            {
                result = Vector2.zero;
            }

            return result;
        }
    }
}
