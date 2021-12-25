using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum CinemachineCameraType { Title, Lobby, Play }

public class CinemachineCamera : MonoBehaviour
{
    public static CinemachineVirtualCamera main
    {
        get
        {
            return _vcams[(int)_type];
        }
    }

    private static CinemachineVirtualCamera[] _vcams;
    private static CinemachineCameraType _type = CinemachineCameraType.Title;

    private void Awake()
    {
        _vcams = GetComponentsInChildren<CinemachineVirtualCamera>(true);
    }

    public static void Change(CinemachineCameraType type)
    {
        if (_type == type) return;

        main.gameObject.SetActive(false);
        _type = type;
        main.gameObject.SetActive(true);
    }
}
