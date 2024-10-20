using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;

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

        //direction of movement
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);


        //add gravity to the player
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
}
