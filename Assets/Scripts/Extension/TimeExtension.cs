using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extension
{
    public static class TimeExtension
    {
        public static float slow;

        private static float _orgScale = 1;
        private static int _count;

        public static void Slow()
        {
            if (slow == 1) return;

            _count++;
            if (_count == 1) Time.timeScale *= slow;
        }

        public static void Fast()
        {
            if (slow == 1) return;

            _count--;
            if (_count == 0) Time.timeScale /= slow;
        }

        public static void Pause()
        {
            _orgScale = Time.timeScale;
            Time.timeScale = 0;
        }

        public static void Resume()
        {
            Time.timeScale = _orgScale;
        }

        public static void Reset()
        {
            _count = 0;
            Time.timeScale = 1;
        }
    }
}