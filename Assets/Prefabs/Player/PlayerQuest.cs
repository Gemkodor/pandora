using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;

namespace Pandora {

public class PlayerQuest : MonoBehaviour {
    [SerializeField] InputAction _accomplishQuestInput;

    [SerializeField] QuestGiver questGiver;
    [SerializeField] Quest quest;
    [SerializeField] int gold;

    void OnEnable() {
        _accomplishQuestInput.Enable();
    }

    void OnDisable() {
        _accomplishQuestInput.Disable();
    }

    private void Update() {
        QuestInput();
    }

    public void SetQuest(Quest quest) {
        this.quest = quest;
    }

    public Quest GetQuest() {
        return this.quest;
    }

    private void QuestInput() {
        if (_accomplishQuestInput.triggered) {
            quest.goal.EnemyKilled();
            if (quest.goal.IsReached()) {
                gold += quest.goldReward;
                quest.Complete();
            }
        }
    }

}

}

