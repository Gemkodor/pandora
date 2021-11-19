using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

namespace Pandora {

public class QuestGiver : MonoBehaviour
{
    public Quest quest;

    public Player player;

    [Header("Quest window UI")]
    public GameObject questWindow;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI goldText;

    [Header("Input actions")]
    [SerializeField] InputAction _interact;

    private bool questWindowOpened = false;

    void OnEnable() {
        _interact.Enable();
    }

    void OnDisable() {
        _interact.Disable();
    }
    
    private void Update() {
        if (!questWindowOpened && _interact.triggered && Vector3.Distance(transform.position, player.transform.position) < 3) {
            OpenQuestWindow();
        }
    }

    public void OpenQuestWindow() {
        questWindow.SetActive(true);
        player.canMove = false;
        questWindowOpened = true;

        // Display cursor to let player click on buttons
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        goldText.text = "Récompense : " + quest.goldReward.ToString() + " $";
    }

    public void AcceptQuest() {
        CloseInterface();
        quest.isActive = true;
        player.quest = quest;   
    }

    public void CloseInterface() {
        questWindow.SetActive(false);
        questWindowOpened = false;
        player.canMove = true;
    }
}

}

