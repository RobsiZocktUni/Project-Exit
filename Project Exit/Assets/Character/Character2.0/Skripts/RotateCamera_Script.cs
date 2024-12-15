using System.Collections;
using UnityEngine;

public class RotateCamera_Script : MonoBehaviour
{
    // Initial rotation angles for the camera (X, Y, Z)
    public Vector3 startAngles = new Vector3(-10.0f, 70.0f, 50.0f);

    // Target rotation angles for the camera (X, Y, Z)
    public Vector3 targetAngles = new Vector3(0.0f, 90.0f, 0.0f);

    // Time (in seconds) for the camera to complete the rotation
    public float timeTillArrival = 2.0f; 

    private Quaternion startRotation;  // Starting rotation of the camera
    private Quaternion targetRotation;  // Target rotation of the camera

    private bool isAnimating = false;  // Indicates whether the camera is currently animating


    /// <summary>
    /// Starts the camera rotation animation
    /// </summary>
    public void CameraRotation()
    {
        // Set the cameras rotation to the specified start angles
        transform.rotation = Quaternion.Euler(startAngles);

        // Store the current rotation as the starting rotation
        startRotation = transform.rotation;

        // Define the target rotation using the specified target angles
        targetRotation = Quaternion.Euler(targetAngles);

        // Begin the rotation animation by starting the coroutine
        StartCoroutine(RotateCamera());
    }
    
    /// <summary>
    /// Animates the rotation of the camera to the target rotation over the defined duration.
    /// </summary>
    private IEnumerator RotateCamera()
    {
        float elapsed = 0.0f;  // Track elapsed time since the animation began

        isAnimating = true;  // Mark the animation as active

        // Gradually interpolate the cameras rotation over duration
        while (elapsed < timeTillArrival)
        {
            // Linearly interpolate between the starting and target rotations based on the elapsed time
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsed / timeTillArrival);

            elapsed += Time.deltaTime; // Increment elapsed time

            yield return null; // Wait for the next frame
        }

        // Snap the rotation to the exact target to ensure precision
        transform.rotation = targetRotation;

        isAnimating = false; // Mark the animation as finished
    }
}
