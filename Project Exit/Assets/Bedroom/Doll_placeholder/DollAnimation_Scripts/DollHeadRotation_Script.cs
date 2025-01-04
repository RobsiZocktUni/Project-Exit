using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main code of the DollHeadRotation_Script was written by Hendrik Wendt
/// </summary>
public class DollHeadRotation_Script : MonoBehaviour
{
    public Transform playerCamera;  // Reference to the player camera
    public Transform dollHead;      // Reference to the doll head

    public float rotationSpeed = 6.0f;  // Rotation speed
    public float verLimit = 0.0f;      // aximum vertical rotation limit in degrees (up/down movement)
    public float horLimit = 45.0f;      // Maximum horizontal rotation limit in degrees (left/right movement)

    private Quaternion initialRotation; // Stores the initial rotation of the doll's head

    private void Start()
    {
        // Save the initial local rotation of the doll's head
        // Reference point for all future rotations
        initialRotation = dollHead.localRotation;
    }

    private void Update()
    {
        // Calculate the direction vector from the doll's head to the player's camera
        Vector3 targetDirection = playerCamera.position - dollHead.position;

        // Ensure the direction vector has a minimum length to avoid issues
        if (targetDirection.magnitude < 0.1f) return;

        // Calculate the desired rotation needed to look at the target direction
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        // Constrain the rotation to stay within the specified limits relative to the initial rotation
        Quaternion constrainedRotation = ConstrainRotation(targetRotation);

        // Rotate the doll's head towards the constrained rotation
        dollHead.localRotation = Quaternion.Slerp(dollHead.localRotation, constrainedRotation, Time.deltaTime * rotationSpeed);
    }

    /// <summary>
    /// Constrains the target rotation to remain within vertical and horizontal limits relative to the initial rotation
    /// </summary>
    /// <param name="targetRotation"></param>
    /// <returns></returns>
    private Quaternion ConstrainRotation(Quaternion targetRotation)
    {
        // Calculate the ralative rotation from the initial rotation
        Quaternion relRotation = Quaternion.Inverse(initialRotation) * targetRotation;

        // Convert the relative rotation into Euler angles (pitch, yaw, roll)
        Vector3 relEuler = relRotation.eulerAngles;

        // Adjust angles to account for wrapping around 360 degrees
        // Example: 350 degrees becomes -10 degrees for easier clamping
        relEuler.x = (relEuler.x > 180) ? relEuler.x - 360 : relEuler.x;
        relEuler.y = (relEuler.y > 180) ? relEuler.y - 360 : relEuler.y;

        // Clamp the rotation within the limity
        relEuler.x = Mathf.Clamp(relEuler.x, -verLimit, verLimit); // Vertical rotation (pitch)
        relEuler.y = Mathf.Clamp(relEuler.y, -horLimit, horLimit); // Horizontal rotation (yaw)

        // Convert the clamped Euler angles back into a Quaternion
        Quaternion clampedRelativeRotation = Quaternion.Euler(relEuler);

        // Combine the clamped relative rotation with the initial rotation
        return initialRotation * clampedRelativeRotation;
    }
}