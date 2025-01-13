using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#region CodeFrom: Beck Jonas

// Class to Toggle the inventory between visible and invisible with a Button
public class ToggleInventory : MonoBehaviour
{
    public GameObject Inventory;
    public void Toggle()
    {
        if (Inventory.activeInHierarchy)
        {
            Inventory.SetActive(false);
        }
        else
        {
            Inventory.SetActive(true);
        }
    }
    private void Start()
    {
        Inventory = GameObject.Find("Ui").transform.Find("Inventory").gameObject;
    }
}
#endregion