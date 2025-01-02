using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#region CodeFrom: Beck Jonas
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;    // Singelton
    public List<KeyItem> InventoryItems = new List<KeyItem>();    // List with all the items that are currently in the inventory.
    public Transform inventoryListContainer;    //Transform of the Container in the ui that holds all the InventoryTiles of the items.

    private void Awake()
    {
        Instance = this;
    }
    public void AddItem(KeyItem keyitem)
    {
        InventoryItems.Add(keyitem);
        keyitem.Pickup(keyitem.transform);
        DrawItems();
    }

    public void DeleteItem(KeyItem keyitem)
    {
        InventoryItems.Remove(keyitem);
        keyitem.Delete();
        DrawItems();
    }

    public void DropItem(KeyItem keyitem, Vector3 dropCoordinates)
    {
        InventoryItems.Remove(keyitem);
        keyitem.Drop(dropCoordinates);
        DrawItems();
    }

    private void DrawItems() // Adds the InventoryTiles of the items in the Inventory List to the Ui
    {
        foreach (Transform item in inventoryListContainer)    // Delets all current InventoryTiles to remove all the ones not needed anymore
        {
            Destroy(item.gameObject);
        }

        foreach (var item in InventoryItems)    //Adds all the InventoryTiles needed to the ui
        {
            GameObject newInventoryTile = Instantiate(item.InventoryTile, inventoryListContainer);
            newInventoryTile.GetComponent<Button>().onClick.AddListener(item.UseItem);
        }
    }
    
}
#endregion
