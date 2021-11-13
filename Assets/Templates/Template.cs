using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

namespace Pandora {

public class Template : MonoBehaviour {
    [SerializeField] InputAction _mouseClick;

    void Start() {
        // Example of getting a component.
        var rigidBody = GetComponent<Rigidbody>();
        Assert.IsNotNull(rigidBody);
    }

    void OnEnable() {
        _mouseClick.Enable();
    }

    void OnDisable() {
        _mouseClick.Disable();
    }

    void Update() {
        // Example of new input system.
        if (_mouseClick.triggered) {
            Debug.Log("Test");
        }
    }
}

}