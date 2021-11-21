using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;
using TMPro;

namespace Pandora {

public class QuestGiver : MonoBehaviour {
    public Quest quest;

    private PlayerQuest _playerQuest;
    private ThirdPersonInput _thirdPersonInput;

    [Header("Quest window UI")]
    public GameObject questWindow;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI goldText;

    [Header("Input actions")]
    [SerializeField] InputAction _interact;

    private bool questWindowOpened = false;

    void Start() {
        _playerQuest = FindObjectOfType<PlayerQuest>();
        Assert.IsNotNull(_playerQuest);
        _thirdPersonInput = FindObjectOfType<ThirdPersonInput>();
        Assert.IsNotNull(_thirdPersonInput);
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
        questWindow.SetActive(true);
        questWindowOpened = true;
        _thirdPersonInput.DisableInputs();
        _thirdPersonInput.ShowCursor();

        titleText.text = quest.title;
        descriptionText.text = quest.description;
        goldText.text = "Récompense : " + quest.goldReward.ToString() + " $";
    }

    public void AcceptQuest() {
        CloseInterface();
        quest.isActive = true;
        _playerQuest.quest = quest;
    }

    public void CloseInterface() {
        questWindow.SetActive(false);
        questWindowOpened = false;
        _thirdPersonInput.EnableInputs();
        _thirdPersonInput.HideCursor();
    }
}

}

