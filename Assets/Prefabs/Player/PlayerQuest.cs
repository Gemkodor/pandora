using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;

namespace Pandora {

public class PlayerQuest : MonoBehaviour {
    [SerializeField] InputAction _accomplishQuestInput;

    public QuestGiver questGiver;
    public Quest quest;
    public int gold;
    public bool canMove = true;

    void OnEnable() {
        _accomplishQuestInput.Enable();
    }

    void OnDisable() {
        _accomplishQuestInput.Disable();
    }

    private void Update() {
        QuestInput();
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

