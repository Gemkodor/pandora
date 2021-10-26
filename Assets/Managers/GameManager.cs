using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private static GameManager _instance;

    public static GameManager Instance {
        get {
            Assert.IsNotNull(_instance);
            return _instance;
        }
    }

    void Awake() {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            StartGame();
        } else if (_instance != this) {
            Destroy(this.gameObject);
        }
    }

    void StartGame() {
        Debug.Log("Game Manager created.");

        var currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == Scenes.LoadingScreen) {
            // Game is started normally.
        } else {
            // Game is started with a specific scene.
            // This is most likely a run in the Unity editor.
        }
    }
}
