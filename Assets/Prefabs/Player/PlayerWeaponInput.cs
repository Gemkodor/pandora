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

    [Header("Shooting Mechanic")]
    [SerializeField] GameObject _arrowPrefab;
    [SerializeField] Transform _arrowSpawn;
    [SerializeField] float _aimDistance = 20f;
    [SerializeField] float _arrowLaunchVelocity = 2000f;

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
        if (!_fireInput.triggered) {
            return;
        }

        // Where the player is aiming.
        Ray cameraRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        // Where the arrow is heading at.
        Vector3 cameraTarget = cameraRay.origin + cameraRay.direction * _aimDistance;
        // Direction of the arrow, from the spawn position to the target.
        Vector3 arrowDirection = (cameraTarget - _arrowSpawn.position);
        var arrow = Instantiate(_arrowPrefab, _arrowSpawn.position, Quaternion.LookRotation(arrowDirection));
        arrow.GetComponent<Rigidbody>().AddRelativeForce(new Vector3 (0, 0, _arrowLaunchVelocity));
    }
}

}