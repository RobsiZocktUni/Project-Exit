using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// The code of the CloseInventory_Script was written by: Wendt Hendrik
/// </summary>
public class CloseInventory_Script : MonoBehaviour
{
    /// <summary>
    /// Closes the inventory UI and deactivates any related functionality
    /// </summary>
    public void CloseInvetory()
    {
        // Deactivate the inventory in the character controller
        Character_Controller.SetInventoryActive(false);
    }
}
