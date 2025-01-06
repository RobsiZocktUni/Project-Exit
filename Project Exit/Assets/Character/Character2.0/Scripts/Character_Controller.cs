using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

/// <summary>
/// Main Code of the Character_Controller Script was written by: Wendt Hendrik
/// Parts from Beck Jonas and Hartmann Lennart are marked as regions
/// </summary>
public class Character_Controller : MonoBehaviour
{
    // Proberty to control if the player can move; can be set externally if needed
    public bool CanMove { get; private set; } = true;

    // Determines if the player is sprinting based on sprint capability and input key
    private bool isSprinting => canSprint && Input.GetKey(sprintKey);

    // Determines if the player should initiate crouching based on key input, ground status, and whether a crouch animation is ongoing
    private bool ShouldCrouch => Input.GetKeyDown(crouchKey) && !duringCrouchAnimation & characterController.isGrounded;

    [Header("Functional Options")]
    public bool canSprint = true;  // Enables or disables sprint feature
    public bool canCrouch = true;  // Enables or disables crouch functionality


    [Header("Controls")]
    public KeyCode sprintKey = KeyCode.LeftShift;  // Key assigned for sprinting
    public KeyCode crouchKey = KeyCode.LeftControl;  // Key assigned for crouching

    [Header("Parameters: Movement")]
    public float walkSpeed = 4.0f;  // Walking speed for the character
    public float sprintSpeed = 6.0f;  // Sprinting speed for the character
    public float crouchSpeed = 2.0f;  // Crouch speed for the character
    public float gravity = 9.81f;  // Gravity force applied to the character

    [Header("Parameters: Look")]
    public float lookSpeedX = 2.0f;  // Horizontal look speed
    public float lookSpeedY = 2.0f;  // Vertical look speed
    public float upLookLimit = 80.0f;  // Maximal upward look angle
    public float lowLookLimit = 70.0f;  // Maximal downward look angle

    [Header("Parameters: Crouch")]
    public float crouchHeight = 1.3f;  // Height of character while crouching
    public float standingHeight = 2.0f;  // Height of character while standing
    public float timeToCrouch = 0.25f;  // Time taken for crouch transition animation
    public Vector3 crouchingCenter = new Vector3(0.0f, 0.6f, 0.0f);  // Center point of character collider when crouching
    public Vector3 standingCenter = new Vector3(0.0f, 0.0f, 0.0f);  // Center point of character collider when standing
    private bool isCrouching;  // Tracks if the character is currently crouching
    private bool duringCrouchAnimation;  // Tracks if a crouch/stand animation is in progress

    // Private variables for player components and states
    private Camera playerCamera;  // Camera component to control player view
    private CharacterController characterController;  // CharacterController component for movement

    private Vector3 moveDirection;  // Direction vector for character movement
    private Vector2 currentInput;  // Stores input values for movement

    private float rotationX = 0.0f;  // Tracks vertical rotation for clamping look angle

    private bool controlsEnabled = true; // Status of the controller

    #region CodeFrom BeckJonas
    //PickUp --------------
    InventoryManager InvManager;
    private Camera MainCamera;
    //---------------------------

    //Inventory --------------
    [Header("Parameters: InventoryUi")]
    public GameObject InventoryUi;  //Ui Inventory Gameobject
                                    //-------------------------
    #endregion

    #region CodeFrom: HartmannLennart
    //Object that needs to be triggerd in order to play steps
    public AK.Wwise.Event triggerSteps;
    //variables that track and time the playback speed of a single step
    public float stepsInterval = 0.5f;
    //using System.Diagnostics stopwatch to time the intervall between steps
    private Stopwatch stopwatch = new Stopwatch();
    #endregion


    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();  // Get the Camera component in children
        characterController = GetComponent<CharacterController>();  // Get the CharacterController component
        Cursor.lockState = CursorLockMode.Locked;  // Locks cursor to the center of the screen
        Cursor.visible = false;  // Hides cursor

