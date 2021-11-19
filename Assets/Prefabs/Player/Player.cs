using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pandora {

public class Player : MonoBehaviour
{
    [SerializeField] InputAction _accomplishQuest;
    
    public QuestGiver questGiver;
    public Quest quest;
    public int gold;
    public bool canMove = true;
    
    void OnEnable() {
        _accomplishQuest.Enable();
    }

    void OnDisable() {
        _accomplishQuest.Disable();
    }

    private void Update() {
        if (_accomplishQuest.triggered) {
            quest.goal.EnemyKilled();
            if (quest.goal.IsReached()) {
                gold += quest.goldReward;
                quest.Complete();
            }
        }

    }
}

}

