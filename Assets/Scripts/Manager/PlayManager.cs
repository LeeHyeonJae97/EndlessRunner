using Cinemachine;
using Extension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayManager : MonoBehaviour
{
    [SerializeField] private PlayEventChannelSO _channel;

    private void OnEnable()
    {
        _channel.onPressStart += ChangeCameraInLobby;
        _channel.onGameStarted += TimeExtension.Reset;
        _channel.onGameOver += TimeExtension.Reset;
        _channel.onGameCleared += TimeExtension.Reset;
        _channel.onGameRestarted += TimeExtension.Reset;
        _channel.onPause += TimeExtension.Pause;
        _channel.onResume += TimeExtension.Resume;
        _channel.onBackToMain += TimeExtension.Reset;
    }

    private void OnDisable()
    {
        _channel.onPressStart -= ChangeCameraInLobby;
        _channel.onGameStarted -= TimeExtension.Reset;
        _channel.onGameOver -= TimeExtension.Reset;
        _channel.onGameCleared -= TimeExtension.Reset;
        _channel.onGameRestarted -= TimeExtension.Reset;
        _channel.onPause -= TimeExtension.Pause;
        _channel.onResume -= TimeExtension.Resume;
        _channel.onBackToMain -= TimeExtension.Reset;
    }

    private void ChangeCameraInLobby()
    {
        CinemachineCamera.Change(CinemachineCameraType.Lobby);
    }
}
