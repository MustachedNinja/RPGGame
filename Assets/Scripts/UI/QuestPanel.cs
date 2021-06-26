using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Text;
using System;

public class QuestPanel : ToggleablePanel
{
    [SerializeField] private Quest _selectedQuest;
    [SerializeField] private Step _selectedStep;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _currentObjectivesText;
    [SerializeField] private Image _iconImage;

    [ContextMenu("Bind")]
    public void Bind()
    {
        _selectedStep = _selectedQuest.Steps.FirstOrDefault();
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
                builder.AppendLine(objective.ToString());
            }
        }
        _currentObjectivesText.SetText(builder.ToString());
    }

    public void SelectQuest(Quest quest) {
        _selectedQuest = quest;
        Bind();
    }
}
