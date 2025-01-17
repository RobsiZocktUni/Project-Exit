using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The Code of the BackToStart_Script was written by: Wendt Hendrik
/// </summary>
public class BackToStart_Script : MonoBehaviour
{
    /// <summary>
    /// Method is used to load the Homescreen Scene
    /// </summary>
    public void BackToStart()
    {
        // Load the previous scene based on the current scene's build index - 1
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }
}
