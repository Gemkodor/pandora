using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;
using TMPro;

namespace Pandora {

public class QuestGiver : MonoBehaviour {
    [Header("Quest window UI")]
    [SerializeField] GameObject questWindow;
    [SerializeField] GameObject noQuestWindow;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] GameObject interactionTipLabel;
    [SerializeField] GameObject activeQuestPanel;

    [Header("Input actions")]
    [SerializeField] InputAction _interact;

    public List<Quest> quests;
    public Quest activeQuest;

    private PlayerQuest _playerQuest;
    private ThirdPersonInput _thirdPersonInput;
    private bool questWindowOpened = false;

    void Start() {
        _playerQuest = FindObjectOfType<PlayerQuest>();
        Assert.IsNotNull(_playerQuest);
        _thirdPersonInput = FindObjectOfType<ThirdPersonInput>();
        Assert.IsNotNull(_thirdPersonInput);
        Assert.IsNotNull(interactionTipLabel);
        Assert.IsNotNull(activeQuestPanel);
    }

    void OnEnable() {
        _interact.Enable();
    }

    void OnDisable() {
        _interact.Disable();
    }

    private void Update() {
        if (!questWindowOpened &&
                _interact.triggered &&
                Vector3.Distance(transform.position, _playerQuest.transform.position) < 3) {
            OpenQuestWindow();
        }
    }

    public void OpenQuestWindow() {
        _thirdPersonInput.DisableInputs();
        _thirdPersonInput.ShowCursor();
        interactionTipLabel.SetActive(false);

        Quest quest = GetNextQuestToDisplay();

        if (quest != null) {
            questWindow.SetActive(true);
            questWindowOpened = true;

            titleText.text = quest.title;
            descriptionText.text = quest.description;
            goldText.text = "Récompense : " + quest.goldReward.ToString() + " $";
            activeQuest = quest;
        } else {
            noQuestWindow.SetActive(true);
        }
    }

    private Quest GetNextQuestToDisplay() {
        if (_playerQuest.GetQuest() == null || !_playerQuest.GetQuest().isActive) {
            foreach (Quest quest in quests) {
                if (!quest.hasBeenCompleted) {
                    return quest;
                }
            }
        }

        return null;
    }

    public void AcceptQuest() {
        CloseInterface();
        activeQuest.isActive = true;
        activeQuestPanel.SetActive(true);
        _playerQuest.SetQuest(activeQuest);
    }

    public void CloseInterface() {
        noQuestWindow.SetActive(false);
        questWindow.SetActive(false);
        questWindowOpened = false;
        _thirdPersonInput.EnableInputs();
        _thirdPersonInput.HideCursor();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            interactionTipLabel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            interactionTipLabel.SetActive(false);
        }
    }
}

}

