using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtensionMethods
{
    public static class CameraExtensionMethods
    {
        public static bool IsOutOfScreen(this Camera cam, Vector3 _position)
        {
            Vector3 viewportPoint = cam.WorldToViewportPoint(_position);
            if (viewportPoint.x < 0 || viewportPoint.x > 1 || viewportPoint.y < 0 || viewportPoint.y > 1) return true;
            return false;
        }
    }
}

