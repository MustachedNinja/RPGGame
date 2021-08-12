using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest")]
public class Quest : ScriptableObject
{

    public event Action Changed;

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
    public Step CurrentStep => Steps[_currentStepIndex];

    private void OnEnable() {
        _currentStepIndex = 0;
        foreach(var step in Steps) {
            foreach(var objective in step.Objectives) {
                if (objective.GameFlag != null) {
                    objective.GameFlag.Changed += HandleFlagChanged;
                }
            }
        }
    }

    public void TryProgress() {
        Step currentStep = GetCurrentStep();
        if (currentStep.HasAllObjectivesCompleted()) {
            _currentStepIndex++;
            Changed?.Invoke();
        }
    }

    private Step GetCurrentStep() {
        return Steps[_currentStepIndex];
    }

    private void HandleFlagChanged() {
        TryProgress();
        Changed?.Invoke();
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

    [SerializeField] private GameFlag _gameFlag;

    public GameFlag GameFlag => _gameFlag;

    public bool IsCompleted { 
        get {
            switch(_objectiveType) {
                case ObjectiveType.Flag:
                    return _gameFlag.Value;
                
                default:
                    return false;
            }
        } 
    }

    public enum ObjectiveType {
        Flag,
        Item,
        Kill
    }

    public override string ToString() {
        switch(_objectiveType) {
            case ObjectiveType.Flag:
                return _gameFlag.name;
            default:
                return _objectiveType.ToString();
        }
    }

}