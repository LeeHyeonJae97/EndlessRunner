using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Extension;

public class GameStatusUI : MonoBehaviour
{
    [SerializeField] private GameObject[] _starImages;
    [SerializeField] private Player _player;
    [SerializeField] private PlayEventChannelSO _channel;

    private Canvas _canvas;

    private void Start()
    {
        _channel.onGameStarted += Show;
        _channel.onBackToMain += Hide;

        _canvas = GetComponent<Canvas>();
    }

    private void OnDestroy()
    {
        _channel.onGameStarted -= Show;
        _channel.onBackToMain -= Hide;
    }

    private void OnEnable()
    {
        _player.onStarValueChanged += UpdateStarUI;
    }

    private void OnDisable()
    {
        _player.onStarValueChanged -= UpdateStarUI;
    }

    private void Show()
    {
        _canvas.SetActive(true);
    }

    private void Hide()
    {
        _canvas.SetActive(false);
    }

    private void UpdateStarUI(int count)
    {
        for (int i = 0; i < _starImages.Length; i++)
            _starImages[i].SetActive(i < count);
    }
}