        // Load MouseSensitivity from PlayerPrefs and apply it to look speed
        float savedSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 2.0f);
        lookSpeedX = savedSensitivity;
        lookSpeedY = savedSensitivity;

        #region CodeFrom BeckJonas
        //PickUp -------------------------
        MainCamera = Camera.main;
        InvManager = this.GetComponent<InventoryManager>();
        //--------------------------------------
        #endregion

        #region CodeFrom HartmannLennart
        stopwatch.Start(); //start Stopwatch
        #endregion
    }

    /// <summary>
    /// Update is called once per frame
    /// Called every frame to update movement and look based on player input
    /// </summary>
    void Update()
    {
        // Do not process input or movements when the game is paused or inventory is activated
        if (PauseMenu_Script.IsPaused || InventoryUi.activeSelf || EndAnimation_Script.gameEnded)
        {
            return;
        }

        if (CanMove && controlsEnabled)
        {
            HandleMovementInput();  // Handles WASD or arrow key movement
            HandleMouseLook();  // Handles mouse look functionality

            if (canCrouch)  // Only checks crouch input if crouch is enabled
                HandleCrouch();  

            ApplyFinalMovement();  // Applies movement and gravity to the character
        }

        #region CodeFrom BeckJonas
        if (Input.GetMouseButtonDown(0))
        {
            //calculate the center of the screen
            Vector3 screenCenter = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0);
            
            Ray ray = MainCamera.ScreenPointToRay(screenCenter);
            if (Physics.Raycast(ray, out RaycastHit hit, 3f))
            {
                if (hit.collider.gameObject.GetComponent<KeyItem>())
                {
                    InventoryManager.Instance.AddItem(hit.collider.gameObject.GetComponent<KeyItem>());
                }
                if (hit.collider.gameObject.GetComponent<InteractableObject>())
                {
                    hit.collider.gameObject.GetComponent<InteractableObject>().Interact();
                }

            }
        }

        //ToggleInventory-----------------------
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (InventoryUi.activeSelf)
            {
                EnableControls();
                InventoryUi.SetActive(false);
            }
            else
            {
                DisableControls();
                InventoryUi.SetActive(true);
            }
        }
        #endregion
    }

    /// <summary>
    /// Handles input for character movement based on walk speed and input axes
    /// </summary>
    private void HandleMovementInput()
    {
        // Initialize the variable to hold the player's current movement speed
        float currentSpeed;

        // Determine movement speed based on player state
        if (isSprinting)
        {
            currentSpeed = sprintSpeed;  // Set speed to sprint speed if the player is sprinting
        }
        else if (isCrouching)
        {
            currentSpeed = crouchSpeed;  // Set speed to crouch speed if the player is crouching
        }
        else
        {
            currentSpeed = walkSpeed;  // Default to walking speed if the player is neither sprinting nor crouching
        }

        // Calculate the movement input based on speed and player input along the horizontal and vertical axes
        currentInput = new Vector2(
            currentSpeed * Input.GetAxis("Horizontal"),  // Apply speed to horizontal input
            currentSpeed * Input.GetAxis("Vertical"));  // Apply speed to vertical input

        // Preserve vertical movement (gravity) by storing the Y-axis value
        float moveDirectionY = moveDirection.y;

        // Set movement direction based on input and character orientation
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.y) + (transform.TransformDirection(Vector3.right) * currentInput.x);

        // Restore previous Y-axis movement (gravity effect)
        moveDirection.y = moveDirectionY;

    }

    /// <summary>
    /// Handles the mouse input to rotate the camera and character
    /// </summary>
    private void HandleMouseLook()
    {
        // Adjust vertical rotation based on mouse Y-axis input and clamp within look limits
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upLookLimit, lowLookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        // Rotate the character horizontally based on mouse X-axis input
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
    }

    /// <summary>
    /// Checks if crouch should be toggled and starts crouch or stand transition if necessary
    /// </summary>
    private void HandleCrouch()
    {
        if (ShouldCrouch)
            StartCoroutine(CrouchStand());
    }

    /// <summary>
    /// Applies gravity and moves the character based on the final movement direction
    /// </summary>
    private void ApplyFinalMovement()
    {
        // Apply gravity if the character is not grounded
        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        // Move the character based on the calculated direction
        characterController.Move(moveDirection * Time.deltaTime);
        PLayStepsAudio();
    }

    /// <summary>
    /// Smoothly transitions between crouching and standing over time, adjusting height and center point of the character collider
    /// </summary>
    /// <returns>A coroutine that performs the transition over time</returns>
    private IEnumerator CrouchStand()
    {
        // Prevents standing up if there's an obstacle above the player
        if (isCrouching && Physics.Raycast(playerCamera.transform.position, Vector3.up, 1f))
            yield break;

        duringCrouchAnimation = true;  // Flag to prevent multiple crouch animations simultaneously

        float timeElapsed = 0.0f;  // Tracks elapsed time for the animation
        float targetHeight = isCrouching ? standingHeight : crouchHeight;  //Target height based on current state
        float currentHeight = characterController.height;  // Current height of the character
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;  //Target center based on state
        Vector3 currentCenter = characterController.center;  // Current center of the character collider

        // Smoothly transitions height and center over `timeToCrouch` duration
        while (timeElapsed < timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed/timeToCrouch);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed/timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Sets final height and center values once animation is complete
        characterController.height = targetHeight;
        characterController.center = targetCenter;

        // Toggle crouch state
        isCrouching = !isCrouching;  // Toggle crouch state
        duringCrouchAnimation = false;  // Mark animation as complete
    }

    /// <summary>
    /// Updates the mouse sensitivity for both horizontal and vertical look speeds
    /// This method adjusts the sensitivity for the player's camera controls based on the input value
    /// </summary>
    /// <param name="sensitivity">The new sensitivity value to apply to both horizontal and vertical mouse movements</param>
    public void UpdateMouseSensitivity(float sensitivity)
    {
        lookSpeedX = sensitivity;
        lookSpeedY = sensitivity;
    }

    /// <summary>
    /// Disables player controls
    /// </summary>
    public void DisableControls()
    {
        CanMove = false;
        controlsEnabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;   
    }

    /// <summary>
    /// Enables controls
    /// </summary>
    public void EnableControls()
    {
        CanMove = true;
        controlsEnabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    #region CodeFrom HartmannLennart
    private void PLayStepsAudio()
    {
        //checks if the character is not on the ground, and wont play audio if so
        if (!characterController.isGrounded)
            return;


        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, 0, vertical);
        //checks if the character has moved enough to play a step sound
        if (movement.magnitude > 0.1f)
        {
            if (stopwatch.Elapsed.TotalSeconds >= stepsInterval)
            {
                stopwatch.Restart();    //resets stopwatch
                triggerSteps.Post(gameObject);  //triggers a Wwise post event
            }
        }
    }
    #endregion
}
