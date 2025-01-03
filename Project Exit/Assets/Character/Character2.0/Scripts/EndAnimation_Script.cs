using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;

/// <summary>
/// Main Code of the EndAnimation_Script was written by: Wendt Hendrik
/// </summary>
public class EndAnimation_Script : InteractableObject
{
    [Header("References")]
    public Character_Controller characterController;  // Reference to the Character_Controller script for enabling/disabling character controls
    public Image fadeImage;  // Reference to the Image component used for fading to black
    public Canvas animationCanvas;  // Reference to the Canvas component used for fading to black

    [Header("Animation")]
    public float fadeDuration = 3.0f;  // Duration (in seconds) for the fade effect to complete

    // Static game ended state accessible to other scripts
    public static bool gameEnded { get; private set; } = false;

    /// <summary>
    /// Called when the player clicks on the object with this script attached
    /// Disables character controls and starts fade- out effect
    /// </summary>
    private void OnMouseDown()
    {
        // Check if the characterController has been assigned in the Inspector
        if (characterController != null)
        {
            animationCanvas.gameObject.SetActive(true);  // Activate Canvas for end animation

            gameEnded = true;

            // Disable character controls when the player triggers the event
            characterController.DisableControls();
            Debug.Log("Animation wurde ausgelöst");

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Start the coroutine to delay the scene change
            StartCoroutine(DelayBeforeEnd());
        }

    }

    /// <summary>
    /// Waits for a delay before changing to the next scene
    /// </summary>
    /// <returns></returns>
    private IEnumerator DelayBeforeEnd()
    {
        // Wait for 5 seconds before continuing the logic
        yield return new WaitForSeconds(2.0f);

        // Fade from transparent to opaque (black)
        yield return StartCoroutine(Fade(0.0f, 1.0f));

        // Load the Homescreen Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameEnded = false;

        // Alternatively: Load the scene named "Homescreen"
        // Ensure "Homescreen" is added to the Build Settings in Unity for this to work
        // SceneManager.LoadScene("Homescreen");
    }

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

    ///// <summary>
    ///// This function is called when another collider enters the trigger zone
    ///// </summary>
    ///// <param name="other">The collider that entered the trigger zone</param>
    //private void OnTriggerEnter(Collider other)
    //{
    //    // Check if the object entering the trigger is tagged as "Player"
    //    if (other.CompareTag("Player"))
    //    {
    //        // Disable character controls when the player triggers the event
    //        characterController.DisableControls();
    //        Debug.Log("Animation wurde ausgelöst");

    //        // Start the coroutine to delay the scene change
    //        StartCoroutine(DelayBeforeEnd());
    //    }
    //}
}
