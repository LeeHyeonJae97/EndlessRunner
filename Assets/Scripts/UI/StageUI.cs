using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Extension;

public class StageUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _stageNameText;
    [SerializeField] private TextMeshProUGUI _difficultyText;
    [SerializeField] private Image[] _starImages;
    [SerializeField] private Color _starOnColor;
    [SerializeField] private Color _starOffColor;
    [SerializeField] private Image[] _stageListImages;
    [SerializeField] private Image _stageSelectionImage;
    [SerializeField] private StageManager _stageManager;
    [SerializeField] private PlayEventChannelSO _channel;

    private Canvas _canvas;

    private void Start()
    {
        _channel.onPressStart += Show;
        _channel.onGameStarted += Hide;
        _channel.onBackToMain += Show;

        _canvas = GetComponent<Canvas>();

        UpdateStageListImageLayout();
    }

    private void OnDestroy()
    {
        _channel.onPressStart -= Show;
        _channel.onGameStarted -= Hide;
        _channel.onBackToMain -= Show;
    }

    private void OnEnable()
    {
        _stageManager.onStageValueChanged += UpdateStageUI;
        _stageManager.onDifficultyValueChanged += UpdateDifficultyUI;
        _stageManager.onStageBundleValueChanged += UpdateStageBundleUI;
    }

    private void OnDisable()
    {
        _stageManager.onStageValueChanged -= UpdateStageUI;
        _stageManager.onDifficultyValueChanged -= UpdateDifficultyUI;
        _stageManager.onStageBundleValueChanged -= UpdateStageBundleUI;
    }

    public void ChangeStage(bool value)
    {
        _stageManager.ChangeStage(value);
    }

    public void ChangeDifficulty()
    {
        _stageManager.ChangeDifficulty();
    }

    private void UpdateStageUI(Difficulty difficulty, Stage stage)
    {
        _stageNameText.text = stage.Name;
        for (int i = 0; i < _starImages.Length; i++)
            _starImages[i].color = i < stage.Star[(int)difficulty] ? _starOnColor : _starOffColor;
        _stageListImages[stage.Id].color = stage.Cleared[(int)difficulty] ? Color.green : Color.white;
        _stageSelectionImage.transform.position = _stageListImages[stage.Id].transform.position;
    }

    private void UpdateDifficultyUI(Difficulty difficulty)
    {
        _difficultyText.text = $"{difficulty}";
    }

    private void UpdateStageBundleUI(Difficulty difficulty, List<Stage> stages)
    {
        for (int i = 0; i < stages.Count; i++)
            _stageListImages[i].color = stages[i].Cleared[(int)difficulty] ? Color.green : Color.white;
    }

    private void UpdateStageListImageLayout()
    {
        for (int i = 0; i < _stageListImages.Length; i++)
            _stageListImages[i].gameObject.SetActive(i < _stageManager.StageCount);
    }

    private void Show()
    {
        _canvas.SetActive(true);
    }

    private void Hide()
    {
        _canvas.SetActive(false);
    }
}
