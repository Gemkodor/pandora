using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

namespace Pandora {

public class ThirdPersonInput : MonoBehaviour {
    [Header("Controller Input")]
    [SerializeField] InputAction _movementInput;
    [SerializeField] InputAction _jumpInput;
    [SerializeField] InputAction _strafeInput;
    [SerializeField] InputAction _sprintInput;

    [Header("Camera Input")]
    [SerializeField] InputAction _cameraInput;

    [HideInInspector] public Invector.vCharacterController.vThirdPersonController cc;
    [HideInInspector] public ThirdPersonCamera tpCamera;
    [HideInInspector] public Camera cameraMain;

    protected virtual void Start() {
        InitilizeController();
        InitializeTpCamera();
    }

    private void OnEnable()
    {
        _movementInput.Enable();
        _jumpInput.Enable();
        _strafeInput.Enable();
        _sprintInput.Enable();
        _cameraInput.Enable();
    }

    private void OnDisable()
    {
        _movementInput.Disable();
        _jumpInput.Disable();
        _strafeInput.Disable();
        _sprintInput.Disable();
        _cameraInput.Disable();
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

    #region Basic Locomotion Inputs

    protected virtual void InitilizeController() {
        cc = GetComponent<Invector.vCharacterController.vThirdPersonController>();

        if (cc != null)
            cc.Init();
    }

    protected virtual void InitializeTpCamera() {
        if (tpCamera == null) {
            tpCamera = FindObjectOfType<Pandora.ThirdPersonCamera>();
            if (tpCamera == null)
                return;
            if (tpCamera) {
                tpCamera.SetMainTarget(this.transform);
                tpCamera.Init();
            }
        }
    }

    protected virtual void InputHandle() {
        MoveInput();
        CameraInput();
        SprintInput();
        StrafeInput();
        JumpInput();
    }

    public virtual void MoveInput() {
        Vector2 input = _movementInput.ReadValue<Vector2>();
        cc.input.x = input.x;
        cc.input.z = input.y;
    }

    protected virtual void CameraInput() {
        if (!cameraMain) {
            if (!Camera.main) Debug.Log("Missing a Camera with the tag MainCamera, please add one.");
            else {
                cameraMain = Camera.main;
                cc.rotateTarget = cameraMain.transform;
            }
        }

        if (cameraMain) {
            cc.UpdateMoveDirection(cameraMain.transform);
        }

        if (tpCamera == null)
            return;

        Vector2 input = _cameraInput.ReadValue<Vector2>();
        tpCamera.RotateCamera(input.x, input.y);
    }

    protected virtual void StrafeInput() {
        if (_strafeInput.triggered)
            cc.Strafe();
    }

    protected virtual void SprintInput() {
        if (_sprintInput.ReadValue<float>() > 0.5f)
            cc.Sprint(true);
        else 
            cc.Sprint(false);
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
        if (_jumpInput.ReadValue<float>() > 0.5f && JumpConditions())
            cc.Jump();
    }

    #endregion
}

}