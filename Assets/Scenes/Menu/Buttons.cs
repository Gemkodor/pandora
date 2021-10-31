using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour {
    public void OnNewGame() {
        Debug.Log("Starting new game.");
    }

    public void OnSandbox() {
        Debug.Log("Playing in the sandbox.");
    }

    public void OnQuit() {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
