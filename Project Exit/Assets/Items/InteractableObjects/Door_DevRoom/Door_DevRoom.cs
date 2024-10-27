using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_DevRoom : InteractableObject
{
    public Transform Parent;
    public float rotationSpeed = 2f; // Speed of rotation
    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private bool isRotating = false;

    void Start()
    {
        initialRotation = Parent.rotation;
        targetRotation = initialRotation * Quaternion.Euler(0, 90, 0); // Rotate 90 degrees around the Y axis
    }

    void Update()   //Handles the Door rotation
    {
        if (isRotating)
        {
            // Lerp the rotation from initial to target
            Parent.rotation = Quaternion.Lerp(Parent.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            // Check if the rotation is close enough to stop
            if (Quaternion.Angle(Parent.rotation, targetRotation) < 0.1f)
            {
                Parent.rotation = targetRotation; // Snap to target rotation
                isRotating = false;
            }
        }
    }
    public override void Interact() //Checks if Key is in Inventory and opens the door if it is
    {
        bool itemInInventory = false;
        foreach (var item in InventoryManager.Instance.InventoryItems)
        {
            if (item.ItemName == "Key_DevRoom")
            {
                Debug.Log("You used the DevRoom Key to open the door");
                itemInInventory = true;
                InventoryManager.Instance.DeleteItem(item);
                isRotating = true;
                break;
            }
        }
        if (itemInInventory == false)
        {
            Debug.Log("You need to find a Key");
        }
    }
}
