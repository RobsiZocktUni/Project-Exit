using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The Code of the MainMenu_Script was written by: Wendt Hendrik
/// </summary>
public class MainMenu_Script : MonoBehaviour
{
    // Reference to the FadeToBlack_Script for handling the fade effect
    public FadeToBlack_Script fade;

    /// <summary>
    /// Starts the game by transitioning to the next scene in the buld index
    /// This method is called when the player presses the 'Play' button in the main menu
    /// </summary>
    public void PlayGame()
    {
        // Start the game with a fade transition
        StartCoroutine(PlayGameWithFade());
    }

    /// <summary>
    /// Starts the credits by transitioning to the third scene in the build index
    /// This method is called when the player presses the 'Credits' button in the main menu
    /// </summary>
    public void LoadCredits()
    {
        // Load the third scene in the build order
        // This assumes the scenes are properly ordered in the build settings
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);

        // Alternatively: Load the scene named "Credits_Scene"
        // Ensure "Credits_Scene" is added to the Build Settings in Unity for this to work
        //SceneManager.LoadScene("Credits_scene");
    }

    /// <summary>
    /// Handles the game start with a fade effect before loading the next scene
    /// </summary>
    /// <returns>An IEnumerator for the coroutine</returns>
    private IEnumerator PlayGameWithFade() 
    {
        // Wait until the fade-in is complete
        yield return StartCoroutine(fade.Fade(0.0f, 1.0f)); // Wait until the fade finishes
        
        // Log index for debugging purposes
        //Debug.Log("Game Started");

        // Load the next scene in the build order
        // This assumes the scenes are properly ordered in the build settings
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        // Alternatively: Load the scene named "MainScene"
        // Ensure "MainScene" is added to the Build Settings in Unity for this to work
        //SceneManager.LoadScene("MainScene");
    }

    /// <summary>
    /// Exits the game application
    /// This method is called when the player presses the 'Quit' button in the main menu
    /// </summary>
    public void QuitGame()
    {
        // Log a message to indicate the application is quitting
        Debug.Log("QUIT!");

        // Exit the application
        // Note: This only works in a build version of the game and not in the Unity editor
        Application.Quit();
    }
}
