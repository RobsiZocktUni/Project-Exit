using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Movement : MonoBehaviour
{
    public float mouseSensitivity = 120f;

    //reference for the camera to the player-body
    public Transform playerBody;

    float xRotation = 0f;


    // Start is called before the first frame update
    void Start()
    {
        //lock cursor on the middle on the screen
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {

        //Change on mosue movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;   //Time.deltaTime: time that has gone by since the last time update function was called
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; 

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);   //Player can not look up/down more than 90 degrees


        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
