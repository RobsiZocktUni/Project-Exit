using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu_Script : MonoBehaviour
{
    /// <summary>
    /// Starts the game by transitioning to the next scene in the buld index
    /// </summary>
    public void PlayGame()
    {
        // Log index for debugging purposes
        Debug.Log("Game Started");

        // Load the next scene in the build order
        // This assumes the scenes are properly ordered in the build settings
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        // Load the scene named "MainScene"
        // Ensure "MainScene" is added to the Build Settings in Unity for this to work
        //SceneManager.LoadScene("MainScene");
    }

    /// <summary>
    /// Exits the game application
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
