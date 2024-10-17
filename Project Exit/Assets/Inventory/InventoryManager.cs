using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    private List<KeyItem> inventoryItems = new List<KeyItem>();
    public Transform inventoryListContainer;

    void Start()
    {
        Instance = this;
    }
    public void AddItem(KeyItem keyitem)
    {
        inventoryItems.Add(keyitem);
        keyitem.Pickup();
        DrawItems();
    }

    public void DeleteItem(KeyItem keyitem)
    {
        inventoryItems.Remove(keyitem);
        keyitem.Delete();
        DrawItems();
    }

    public void DropItem(KeyItem keyitem, Vector3 dropCoordinates)
    {
        inventoryItems.Remove(keyitem);
        keyitem.Drop(dropCoordinates);
        DrawItems();
    }

    private void DrawItems() // Adds the InventoryTiles of the items in the Inventory List to the Ui
    {
        foreach (Transform item in inventoryListContainer)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in inventoryItems)
        {
            GameObject newInventoryTile = Instantiate(item.InventoryTile, inventoryListContainer);
            newInventoryTile.GetComponent<Button>().onClick.AddListener(item.UseItem);
        }
    }
    
}
