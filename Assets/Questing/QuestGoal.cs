using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pandora {

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;
    public int requiredAmount;
    public int currentAmount;

    public bool IsReached() {
        return currentAmount >= requiredAmount;
    }

    public void EnemyKilled() {
        if (goalType == GoalType.Kill) {
            currentAmount++;
        }
    }

    public void ItemCollected() {
        if (goalType == GoalType.Gathering) {
            currentAmount++;
        }
    }
}

public enum GoalType {
    Kill,
    Gathering
}

}

