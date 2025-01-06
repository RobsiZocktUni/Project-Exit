using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main Code of the MoveCamera_Script was written by: Wendt Hendrik
/// </summary>
public class MoveCamera_Script : MonoBehaviour
{
    // Initial position for the camera movement (X, Y, Z)
    public Vector3 startPosition = new Vector3(-71.67f, 1.0f, 21.61f);

    // Target position for the camera movement (X, Y, Z)
    public Vector3 targetPosition = new Vector3(-71.67f, 3.412f, 21.61f);

    // Time (in seconds) for the camera to complete the movement
    public float timeTillArrival = 1.0f;


    private Vector3 startMovementPosition;  // Position at the start of the animation
    private Vector3 targetMovementPosition; // Target position to move toward

    private bool isAnimating = false;  // Indicates whether the camera is currently animating

    /// <summary>
    /// Starts the camera movement animation from the defined start position to the target position.
    /// </summary>
    public void CameraMovement()
    {
        // Initialize the start and target positions
        startMovementPosition = startPosition;
        targetMovementPosition = targetPosition;

        // Optionally, set the initial position at the start
        transform.position = startMovementPosition;

        // Begin the movement animation by starting the coroutine
        StartCoroutine(MoveCamera());
    }


    /// <summary>
    /// Animates the camera position from the start position to the target position
    /// </summary>
    /// <returns>An IEnumerator for the coroutine</returns>
    private IEnumerator MoveCamera()
    {
        float elapsed = 0.0f; // Track elapsed time

        isAnimating = true; // Mark the animation as active

        // Gradually interpolate the camera's position over the specified duration
        while (elapsed < timeTillArrival)
        {
            // Linearly interpolate between the starting and target positions based on the elapsed time
            transform.position = Vector3.Lerp(startMovementPosition, targetMovementPosition, elapsed / timeTillArrival);

            elapsed += Time.deltaTime; // Increment elapsed time
            yield return null; // Wait for the next frame
        }

        // Ensure the camera's position snaps precisely to the target position
        transform.position = targetMovementPosition;

        isAnimating = false; // Mark the animation as finished
    }
}
