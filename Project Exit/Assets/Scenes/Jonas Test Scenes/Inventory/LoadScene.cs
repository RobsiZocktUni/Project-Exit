using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    void OnGUI()
    {
        if (GUI.Button(new Rect(20, 30, 150, 30), "InventoryTestScene"))
        {
            SceneManager.LoadScene("Cube Test Scene", LoadSceneMode.Additive);
            this.enabled = false;
        }

        if (GUI.Button(new Rect(20, 60, 150, 30), "DevRoomTest"))
        {
            SceneManager.LoadScene("YourScene", LoadSceneMode.Additive);
            this.enabled = false;
        }
    }
}