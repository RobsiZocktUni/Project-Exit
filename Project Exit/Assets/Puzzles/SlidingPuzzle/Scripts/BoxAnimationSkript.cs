using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main Code of the BoxAnimationSkript was written by: Wendt Hendrik
/// </summary>
public class BoxAnimationSkript : MonoBehaviour
{
    #region CodeFromLennart
    public AK.Wwise.Event triggerMoveTop;
    #endregion
    private Vector3 from = new Vector3(-68.0f, 2.105f, 29.363f);  // The initial position of the box before animation starts
    private Vector3 to = new Vector3(-67.45f, 2.105f, 29.363f);  // The target position of the box after the animation finishes

    private float timeInSec = 1.5f;  // The time duration of the animation in seconds

    public bool isAnimating = false;  // A flag indicating whether the animation is currently playing

    /// <summary>
    /// Opens the box by animating it to the target position
    /// </summary>
    public void BoxOpen()
    {
        // If the box is already animating, exit the function to avoid starting a new animation
        if (isAnimating) return;

        triggerMoveTop.Post(gameObject);
        // Start the animation coroutine to move the box to the target position
        StartCoroutine(MoveToPos(to, timeInSec));
    }

    /// <summary>
    /// Animates the box to the specified target position over a given duration
    /// </summary>
    /// <param name="moveto">The target position the box should move to</param>
    /// <param name="duration">The duration of the animation in seconds</param>
    /// <returns>An IEnumerator used for the coroutine</returns>
    private IEnumerator MoveToPos(Vector3 moveto, float duration)
    {
        // Mark the animation as active
        isAnimating = true;

        // Store the initial position of the box
        Vector3 startPosition = transform.position;

        // Variable to track the elapsed time of the animation
        float elapsed = 0.0f;

        // While the elapsed time is less than the duration, keep animating the box
        while (elapsed < duration)
        {
            // Calculate the interpolation factor (t) based on the elapsed time
            float t = elapsed / duration;

            // Interpolate between the start position and the target position
            transform.position = Vector3.Lerp(startPosition, moveto, t);

            // Increment the elapsed time by the time passed since the last frame
            elapsed += Time.deltaTime;

            // Wait until the next frame
            yield return null;
        }

        // Ensure the box reaches the target position at the end of the animation
        transform.position = moveto;

        // Mark the animation as finished
        isAnimating = false;
    }
}
