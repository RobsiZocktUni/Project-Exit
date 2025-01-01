using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// Main Code of the EndAnimation_Script was written by: Wendt Hendrik
/// </summary>
public class EndAnimation_Script : MonoBehaviour
{
    // Name of the animation to be triggered (currently not used in the script)
    public string animationOnTrigger;

    // Reference to the Character_Controller script for enabling/disabling character controls
    public Character_Controller characterController;

    /// <summary>
    /// This function is called when another collider enters the trigger zone
    /// </summary>
    /// <param name="other">The collider that entered the trigger zone</param>
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Disable character controls when the player triggers the event
            characterController.DisableControls();
            Debug.Log("Animation wurde ausgelöst");

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
        yield return new WaitForSeconds(5.0f);

        // Load the Homescreen Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        // Alternatively: Load the scene named "Homescreen"
        // Ensure "Homescreen" is added to the Build Settings in Unity for this to work
        // SceneManager.LoadScene("Homescreen");
    }
}
