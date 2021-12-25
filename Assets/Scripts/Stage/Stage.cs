using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// NOTE
// Beginner, Expert 두 단계로 바꾸자
//
// Beginner : 모든 stuff에서 감속, 세이브 포인트 존재
// Expert : 감속 X, 세이브 포인트 X, (+1.3배속)
public enum Difficulty { Beginner, Expert }

public class Stage : MonoBehaviour
{
    [field: SerializeField] public int Id { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public AudioClip Bgm { get; private set; }
    [field: SerializeField] public Vector3 StartPos { get; private set; }
    [field: SerializeField] public Vector3 StartDir { get; private set; }

    [field: HideInInspector] [field: SerializeField] public bool[] Cleared { get; private set; }
    [field: HideInInspector] [field: SerializeField] public int[] Star { get; private set; }

    private string DirPath => Application.persistentDataPath + "/Save";
    private string FilePath => $"{Id}.json";

    public void Spawn(float offset, float duration)
    {
        gameObject.SetActive(true);

        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y - offset, pos.z);

        transform.DOMoveY(pos.y, duration).SetEase(Ease.OutQuad);
    }

    public void Despawn()
    {
        transform.DOKill(true);
        gameObject.SetActive(false);
    }

    public void Clear(Difficulty difficulty, int star)
    {
        Cleared[(int)difficulty] = true;
        if (star > Star[(int)difficulty]) Star[(int)difficulty] = star;
    }

    public void Load()
    {
        if (!Directory.Exists(DirPath)) Directory.CreateDirectory(DirPath);

        string fullPath = Path.Combine(DirPath, FilePath);
        if (File.Exists(fullPath))
        {
            JsonUtility.FromJsonOverwrite(File.ReadAllText(fullPath), this);
        }
        else
        {
            Cleared = new bool[System.Enum.GetValues(typeof(Difficulty)).Length];
            Star = new int[System.Enum.GetValues(typeof(Difficulty)).Length];
        }
    }

    public void Save()
    {
        if (!Directory.Exists(DirPath)) Directory.CreateDirectory(DirPath);

        string fullPath = Path.Combine(DirPath, FilePath);
        File.WriteAllText(fullPath, JsonUtility.ToJson(this));
    }
}
