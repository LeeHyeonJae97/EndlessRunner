using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private Transform _text;
    [SerializeField] private float _value;
    [SerializeField] private float _duration;
    [SerializeField] private PlayEventChannelSO _channel;

    private Canvas _canvas;

    private void Start()
    {
        _channel.onPressStart += Hide;

        _canvas = GetComponent<Canvas>();

        Vector3 scale = _text.localScale * _value;
        _text.DOScale(scale, _duration).SetLoops(-1, LoopType.Yoyo);
    }

    private void Show()
    {
        _canvas.enabled = true;
    }

    private void Hide()
    {
        _canvas.enabled = false;
    }
}
