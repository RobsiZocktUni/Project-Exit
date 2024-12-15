using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndAnimation_Script : MonoBehaviour
{
    public string animationOnTrigger;

    public Character_Controller characterController;  // Reference to the Character_Controller script for enabling/disabling character controls

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            characterController.DisableControls();
            Debug.Log("Animation wurde ausgelöst");

            StartCoroutine(DelayBeforeEnd());
        }
    }

    private IEnumerator DelayBeforeEnd()
    {
        yield return new WaitForSeconds(5.0f);

        // Load the Homescreen Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        // Load the scene named "Homescreen"
        // Ensure "Homescreen" is added to the Build Settings in Unity for this to work
        //SceneManager.LoadScene("Homescreen");
    }
}
