using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableDrawerR_AnimationScript : MonoBehaviour
{
    public Vector3 openPositionOffset;  // Offset to apply to the closed position to determine the open position

    public float timeTillArrival = 1.0f;  // Duration of the animation in seconds

    private Vector3 closedPosition;  // Local position of the drawer when closed

    private Vector3 openPosition;  // Local position of the drawer when open

    private bool isOpen = false;  // Flag indicating whether the drawer is currently open

    private bool isAnimating = false;  // Flag indicating whether the animation is currently playing

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the closed position to the current local position of the drawer
        closedPosition = transform.localPosition;

        // Calculate the open position by adding the offset to the closed position
        openPosition = closedPosition + openPositionOffset;
    }

    /// <summary>
    /// Called when the user clicks on the object with the mouse
    /// </summary>
    private void OnMouseDown()
    {
        // If an animation is already running, do nothing
        if (!isAnimating)
        {
            // Start the animation to move the drawer to the open or closed position
            StartCoroutine(MoveDrawer(isOpen ? closedPosition : openPosition));

            // Toggle the isOpen flag to reflect the new state
            isOpen = !isOpen;
        }
    }

    /// <summary>
    /// Animates the drawer to the specified target position over the defined duration
    /// </summary>
    /// <param name="moveto">The target position to move the drawer to</param>
    /// <returns>An IEnumerator for the coroutine</returns>
    private System.Collections.IEnumerator MoveDrawer(Vector3 moveto)
    {
        // Store the starting position of the animation
        Vector3 startPos = transform.localPosition;

        // Track the elapsed time since the animation started
        float elapsed = 0.0f;

        // Mark the animation as active
        isAnimating = true;

        // Loop until the drawer reaches the target position
        do
        {
            // Interpolate between the starting and target positions using using linear interpolation (Lerp)
            transform.localPosition = Vector3.Lerp(startPos, moveto, elapsed / timeTillArrival);

            // Increment the elapsed time by the time passed since the last frame
            elapsed += Time.deltaTime;

            // Wait until the next frame before continuing
            yield return null;

        } while (Vector3.Distance(transform.localPosition, moveto) >= 0.001f);  // Continue the loop until the drawer is sufficiently close to the target position

        // Snap the drawer to the target position to ensure precision
        transform.localPosition = moveto;

        // Mark the animation as finished
        isAnimating = false;
    }
}
