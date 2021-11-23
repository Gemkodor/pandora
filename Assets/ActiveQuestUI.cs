using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using TMPro;

namespace Pandora {

public class ActiveQuestUI : MonoBehaviour
{
    PlayerQuest playerQuest;

    [SerializeField] TextMeshProUGUI questTitle;
    [SerializeField] TextMeshProUGUI amount;

    void Start()
    {
        playerQuest = FindObjectOfType<PlayerQuest>();
        Assert.IsNotNull(playerQuest);

        Assert.IsNotNull(questTitle);
        Assert.IsNotNull(amount);
    }

    private void Update() {
        if (playerQuest.quest.isActive) {
            questTitle.text = playerQuest.quest.title;
            amount.text = playerQuest.quest.goal.currentAmount + " / " + playerQuest.quest.goal.requiredAmount;
        } else {
            gameObject.SetActive(false);
        }
    }
}

}
