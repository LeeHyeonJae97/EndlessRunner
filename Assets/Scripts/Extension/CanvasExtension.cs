using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extension
{
    public static class CanvasExtension
    {
        public static void SetActive(this Canvas canvas, bool value)
        {
            canvas.enabled = value;
            canvas.gameObject.BroadcastMessage(value ? "OnEnable" : "OnDisable");
        }
    }
}