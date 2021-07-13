using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest")]
public class Quest : ScriptableObject
{
    [SerializeField] private string _displayName;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _sprite;

    [Tooltip("Development notes, not visible in game")]
    [SerializeField] private string _notes;

    public List<Step> Steps;
    private int _currentStepIndex;

    public string DisplayName => _displayName;
    public string Description => _description;
    public Sprite Sprite => _sprite;

    public void TryProgress() {
        Step currentStep = GetCurrentStep();
        if (currentStep.HasAllObjectivesCompleted()) {
            _currentStepIndex++;
        }
    }

    private Step GetCurrentStep() {
        return Steps[_currentStepIndex];
    }

}

[Serializable]
public class Step {

    [SerializeField] private string _instructions;
    public List<Objective> Objectives;
    public string Instructions => _instructions;

    public bool HasAllObjectivesCompleted() {
        return Objectives.TrueForAll(objective => objective.IsCompleted);
    }
}

[Serializable]
public class Objective {

    [SerializeField] private ObjectiveType _objectiveType;

    public bool IsCompleted { get; }

    public enum ObjectiveType {
        Flag,
        Item,
        Kill
    }

    public override string ToString() {
        return _objectiveType.ToString();
    }

}