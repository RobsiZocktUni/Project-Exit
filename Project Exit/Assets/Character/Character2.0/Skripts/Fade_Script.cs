using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Main Code of the Fade_Script was written by: Wendt Hendrik
/// </summary>
public class Fade_Script : MonoBehaviour
{
    // Reference to the Image component used for fading
    public Image fadeImage;

    // Duration (in seconds) for the fade effect to complete
    public float fadeDuration = 1.0f;

    /// <summary>
    /// Fades the image between two alpha values (transparency) over a specified duration
    /// </summary>
    /// <param name="startAlpha">The starting alpha value (0 = fully transparent, 1 = fully opaque)</param>
    /// <param name="endAlpha">The target alpha value to fade to</param>
    /// <returns>An IEnumerator for the coroutine</returns>
    public IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float timer = 0.0f;  // Timer to track the elapsed time during the fade

        // Calculate the alpha value based on linear interpolation between start and end alpha
        Color color = fadeImage.color;

        // Continue fading until the specified duration is reached
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;  // Increment the timer by the time elapsed in the frame

            // Calculate the alpha value based on linear interpolation between start and end alpha
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timer / fadeDuration);

            // Update the image color with the new alpha value
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // Ensure the final alpha value is set to the end value after the fade is complete
        fadeImage.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}
