using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayEventChannel", menuName = "ScriptableObject/PlayEventChannel")]
public class PlayEventChannelSO : ScriptableObject
{
    public event UnityAction onPressStart;
    public event UnityAction onGameStarted;
    public event UnityAction onGameOver;
    public event UnityAction onGameRestarted;
    public event UnityAction onPause;
    public event UnityAction onResume;
    public event UnityAction onGameCleared;
    public event UnityAction onBackToMain;

    public void OnPressStart()
    {
        onPressStart?.Invoke();
    }

    public void OnGameStarted()
    {
        onGameStarted?.Invoke();
    }

    public void OnGameOver()
    {
        onGameOver?.Invoke();
    }

    public void OnGameRestarted()
    {
        onGameRestarted?.Invoke();
    }

    public void OnPause()
    {
        onPause?.Invoke();
    }

    public void OnResume()
    {
        onResume?.Invoke();
    }

    public void OnGameCleared()
    {
        onGameCleared?.Invoke();
    }

    public void OnBackToMain()
    {
        onBackToMain?.Invoke();
    }
}
