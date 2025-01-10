using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main Code of the StartAnimation_Script was written by: Wendt Hendrik
/// </summary>
public class StartAnimation_Script : MonoBehaviour
{
    [Header("References")]
    public RotateCamera_Script RotateCamera;  // Reference to the RotateCamera script for rotating the camera
    public MoveCamera_Script MoveCamera;  // Reference to the MoveCamera script for moving the camera
    public Character_Controller characterController;  // Reference to the Character_Controller script for enabling/disabling character controls
    public Fade_Script fade;
    public GameObject FadeManager;

    public static bool animationsDone { get; private set; } = false;

    /// <summary>
    /// Initializes the script and starts the animation sequence.
    /// </summary>
    private void Start()
    {
        // Get the RotateCamera script attached to this GameObject
        RotateCamera = GetComponent<RotateCamera_Script>();

        // Get the MoveCamera script attached to this GameObject
        MoveCamera = GetComponent<MoveCamera_Script>();

        // Disable the character's controls at the start of the sequence
        characterController.DisableControls();

        // Start the first animation with a delay
        StartCoroutine(DelayBeforeStart());

        // Start the second animation with a delay after the first
        StartCoroutine(DelayBeforeSecondAnimation());
    }

    /// <summary>
    /// Waits for a specified delay before triggering the first animation (camera rotation).
    /// </summary>
    /// <returns>An IEnumerator for the coroutine</returns>
    private IEnumerator DelayBeforeStart()
    {
        // Wait for the specified delay time
        //yield return new WaitForSeconds(1.0f);

        yield return StartCoroutine(fade.Fade(1.0f, 0.0f));

        // Trigger the camera rotation animation
        RotateCamera.CameraRotation();

        FadeManager.gameObject.SetActive(false);
    }

    /// <summary>
    /// Waits for a specified delay before triggering the second animation (camera movement),
    /// and then enables character controls after another delay.
    /// </summary>
    /// <returns></returns>
    private IEnumerator DelayBeforeSecondAnimation()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(6.0f);

        // Trigger the camera movement animation
        MoveCamera.CameraMovement();

        // Wait for an additional delay before enabling the character controller
        yield return new WaitForSeconds(2.0f);

        // Enable the character's controls after the animations are complete
        characterController.EnableControls();

        animationsDone = true;
    }
}
