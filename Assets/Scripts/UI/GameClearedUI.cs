using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearedUI : MonoBehaviour
{
    [SerializeField] private Image[] _starImages;
    [SerializeField] private Color _gained = Color.white;
    [SerializeField] private Color _default = Color.white;
    [SerializeField] private Player _player;
    [SerializeField] private PlayEventChannelSO _channel;

    private Canvas _canvas;

    private void Start()
    {
        _channel.onGameStarted += Hide;
        _channel.onGameRestarted += Hide;
        _channel.onGameCleared += OnGameCleared;
        _channel.onGameCleared += Show;
        _channel.onBackToMain += Hide;

        _canvas = GetComponent<Canvas>();
    }

    private void OnDestroy()
    {
        _channel.onGameStarted -= Hide;
        _channel.onGameRestarted -= Hide;
        _channel.onGameCleared -= OnGameCleared;
        _channel.onGameCleared -= Show;
        _channel.onBackToMain -= Hide;
    }

    private void OnGameCleared()
    {
        for (int i = 0; i < _starImages.Length; i++)
            _starImages[i].color = i < _player.Star ? _gained : _default;
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
