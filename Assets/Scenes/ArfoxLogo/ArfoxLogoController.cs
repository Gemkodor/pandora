using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;


public class ArfoxLogoController : MonoBehaviour {
    [SerializeField][Tooltip("Duration of the scene, in seconds")] float _duration;
    const string _nextScene = Scenes.Menu;

    void Start() {
        // Start a timer that automatically moves to the next scene.
        StartCoroutine(Timer());
    }

    void OnEnable() {
        // Move to the next scene if a button is pressed.
        InputSystem.onEvent += OnInputEvent;
    }

    void OnDisable() {
        InputSystem.onEvent -= OnInputEvent;
    }

    void OnInputEvent(InputEventPtr eventPtr, InputDevice device) {
        // https://forum.unity.com/threads/check-if-any-key-is-pressed.763751/#post-5093831
        if (!eventPtr.IsA<StateEvent>() && !eventPtr.IsA<DeltaStateEvent>())
            return;
        var controls = device.allControls;
        var buttonPressPoint = InputSystem.settings.defaultButtonPressPoint;
        for (var i = 0; i < controls.Count; ++i) {
            var control = controls[i] as ButtonControl;
            if (control == null || control.synthetic || control.noisy)
                continue;
            if (control.ReadValueFromEvent(eventPtr, out var value) && value >= buttonPressPoint) {
                GameManager.Instance.ImmediateLoadScene(_nextScene);
                return;
            }
        }
    }

    IEnumerator Timer() {
        yield return new WaitForSeconds(_duration);
        GameManager.Instance.ImmediateLoadScene(_nextScene);
    }
}
