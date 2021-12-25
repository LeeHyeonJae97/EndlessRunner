using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    private static Timer _instance;

    [SerializeField] private TextMeshProUGUI _timerText;

    private List<IEnumerator> _callbacks = new List<IEnumerator>();
    private Canvas _canvas;
    private int _time;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            _instance = null;
        }
    }

    private void Start()
    {
        _canvas = GetComponent<Canvas>();
    }

    public static void AddCallback(IEnumerator callback)
    {
        _instance._callbacks.Add(callback);
    }

    public static void ResetCallbacks()
    {
        _instance._callbacks = new List<IEnumerator>();
    }

    public static void Init(int time)
    {
        _instance._time = time;
        _instance.StartCoroutine(_instance.TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        WaitForSeconds interval = new WaitForSeconds(1);

        _canvas.enabled = true;
        while (_time > 0)
        {
            _timerText.text = $"{_time}";
            _time -= 1;

            yield return interval;
        }
        _canvas.enabled = false;
        for (int i = 0; i < _callbacks.Count; i++)
        {
            yield return StartCoroutine(_callbacks[i]);
        }
        ResetCallbacks();            
    }
}
