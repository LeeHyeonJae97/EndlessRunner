using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private PlayEventChannelSO _channel;

    private Canvas _canvas;

    private void Start()
    {
        _channel.onGameStarted += Show;
        _channel.onGameStarted += HidePanel;
        _channel.onGameRestarted += Show;
        _channel.onGameCleared += Hide;
        _channel.onGameOver += Hide;
        _channel.onBackToMain += Hide;
        _channel.onBackToMain += HidePanel;
        _channel.onPause += ShowPanel;
        _channel.onResume += HidePanel;

        _canvas = GetComponent<Canvas>();
    }

    private void OnDestroy()
    {
        _channel.onGameStarted -= Show;
        _channel.onGameStarted -= HidePanel;
        _channel.onGameRestarted -= Show;
        _channel.onGameCleared -= Hide;
        _channel.onGameOver -= Hide;
        _channel.onBackToMain -= Hide;
        _channel.onBackToMain -= HidePanel;
        _channel.onPause -= ShowPanel;
        _channel.onResume -= HidePanel;
    }

    private void Show()
    {
        _canvas.enabled = true;
    }

    private void Hide()
    {
        _canvas.enabled = false;
    }

    private void ShowPanel()
    {
        _panel.SetActive(true);
    }

    private void HidePanel()
    {
        _panel.SetActive(false);
    }
}
