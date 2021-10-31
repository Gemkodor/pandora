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

    private void Awake() {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            StartGame();
        } else if (_instance != this) {
            Destroy(this.gameObject);
        }
    }

    private void StartGame() {
        Debug.Log("Game Manager created.");

        var currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == Scenes.LoadingScreen) {
            // Game is started normally.
            StartCoroutine(AsyncLoadSceneCoroutine(Scenes.ArfoxLogo));
        } else {
            // Game is started with a specific scene.
            // This is most likely a run in the Unity editor.
        }
    }

    private IEnumerator AsyncLoadSceneCoroutine(string sceneName) {
        Debug.Log($"Loading scene `{sceneName}` in the background.");
        AsyncOperation load = SceneManager.LoadSceneAsync(sceneName);

        while (!load.isDone) {
            yield return null;
        }

        Debug.Log($"Playing scene `{sceneName}`.");
    }

    /*
     * Immediately load a scene and play it.
     * If the scene is huge, prefer `AsyncLoadScene()` to avoid freezing the game.
     */
    public void ImmediateLoadScene(string sceneName) {
        Debug.Log($"Loading scene `{sceneName}`.");
        SceneManager.LoadScene(sceneName);
    }

    /*
     * Load a scene while showing a loading screen.
     */
    public void AsyncLoadScene(string sceneName) {
        SceneManager.LoadScene(Scenes.LoadingScreen);
        StartCoroutine(AsyncLoadSceneCoroutine(sceneName));
    }
}
