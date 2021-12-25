using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UserInfo", menuName = "ScriptableObject/UserInfo")]
public class UserInfoSO : ScriptableObject
{
    public StageInfo[] StageInfos { get; private set; }

    public void Load()
    {

    }

    public void Save()
    {

    }
}
