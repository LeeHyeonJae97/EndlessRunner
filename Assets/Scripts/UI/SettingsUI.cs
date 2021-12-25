using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private PlayEventChannelSO _channel;

    private Canvas _canvas;

    private void Start()
    {
        _channel.onPressStart += Show;
        _channel.onGameStarted += Hide;
        _channel.onGameRestarted += Hide;
        _channel.onBackToMain += Show;

        _canvas = GetComponent<Canvas>();
    }

    private void OnDestroy()
    {
        _channel.onPressStart -= Show;
        _channel.onGameStarted -= Hide;
        _channel.onGameRestarted -= Hide;
        _channel.onBackToMain -= Show;
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
