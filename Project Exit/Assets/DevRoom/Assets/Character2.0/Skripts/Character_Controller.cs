using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    //proberty to control if the player can move; can be set externally if needed
    public bool CanMove { get; private set; } = true;

    //determines if the player is sprinting based on sprint capability and input key
    private bool isSprinting => canSprint && Input.GetKey(sprintKey);

    //determines if the player should initiate crouching based on key input, ground status, and whether a crouch animation is ongoing
    private bool ShouldCrouch => Input.GetKeyDown(crouchKey) && !duringCrouchAnimation & characterController.isGrounded;


    [Header("Functional Options")]
    public bool canSprint = true;  //enables or disables sprint feature
    public bool canCrouch = true;  //enables or disables crouch functionality


    [Header("Controls")]
    public KeyCode sprintKey = KeyCode.LeftShift;  //key assigned for sprinting
    public KeyCode crouchKey = KeyCode.LeftControl;  //key assigned for crouching

    [Header("Parameters: Movement")]
    public float walkSpeed = 6.0f;  //walking speed for the character
    public float sprintSpeed = 9.0f;  //sprinting speed for the character
    public float crouchSpeed = 3.0f;  // crouch speed for the character
    public float gravity = 9.81f;  //gravity force applied to the character

    [Header("Parameters: Look")]
    public float lookSpeedX = 2.0f;  //horizontal look speed
    public float lookSpeedY = 2.0f;  //vertical look speed
    public float upLookLimit = 80.0f;  //maximal upward look angle
    public float lowLookLimit = 70.0f;  //maximal downward look angle

    [Header("Parameters: Crouch")]
    public float crouchHeight = 1.2f;  //height of character while crouching
    public float standingHeight = 1.8f;  //height of character while standing
    public float timeToCrouch = 0.25f;  //time taken for crouch transition animation
    public Vector3 crouchingCenter = new Vector3(0.0f, 0.5f, 0.0f);  //center point of character collider when crouching
    public Vector3 standingCenter = new Vector3(0.0f, 0.0f, 0.0f);  //center point of character collider when standing
    private bool isCrouching;  //tracks if the character is currently crouching
    private bool duringCrouchAnimation;  //tracks if a crouch/stand animation is in progress

    //private variables for player components and states
    private Camera playerCamera;  //camera component to control player view
    private CharacterController characterController;  //CharacterController component for movement

    private Vector3 moveDirection;  //Direction vector for character movement
    private Vector2 currentInput;  //Stores input values for movement

    private float rotationX = 0.0f;  //tracks vertical rotation for clamping look angle

    //PickUp --------------
    InventoryManager InvManager;
    public Camera MainCamera;
    //---------------------------

    //Start is called before the first frame update
    //called once at the start to initialize components and lock cursor
    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();  //find the Camera component in children
        characterController = GetComponent<CharacterController>();  //get the CharacterController component
        Cursor.lockState = CursorLockMode.Locked;  //locks cursor to the center of the screen
        Cursor.visible = false;  //hides cursor

        //PickUp -------------------------
        MainCamera = Camera.main;
        InvManager = this.GetComponent<InventoryManager>();
        //--------------------------------------

    }

    //Update is called once per frame
    //called every frame to update movement and look based on player input
    void Update()
    {
        if (CanMove)
        {
            HandleMovementInput();  //handles WASD or arrow key movement
            HandleMouseLook();  //handles mouse look functionality

            if (canCrouch)  //only checks crouch input if crouch is enabled
                HandleCrouch();  

            ApplyFinalMovement();  //applies movement and gravity to the character
        }


        //PickUp-----------------------
        if (Input.GetMouseButtonDown(0))
        {
            //calculate the center of the screen
            Vector3 screenCenter = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0);
            
            Ray ray = MainCamera.ScreenPointToRay(screenCenter);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
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
        //--------------------------------------
    }

    //handles input for character movement based on walk speed and input axes
    private void HandleMovementInput()
    {
        //get input for movement and multiply by walk speed for forward/backward and strafing
        //get movement input with speed determined by sprint status
        //get movement input while crouching determined by crouch status
        //currentInput = new Vector2((isSprinting ? sprintSpeed : isCrouching ? crouchSpeed :walkSpeed) * Input.GetAxis("Horizontal"), 
        //    (isSprinting ? sprintSpeed : isCrouching ? crouchSpeed : walkSpeed) * Input.GetAxis("Vertical"));

        // Initialize the variable to hold the player's current movement speed
        float currentSpeed;

        // Determine movement speed based on player state
        if (isSprinting)
        {
            currentSpeed = sprintSpeed;  //set speed to sprint speed if the player is sprinting
        }
        else if (isCrouching)
        {
            currentSpeed = crouchSpeed;  //set speed to crouch speed if the player is crouching
        }
        else
        {
            currentSpeed = walkSpeed;  //default to walking speed if the player is neither sprinting nor crouching
        }

        // Calculate the movement input based on speed and player input along the horizontal and vertical axes
        // The Vector2 stores the final movement direction and magnitude for the current frame
        currentInput = new Vector2(
            currentSpeed * Input.GetAxis("Horizontal"),  //apply speed to horizontal input
            currentSpeed * Input.GetAxis("Vertical"));  //apply speed to vertical input


        //preserve vertical movement (gravity) by storing the Y-axis value
        float moveDirectionY = moveDirection.y;

        //set movement direction based on input and character orientation
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.y) + (transform.TransformDirection(Vector3.right) * currentInput.x);

        //restore previous Y-axis movement (gravity effect)
        moveDirection.y = moveDirectionY;

    }

    //handles the mouse input to rotate the camera and character
    private void HandleMouseLook()
    {

        //adjust vertical rotation based on mouse Y-axis input and clamp within look limits
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upLookLimit, lowLookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        //rotate the character horizontally based on mouse X-axis input
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
    }

    //checks if crouch should be toggled and starts crouch or stand transition if necessary
    private void HandleCrouch()
    {
        if (ShouldCrouch)
            StartCoroutine(CrouchStand());

    }


    //applies gravity and moves the character based on the final movement direction
    private void ApplyFinalMovement()
    {
        //apply gravity if the character is not grounded
        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        //move the character based on the calculated direction
        characterController.Move(moveDirection * Time.deltaTime);
    }

    //smoothly transitions between crouching and standing over time, adjusting height and center point of the character collider
    private IEnumerator CrouchStand()
    {
        //prevents standing up if there's an obstacle above the player
        if (isCrouching && Physics.Raycast(playerCamera.transform.position, Vector3.up, 1f))
            yield break;

        duringCrouchAnimation = true;  //flag to prevent multiple crouch animations simultaneously

        float timeElapsed = 0.0f;  //tracks elapsed time for the animation
        float targetHeight = isCrouching ? standingHeight : crouchHeight;  //target height based on current state
        float currentHeight = characterController.height;  //current height of the character
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;  //target center based on state
        Vector3 currentCenter = characterController.center;  //current center of the character collider

        //smoothly transitions height and center over `timeToCrouch` duration
        while (timeElapsed < timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed/timeToCrouch);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed/timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        //sets final height and center values once animation is complete
        characterController.height = targetHeight;
        characterController.center = targetCenter;

        isCrouching = !isCrouching;  //toggle crouch state

        duringCrouchAnimation = false;  //mark animation as complete
    }

}
