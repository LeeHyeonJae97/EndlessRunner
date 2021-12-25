using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using Extension;

public class StageManager : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] private float _spawnOffset;
    [SerializeField] private float _spawnDuration;

    [SerializeField] private Player _player;
    [SerializeField] private PlayEventChannelSO _channel;

    public Stage Stage
    {
        get { return _stage; }

        set
        {
            Spawn(value);

            _stage = value;
            onStageValueChanged?.Invoke(_difficulty, _stage);
        }
    }
    public Difficulty Difficulty
    {
        get { return _difficulty; }

        set
        {
            _difficulty = value;
            _player.Difficulty = value;
            switch (value)
            {
                case Difficulty.Beginner:
                    TimeExtension.slow = 0.5f;
                    break;

                case Difficulty.Expert:
                    TimeExtension.slow = 1f;
                    break;
            }
            onStageValueChanged?.Invoke(_difficulty, _stage);
            onDifficultyValueChanged?.Invoke(_difficulty);
            onStageBundleValueChanged?.Invoke(_difficulty, _stages);
        }
    }
    public int StageCount => _stages.Count;

    private List<Stage> _stages = new List<Stage>();
    private Stage _stage;
    private Difficulty _difficulty;
    private Star[] _stars;
    private SavePoint[] _savePoints;

    public UnityAction<Difficulty, Stage> onStageValueChanged;
    public UnityAction<Difficulty> onDifficultyValueChanged;
    public UnityAction<Difficulty, List<Stage>> onStageBundleValueChanged;

    // DEPRECATED
    private void Awake()
    {
        Load();
    }

    private void Start()
    {
        _channel.onPressStart += Init;
    }

    private void OnDestroy()
    {
        _channel.onPressStart -= Init;
    }

    private void OnEnable()
    {
        _channel.onGameStarted += OnGameStarted;
        _channel.onGameRestarted += OnGameRestarted;
        _channel.onGameCleared += OnGameCleared;
        _channel.onBackToMain += OnBackToMain;
    }

    private void OnDisable()
    {
        _channel.onGameStarted -= OnGameStarted;
        _channel.onGameRestarted -= OnGameRestarted;
        _channel.onGameCleared -= OnGameCleared;
        _channel.onBackToMain -= OnBackToMain;
    }

    private void Init()
    {
        Spawn(_stages[0]);
        _stage = _stages[0];
        Difficulty = Difficulty.Beginner;
    }

    public void Load()
    {
        Stage[] prefabs = Resources.LoadAll<Stage>("Stage");
        for (int i = 0; i < prefabs.Length; i++)
        {
            Stage stage = Instantiate(prefabs[i]);
            stage.Load();
            stage.gameObject.SetActive(false);
            _stages.Add(stage);
        }
    }

    public void ChangeStage(bool left)
    {
        int index = _stages.IndexOf(_stage);
        if (left)
        {
            index -= 1;
            if (index < 0) index = _stages.Count - 1;
            Stage = _stages[index];
        }
        else
        {
            index += 1;
            if (index >= _stages.Count) index = 0;
            Stage = _stages[index];
        }
    }

    public void ChangeDifficulty()
    {
        Difficulty = (Difficulty)(((int)Difficulty + 1) % System.Enum.GetValues(typeof(Difficulty)).Length);
    }

    // appearing effect
    private void Spawn(Stage stage)
    {
        if (_stage != stage)
        {
            if (_stage != null) _stage.Despawn();

            // save stars
            _stars = stage.GetComponentsInChildren<Star>();

            // save savepoints
            _savePoints = stage.GetComponentsInChildren<SavePoint>();
        }

        InitStars();
        InitSavePoints();

        stage.Spawn(_spawnOffset, _spawnDuration);
    }

    private void InitStars()
    {
        for (int i = 0; i < _stars.Length; i++)
            _stars[i].Gain(false);
    }

    private void InitSavePoints()
    {
        for (int i = 0; i < _savePoints.Length; i++)
        {
            if (_difficulty == Difficulty.Beginner)
            {
                _savePoints[i].gameObject.SetActive(true);
            }
            else
            {
                _savePoints[i].Save(false);
                _savePoints[i].gameObject.SetActive(true);
            }
        }
    }

    private void OnGameStarted()
    {
        StartCoroutine(OnGameStartedCoroutine());
    }

    private IEnumerator OnGameStartedCoroutine()
    {
        _player.Ready(_stage.StartDir, _stage.StartPos);
        InitStars();
        InitSavePoints();
        CinemachineCamera.Change(CinemachineCameraType.Play);
        yield return new WaitForSeconds(1);
        _player.Go();
    }

    private void OnGameRestarted()
    {
        StartCoroutine(OnGameRestartedCoroutine());
    }

    private IEnumerator OnGameRestartedCoroutine()
    {
        _player.Ready();
        CinemachineCamera.Change(CinemachineCameraType.Play);
        yield return new WaitForSeconds(1);
        _player.Go();
    }

    private void OnGameCleared()
    {
        _stage.Clear(_difficulty, _player.Star);
        _stage.Save();
        _player.Hide();
    }

    private void OnBackToMain()
    {
        Stage = _stage;
        _player.Hide();
        CinemachineCamera.Change(CinemachineCameraType.Lobby);
    }
}
