using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollhouseDoorR_AnimationScript : InteractableObject
{
    #region CodeFromLennart
    //Object that needs to be triggerd in order to play steps
    public AK.Wwise.Event triggerdooropen;
    #endregion

    #region CodeFrom: Wendt Hendrik
    public float openAngle = -40.0f;  // Angle to rotate the door to (in degrees)

    public float timeTillArrival = 2.0f;  // Duration of the animation in seconds

    private Quaternion closedRotation;  // Initial rotation of the door (closed)

    private Quaternion openRotation;  // Initial rotation of the door (open)

    private bool isOpen = false;  // Flag indicating whether the drawer is currently open

    private bool isAnimating = false;  // Flag indicating whether the animation is currently playing
    #region CodeFrom: Beck Jonas
    private bool firstTimeOpening = true;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        #endregion
        // Initialize the closed position to the current local position of the drawer
        closedRotation = transform.rotation;

        // Calculate the open rotation by rotating around the Y-axis (assuming the door swings around the Y-axis)
        openRotation = Quaternion.Euler(0.0f, openAngle, 0.0f);
    }

    /// <summary>
    /// Handles the interaction logic for the door.
    /// </summary>
    /// <remarks>
    /// - If the door is currently animating, no new action will be performed.
    /// - Otherwise, toggles the door's state (open/close) and starts the appropriate animation.
    /// </remarks>
    public override void Interact()
    {
        #endregion

        #region CodeFromBeckJonas
        bool itemInInventory = false;
        foreach (var item in InventoryManager.Instance.InventoryItems)
        {
            if (item.ItemName == "dollhouse key")
            {
                Debug.Log("You used the dollhouse key to open the door");
                if (firstTimeOpening)
                {
                    uiText.SetText("You used the dollhouse key to open the door");
                    firstTimeOpening = false;
                }
                itemInInventory = true;
                #endregion

                #region CodeFrom: Wendt Hendrik
                if (!isAnimating)
                {
                    #region CodeFromLennart
                    triggerdooropen.Post(gameObject);//plays the lock opening sound
                    #endregion

                    if (!isOpen)
                    {
                        // Start the animation to move the drawer to the open or closed position
                        StartCoroutine(RotateDoor(isOpen ? closedRotation : openRotation));
                    }

                    // Toggle the isOpen flag to reflect the new state
                    isOpen = !isOpen;

                    // Provide feedback for the interaction
                    Debug.Log($"The door is now {(isOpen ? "open" : "closed")}.");
                    
                }
                else
                {
                    Debug.Log("The door is already moving.");
                }
                #endregion

                #region CodeFromBeckJonas
                break;
            }
        }
        if (itemInInventory == false)
        {
            Debug.Log("You need to find a key");
            uiText.SetText("You need to find a Key");
        }
        #endregion
    }
    #region CodeFrom: Wendt Hendrik
    /// <summary>
    /// Animates the rotation of the door to the specified target rotation over the defined duration
    /// </summary>
    /// <param name="moveto">The target rotation to move the door to</param>
    /// <returns>An IEnumerator for the coroutine</returns>
    private System.Collections.IEnumerator RotateDoor(Quaternion targetRotation)
    {
        // Store the starting position of the animation
        Quaternion startRotation = transform.rotation;

        // Track the elapsed time since the animation started
        float elapsed = 0.0f;

        // Mark the animation as active
        isAnimating = true;

        // Loop until the door reaches the target rotation
        while (elapsed < timeTillArrival)
        {
            // Interpolate between the starting and target rotations using linear interpolation (Lerp)
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsed / timeTillArrival);

            // Increment the elapsed time by the time passed since the last frame
            elapsed += Time.deltaTime;

            // Wait until the next frame before continuing
            yield return null;
        }

        // Snap the door to the target rotation to ensure precision
        transform.rotation = targetRotation;

        // Mark the animation as finished
        isAnimating = false;
    }
    #endregion
}
