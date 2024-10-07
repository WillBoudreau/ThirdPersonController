using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Interactions;

// Sam Robichaud 
// NSCC Truro 2024
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

public class InputManager : MonoBehaviour
{
    // Script References
    [SerializeField] private PlayerLocomotionHandler playerLocomotionHandler;
    [SerializeField] private CameraManager cameraManager; // Reference to CameraManager

    // Input action
    private PlayerInputActions playerInputActions;

    [Header("Movement Inputs")]
    public float verticalInput;
    public float horizontalInput;
    // public bool jumpInput;
    public PlayerInput playerInput;
    private string currentControlScheme = "KeyboardMouse";
    public float moveAmount;
    public Vector2 movementInput;
    private InputAction movement;
    private InputAction jump;

    [Header("Camera Inputs")]
    public float scrollInput; // Scroll input for camera zoom
    public Vector2 cameraInput; // Mouse input for the camera

    public bool isPauseKeyPressed = false;

    void Awake()
    {
        playerInputActions = new PlayerInputActions();
        if(playerInput == null)
        {
            Debug.LogWarning("PlayerInput is not properly initialized");
        }
        playerInputActions.Player.Move.performed += HandleMovementInput;
        playerInputActions.Player.Jump.performed += HandleJumpInput;
        playerInputActions.SwitchControlScheme.SwitchScheme.performed += HandleSwitchScheme;
    }
    void OnEnable()
    {
        playerInputActions.Enable();
        movement = playerInputActions.Player.Move;
        jump = playerInputActions.Player.Jump;
        movement.Enable();
        jump.Enable();

        playerInputActions.Player.Move.canceled += HandleMovementInput;
        playerInputActions.Player.Jump.canceled += HandleJumpInput;

    }

    void OnDisable()
    {
       playerInputActions.Disable();
    }
    void Update()
    {
        playerLocomotionHandler.playerVelocity = moveAmount;
    }
    void HandleSwitchScheme(InputAction.CallbackContext context)
    {
        Debug.Log("Switching Control Scheme");
        if (context.performed)
        {
            if (currentControlScheme == "KeyboardMouse")
            {
                Debug.Log("KeyboardMouse connected");
                if (Gamepad.current != null)
                {
                    Debug.Log("Gamepad connected");
                    playerInputActions.Player.Disable();

                    playerInput.SwitchCurrentControlScheme("Gamepad", Gamepad.current);
                    currentControlScheme = "Gamepad";

                    playerInputActions.Player.Enable();
                    Debug.Log("Switched to Gamepad");

                }
                else
                {
                    Debug.LogWarning("No Gamepad connected");
                }
            }
            else
            {
                Debug.Log("Gamepad connected");
                if (Keyboard.current != null && Mouse.current != null)
                {
                    Debug.Log("Keyboard and Mouse connected");
                    playerInputActions.Player.Disable();

                    playerInput.SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current, Mouse.current);
                    currentControlScheme = "Keyboard&Mouse";

                    playerInputActions.Player.Enable();
                    Debug.Log("Switched to Keyboard&Mouse");
                }
                else
                {
                    Debug.LogWarning("No Keyboard or Mouse connected");
                }
            }
        }
    }
    public void HandleAllInputs()
    {
        HandleSprintingInput();
        //HandleCameraInput();
        //HandlePauseKeyInput();
    }

    // private void HandleCameraInput()
    // {        
    //         // Get mouse input for the camera
    //         cameraInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

    //         // Get scroll input for camera zoom
    //         scrollInput = Input.GetAxis("Mouse ScrollWheel");

    //         // Send inputs to CameraManager
    //         cameraManager.zoomInput = scrollInput;
    //         cameraManager.cameraInput = cameraInput;        
    // }

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

    // private void HandlePauseKeyInput()
    // {
    //     isPauseKeyPressed = Input.GetKeyDown(KeyCode.Escape); // Detect the escape key press
    // }

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