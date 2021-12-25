using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform _out;

    public Vector3 WarpedPosition => _out.position;    
}
