using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Text;
using System;

public class QuestPanel : ToggleablePanel
{
    [SerializeField] private Quest _selectedQuest;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _currentObjectivesText;
    [SerializeField] private Image _iconImage;

    private Step _selectedStep => _selectedQuest.CurrentStep;

    [ContextMenu("Bind")]
    public void Bind()
    {
        _nameText.SetText(_selectedQuest.DisplayName);
        _descriptionText.SetText(_selectedQuest.Description);
        _iconImage.sprite = _selectedQuest.Sprite;

        DisplayStepInstructions();
        Show();
    }

    private void DisplayStepInstructions() {
        StringBuilder builder = new StringBuilder();
        if (_selectedStep != null)
        {
            builder.AppendLine(_selectedStep.Instructions);
            foreach (Objective objective in _selectedStep.Objectives)
            {
                string rgb = objective.IsCompleted ? "green" : "gray";
                builder.AppendLine($"<color=#{rgb}>{objective}</color>");
            }
        }
        _currentObjectivesText.SetText(builder.ToString());
    }

    public void SelectQuest(Quest quest) {
        if (_selectedQuest) {
            _selectedQuest.Changed -= DisplayStepInstructions;
        }
        _selectedQuest = quest;
        Bind();
        Show();

        _selectedQuest.Changed += DisplayStepInstructions;
    }
}
