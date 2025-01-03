using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Credits_Script : MonoBehaviour
{
    public float scrollSpeed = 70f;       // Speed of scrolling
    public float endYPos;                 // Position where the credits end
    private RectTransform rectTransform;  // Reference to the RectTransform of the credits
    private bool creditsEnded = false;    // Flag to indicate if credits have ended

    public Image fadeImage;
    public Canvas animationCanvas;  // Reference to the Canvas component used for fading to black
    public float fadeDuration = 5.0f;  // Duration (in seconds) for the fade effect to complete

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();  // Get the RectTransform component

        // Calculate endYPosition dynamically
        float canvasHeight = transform.parent.GetComponent<RectTransform>().rect.height; // Height of the parent Canvas
        float textHeight = rectTransform.rect.height; // Height of the credits text RectTransform
        endYPos = textHeight - canvasHeight; // When the text scrolls completely out of view
    }

    // Update is called once per frame
    void Update()
    {
        if (!creditsEnded)
        {
            // Move the credits text upwards
            rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);

            // Check if the credits have reached the end position
            if (rectTransform.anchoredPosition.y >= endYPos)
            {
                creditsEnded = true;
            }
        }

        if (creditsEnded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Start fading to black
                StartCoroutine(HomescreenLoader());
            }
        }
    }

    private IEnumerator HomescreenLoader()
    {
        yield return StartCoroutine(Fade(0.0f, 1.0f));

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);

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
}
