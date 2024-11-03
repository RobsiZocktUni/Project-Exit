using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Bringupsettings : MonoBehaviour
{

    //class to get in the slider settings
    public GameObject setting;
    public bool issettingactive;

    void Update()
    {
        // open up settings when tab is pressed
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (issettingactive == false)
            {
                Pause();

            }
            else
            {
                Resume();
            }


        }
    }


    public void Pause()
    {
        setting.SetActive(true);
        issettingactive = true;
        this.GetComponent<Cam_Movement>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        setting.SetActive(false);
        issettingactive = false;
        this.GetComponent<Cam_Movement>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }


}
