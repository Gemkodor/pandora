using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

namespace Pandora {

public class PlayerWeaponInput : MonoBehaviour {
    [Header("Controller Input")]
    [SerializeField] InputAction _aimInput;
    [SerializeField] InputAction _fireInput;

    [Header("UI")]
    [SerializeField] GameObject _reticle;

    private Animator _animator;

    public static class AnimatorParameters {
        public static int IsAiming = Animator.StringToHash("IsAiming");
    }

    void Start() {
        _animator = GetComponent<Animator>();
        Assert.IsNotNull(_animator);
        Assert.IsNotNull(_reticle);
    }

    private void OnEnable() {
        _aimInput.Enable();
        _fireInput.Enable();
    }

    private void OnDisable() {
        _aimInput.Disable();
        _fireInput.Disable();
    }

    void Update() {
        AimingInput();
        FireInput();
    }

    void AimingInput() {
        bool aiming = _aimInput.ReadValue<float>() > 0.5f;
        _animator.SetBool(AnimatorParameters.IsAiming, aiming);
        _reticle.SetActive(aiming);
    }

    void FireInput() {
        if (_fireInput.triggered) {
            Debug.Log("BOUM!"); // TODO
        }
    }
}

}