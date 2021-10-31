using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {
    public void OnNewGame() {
        Debug.Log("Starting new game.");
    }

    public void OnSandbox() {
        Debug.Log("Playing in the sandbox.");
        GameManager.Instance.AsyncLoadScene(Scenes.Sandbox);
    }

    public void OnQuit() {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
