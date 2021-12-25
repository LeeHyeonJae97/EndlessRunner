using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private PlayEventChannelSO _channel;

    private Canvas _canvas;

    private void Start()
    {
        _channel.onGameStarted += Hide;
        _channel.onGameRestarted += Hide;
        _channel.onGameOver += Show;
        _channel.onBackToMain += Hide;

        _canvas = GetComponent<Canvas>();
    }

    private void OnDestroy()
    {
        _channel.onGameStarted -= Hide;
        _channel.onGameRestarted -= Hide;
        _channel.onGameOver -= Show;
        _channel.onBackToMain -= Hide;
    }

    private void Show()
    {
        StartCoroutine(ShowCoroutine());
    }

    private IEnumerator ShowCoroutine()
    {
        yield return new WaitForSeconds(0.8f);
        _canvas.enabled = true;
    }

    private void Hide()
    {
        _canvas.enabled = false;
    }
}
