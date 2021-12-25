using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class DirectionChanger : MonoBehaviour
{
    [SerializeField] private bool _manual;
    [field: ShowIf("_manual")] [field: SerializeField] public Vector3 Direction { get; private set; }

    private void OnValidate()
    {
        if (transform.parent == null) return;

        if (!_manual)
        {
            Transform track = transform.parent;
            Transform nextTrack = track.parent.GetChild(track.GetSiblingIndex() + 1);
            Direction = nextTrack.position - track.position;
        }

        transform.forward = Direction;
    }
}
