using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pandora {

[System.Serializable]
public class Quest
{
    public bool isActive;
    public string title;
    public string description;
    public int goldReward;

    public QuestGoal goal;

    public void Complete() {
        isActive = false;
    }
}

}

