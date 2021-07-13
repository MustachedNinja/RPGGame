using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    [SerializeField] private QuestPanel _questPanel;
    [SerializeField] private List<Quest> _allQuests = new List<Quest>();
    private List<Quest> _activeQuests = new List<Quest>();

    public static QuestManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    public void AddQuest(Quest quest) {
        _activeQuests.Add(quest);
        _questPanel.SelectQuest(quest);
    }

    public void AddQuestByName(string questName) {
        Quest quest = _allQuests.FirstOrDefault(quest => quest.name == questName);
        if (quest != null) {
            AddQuest(quest);
        } else {
            Debug.Log($"Missing quest {questName}");
        }
    }

    public void ProgressQuests() {
        foreach(Quest quest in _activeQuests) {
            quest.TryProgress();
        }
    }
}