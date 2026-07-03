using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public class EditorPauser : MonoBehaviour
    {
        public void Update()
        {
            if (Input.GetKey(KeyCode.P))
            {
                Debug.Break();
            }
        }
    }
}