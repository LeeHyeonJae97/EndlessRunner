using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extension
{
    public static class MonoBehaviourExtension
    {
        public static void StartCoroutine(this MonoBehaviour mono, IEnumerator function, ref Coroutine coroutine)
        {
            if (coroutine != null) mono.StopCoroutine(coroutine);
            coroutine = mono.StartCoroutine(function);
        }

        public static void StopCoroutine(this MonoBehaviour mono, ref Coroutine coroutine)
        {
            if (coroutine != null)
            {
                mono.StopCoroutine(coroutine);
                coroutine = null;
            }
        }
    }
}
