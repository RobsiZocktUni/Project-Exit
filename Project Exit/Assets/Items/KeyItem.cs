using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;


// Base class for items that can be picked up by the player, added to the inventory, and used later.
public abstract class KeyItem : MonoBehaviour
{
    [Header ("Required")]
    public string ItemName;
    public GameObject InventoryTile;
    [Header ("Dont Touch")]
    public InventoryManager InventoryManager;
    private void Start()
    {
        InventoryManager = GameObject.Find("GameManager").GetComponent<InventoryManager>();
    }
    public virtual void Pickup() // Do not use this function directly. Use InventoryManager.AddItem instead.
    {
        foreach (var item in this.transform.GetComponents<UnityEngine.Component>()) // Enables all components of the object
        {
            if (item is Rigidbody)
            {
                ((Rigidbody)item).isKinematic = true;
            }
            else if(item is Behaviour)
            {
                ((Behaviour)item).enabled = false;
            }
            else if (item is Collider)
            {
                ((Collider)item).enabled = false;
            }
            else if (item is Renderer)
            {
                ((Renderer)item).enabled = false;
            }
        }
    }

    public virtual void Drop(Vector3 dropCoordinates) // Do not use this function directly. Use InventoryManager.DropItem instead.
    {
        this.transform.position = dropCoordinates;
        foreach (var item in this.transform.GetComponents<UnityEngine.Component>()) // Disables all components exept the Script "KeyItem" on the current Object
        {
            if (item == this)
            {
                continue;
            }
            else if (item is Rigidbody)
            {
                ((Rigidbody)item).isKinematic = false;
            }
            else if (item is Behaviour)
            {
                ((Behaviour)item).enabled = true;
            }
            else if (item is Collider)
            {
                ((Collider)item).enabled = true;
            }
            else if (item is Renderer)
            {
                ((Renderer)item).enabled = true;
            }
            
        }
    }

    public virtual void Delete() // Do not use this function directly. Use InventoryManager.DeleteItem instead.
    {
        Destroy(this.gameObject);
    }

    public abstract void UseItem(); // Has to be overwritten in derived classes
}
