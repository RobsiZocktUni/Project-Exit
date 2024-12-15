using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Movement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 6f;  //normal walk speed
    public float sprintMultiplier = 1.5f;  //determines how much faster the player moves while sprinting.
    public float gravity = -9.81f;  //gravity for ground detection
    private float currentSpeed; //temporarily stores the modified speed when sprinting is active

    public Transform ground_Check;
    public float ground_Distance = 0.4f;   //sphere for ground detection
    public LayerMask ground_Mask;

    Vector3 velocity;
    bool is_Grounded;

    // Update is called once per frame
    void Update()
    {
        //check if player is on the ground
        is_Grounded = Physics.CheckSphere(ground_Check.position, ground_Distance, ground_Mask);
        if(is_Grounded && velocity.y < 0)
        {
            velocity.y = -2f;   //force the player on the ground
        }

        //Input for z and x axes
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Sprint button using left shift
        currentSpeed = speed;
        //checks if the Left Shift key is held down. When true, it multiplies the speed by sprintMultiplier
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= sprintMultiplier; 
        }


        //direction of movement
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * currentSpeed * Time.deltaTime);

        //add gravity to the player
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
}
