using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerUI : MonoBehaviour
{
    [SerializeField] private PlayEventChannelSO _channel;

    private Canvas _canvas;

    // Start is called before the first frame update
    private void Start()
    {
        _channel.onGameStarted += Show;
        _channel.onGameRestarted += Show;
        _channel.onResume += Show;
        _channel.onGameOver += Hide;
        _channel.onGameCleared += Hide;
        _channel.onPause += Hide;
        _channel.onBackToMain += Hide;

        _canvas = GetComponent<Canvas>();
    }

    private void OnDestroy()
    {
        _channel.onGameStarted -= Show;
        _channel.onGameRestarted -= Show;
        _channel.onResume -= Show;
        _channel.onGameOver -= Hide;
        _channel.onGameCleared -= Hide;
        _channel.onPause -= Hide;
        _channel.onBackToMain -= Hide;
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
