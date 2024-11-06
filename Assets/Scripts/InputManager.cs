using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

// Sam Robichaud 
// NSCC Truro 2024
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

public class InputManager : MonoBehaviour
{
    // Script References
    [SerializeField] private PlayerLocomotionHandler playerLocomotionHandler;
    [SerializeField] private CameraManager cameraManager; // Reference to CameraManager
    public InteractionManager interactionManager;

    // Input action
    private PlayerInputActions playerInputActions;

    [Header("Movement Inputs")]
    public float verticalInput;
    public float horizontalInput;
    // public bool jumpInput;
    public PlayerInput playerInput;
    public float moveAmount;
    public Vector2 movementInput;
    private InputAction movement;
    private InputAction jump;
    private InputAction look;
    private InputAction pause;
    private InputAction playerFire;

    [Header("Camera Inputs")]
    public float scrollInput; 
    public Vector2 cameraInput; 

    public bool isPauseKeyPressed;

    [Header("Control Scheme")]
    public Text textControlScheme1;
    public Text textControlScheme2;

    void Awake()
    {
        playerInputActions = new PlayerInputActions();
        if(playerInput == null)
        {
            Debug.LogWarning("PlayerInput is not properly initialized");
        }
        playerInputActions.Player.Move.performed += HandleMovementInput;
        playerInputActions.Player.Jump.performed += HandleJumpInput;
        playerInputActions.Player.Look.performed += HandleCameraInput;
    }
    void OnEnable()
    {
        playerInputActions.Enable();
        interactionManager = FindObjectOfType<InteractionManager>();
        movement = playerInputActions.Player.Move;
        jump = playerInputActions.Player.Jump;
        look = playerInputActions.Player.Look;
        pause = playerInputActions.Player.Pause;
        playerFire = playerInputActions.Player.Fire;

        movement.Enable();
        jump.Enable();
        look.Enable();
        pause.Enable();
        playerFire.Enable();

        playerInputActions.Player.Move.canceled += HandleMovementInput;
        playerInputActions.Player.Jump.canceled += HandleJumpInput;
        playerInputActions.Player.Look.canceled += HandleCameraInput;
    }

    void OnDisable()
    {
       playerInputActions.Disable();
    }
    void Update()
    {
        playerLocomotionHandler.playerVelocity = moveAmount;
        CheckInputType();
    }
    void HandleInteractionType()
    {
        if(playerFire.IsPressed() && interactionManager.InteractionPossible)
        {
            interactionManager.Interact();
        }
    }
    void CheckInputType()
    {
        foreach(InputDevice device in playerInput.devices)
        {
            if(device is Keyboard|| device is Mouse)
            {
                //Debug.Log("Keyboard and Mouse");
                textControlScheme1.text = "Keyboard and Mouse";

            }
            else if(device is Gamepad)
            {
                //Debug.Log("Gamepad");
                textControlScheme2.text = "Gamepad";
            }
        }
    }
    public void HandleAllInputs()
    {
        //CheckInputType();
        HandleSprintingInput();
        HandleInteractionType();
        //HandleCameraInput();
        HandlePauseKeyInput();
    }

    private void HandleCameraInput(InputAction.CallbackContext context)
    {
        cameraInput = context.ReadValue<Vector2>();
        cameraManager.UpdateCameraRotation(cameraInput.x, cameraInput.y);
    }
    private void HandleMovementInput(InputAction.CallbackContext context)
    {
        Debug.Log("Movement " + context.phase);
        if(context.phase == InputActionPhase.Performed || context.phase == InputActionPhase.Started)
        {
            Vector2 inputVector = context.ReadValue<Vector2>();
            horizontalInput = inputVector.x;
            verticalInput = inputVector.y;

            movementInput = new Vector2(horizontalInput, verticalInput);
            movementInput = context.ReadValue<Vector2>();
            moveAmount = Mathf.Clamp01(movementInput.magnitude);
            playerLocomotionHandler.HandleAllPlayerMovement();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            movementInput = Vector2.zero;   
            moveAmount = 0;
            if(playerLocomotionHandler.isSprinting)
            {
                movementInput = Vector2.zero;
                playerLocomotionHandler.isSprinting = false;
            }
            else if(playerLocomotionHandler.isJumping)
            {
                playerLocomotionHandler.isJumping = false;
            }
        }
    }

    private void HandlePauseKeyInput()
    {
        isPauseKeyPressed = Input.GetKeyDown(KeyCode.Escape); 
    }

    private void HandleSprintingInput()
    {
       if(playerInputActions.Player.Sprint.ReadValue<float>() > 0 && movementInput.magnitude > 0.5f)
        {
            playerLocomotionHandler.isSprinting = true;
        }
        else
        {
            playerLocomotionHandler.isSprinting = false;
        }
    }

    private void HandleJumpInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerLocomotionHandler.HandleJump();
        }
    }
}