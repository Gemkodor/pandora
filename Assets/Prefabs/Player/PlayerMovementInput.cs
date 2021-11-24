using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

namespace Pandora {

public class PlayerMovementInput : MonoBehaviour {
    [Header("Controller Input")]
    [SerializeField] InputAction _movementInput;
    [SerializeField] InputAction _jumpInput;
    [SerializeField] InputAction _strafeInput;
    [SerializeField] InputAction _sprintInput;

    [Header("Camera Input")]
    [SerializeField] InputAction _cameraInput;
    [SerializeField] InputAction _escapeInput;

    [HideInInspector] public Invector.vCharacterController.vThirdPersonController cc;
    [HideInInspector] public PlayerCamera thirdPersonCamera;
    [HideInInspector] public Camera cameraMain;
    [HideInInspector] private bool _enabled = true;

    #region Public methods

    public void DisableInputs() {
        _enabled = false;
        cc.input.x = 0f;
        cc.input.z = 0f;
    }

    public void EnableInputs() {
        _enabled = true;
    }

    public void HideCursor() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ShowCursor() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    #endregion
    #region Internal

    protected virtual void Start() {
        InitilizeController();
        InitializeTpCamera();
        HideCursor();
    }

    private void OnEnable() {
        _movementInput.Enable();
        _jumpInput.Enable();
        _strafeInput.Enable();
        _sprintInput.Enable();
        _cameraInput.Enable();
        _escapeInput.Enable();
    }

    private void OnDisable() {
        _movementInput.Disable();
        _jumpInput.Disable();
        _strafeInput.Disable();
        _sprintInput.Disable();
        _cameraInput.Disable();
        _escapeInput.Disable();
    }

    protected virtual void FixedUpdate() {
        cc.UpdateMotor();               // updates the ThirdPersonMotor methods
        cc.ControlLocomotionType();     // handle the controller locomotion type and movespeed
        cc.ControlRotationType();       // handle the controller rotation type
    }

    protected virtual void Update() {
        InputHandle();                  // update the input methods
        cc.UpdateAnimator();            // updates the Animator Parameters
    }

    public virtual void OnAnimatorMove() {
        cc.ControlAnimatorRootMotion(); // handle root motion animations
    }

    #endregion
    #region Basic Locomotion Inputs

    protected virtual void InitilizeController() {
        cc = GetComponent<Invector.vCharacterController.vThirdPersonController>();
        Assert.IsNotNull(cc);
        cc.Init();
    }

    protected virtual void InitializeTpCamera() {
        if (thirdPersonCamera != null) {
            return;
        }

        thirdPersonCamera = FindObjectOfType<Pandora.PlayerCamera>();
        if (thirdPersonCamera == null) {
            return;
        }

        thirdPersonCamera.SetMainTarget(this.transform);
        thirdPersonCamera.Init();
    }

    protected virtual void InputHandle() {
        MoveInput();
        CameraInput();
        SprintInput();
        StrafeInput();
        JumpInput();
        EscapeInput();
    }

    public virtual void MoveInput() {
        if (!_enabled) {
            return;
        }

        Vector2 input = _movementInput.ReadValue<Vector2>();
        cc.input.x = input.x;
        cc.input.z = input.y;
    }

    protected virtual void CameraInput() {
        if (!cameraMain) {
            Assert.IsNotNull(Camera.main);
            cameraMain = Camera.main;
            cc.rotateTarget = cameraMain.transform;
        }

        if (cameraMain) {
            cc.UpdateMoveDirection(cameraMain.transform);
        }

        if (thirdPersonCamera == null) {
            return;
        }

        if (!_enabled) {
            return;
        }

        Vector2 input = _cameraInput.ReadValue<Vector2>();
        thirdPersonCamera.RotateCamera(input.x, input.y);
    }

    protected virtual void StrafeInput() {
        if (!_enabled) {
            return;
        }

        if (_strafeInput.triggered) {
            cc.Strafe();
        }
    }

    protected virtual void SprintInput() {
        if (!_enabled) {
            cc.Sprint(false);
            return;
        }

        if (_sprintInput.ReadValue<float>() > 0.5f) {
            cc.Sprint(true);
        } else {
            cc.Sprint(false);
        }
    }

    /// <summary>
    /// Conditions to trigger the Jump animation & behavior
    /// </summary>
    /// <returns></returns>
    protected virtual bool JumpConditions() {
        return cc.isGrounded && cc.GroundAngle() < cc.slopeLimit && !cc.isJumping && !cc.stopMove;
    }

    /// <summary>
    /// Input to trigger the Jump
    /// </summary>
    protected virtual void JumpInput() {
        if (!_enabled) {
            return;
        }

        if (_jumpInput.ReadValue<float>() > 0.5f && JumpConditions()) {
            cc.Jump();
        }
    }

    protected virtual void EscapeInput() {
        if (_escapeInput.triggered) {
            if (_enabled) {
                DisableInputs();
                ShowCursor();
            } else {
                EnableInputs();
                HideCursor();
            }
        }
    }

    #endregion
}

}