using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseInventory_Script : MonoBehaviour
{
    public void CloseInvetory()
    {
        Character_Controller.SetInventoryActive(false);
    }
}
