using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour {
    [SerializeField] private Quest _quest;
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            QuestManager.Instance.AddQuest(_quest);
        }
    }
}