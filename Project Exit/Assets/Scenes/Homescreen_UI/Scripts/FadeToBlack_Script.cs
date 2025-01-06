using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Main Code of the FadeToBlack_Script was written by: Wendt Hendrik
/// </summary>
public class FadeToBlack_Script : MonoBehaviour
{
    // Reference to the Image component used for fading to black
    public Image fadeImage;

    // Duration (in seconds) for the fade effect to complete
    public float fadeDuration = 3.0f;

    /// <summary>
    /// Fades the image between two alpha values (transparency) over a specified duration
    /// This is used to fade to black
    /// </summary>
    /// <param name="startAlpha">The starting alpha value (0 = fully transparent, 1 = fully opaque)</param>
    /// <param name="endAlpha">The target alpha value to fade to</param>
    /// <returns>An IEnumerator for the coroutine</returns>
    public IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float timer = 0.0f;  // Timer to track the elapsed time during the fade

        // Get the current color of the fade image (we'll keep the RGB values fixed)
        Color color = fadeImage.color;

        // Continue fading until the specified duration is reached
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;  // Increment the timer by the time elapsed in the frame

            // Calculate the alpha value based on linear interpolation between start and end alpha
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timer / fadeDuration);

            // Update the image color with the new alpha value
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            //fadeImage.color = new Color(color.r, color.g, color.b, alpha);

            // Wait until the next frame before continuing the loop
            yield return null;
        }

        // Ensure the final alpha value is set to the end value after the fade is complete
        fadeImage.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}
