using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;

#region CodeFrom: Beck Jonas
// Base class for items that can be picked up by the player, added to the inventory, and used later.
public abstract class KeyItem : MonoBehaviour
{
    [Header ("Required")]
    public string ItemName;
    public GameObject InventoryTile;
    [Header ("Dont Touch")]
    public InventoryManager InventoryManager;
    public virtual void Start()
    {
        InventoryManager = GameObject.Find("GameManager").GetComponent<InventoryManager>();
    }
    public virtual void Pickup(Transform parent) // Do not use this function directly. Use InventoryManager.AddItem instead.
    {

        DisableComponents(parent);

    }

    public virtual void DisableComponents(Transform parent) // Disables all components of the object
    {
        foreach (var item in parent.GetComponents<UnityEngine.Component>())
        {
            if (item == this)
            {
                continue;
            }
            if (item is Rigidbody)
            {
                ((Rigidbody)item).isKinematic = true;
            }
            else if (item is Behaviour)
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

        foreach (Transform child in parent)     // Disables all components of the objects children
        {
            DisableComponents(child);
        }
    }

    public virtual void Drop(Vector3 dropCoordinates) // Do not use this function directly. Use InventoryManager.DropItem instead.
    {
        this.transform.position = dropCoordinates;
        EnableComponents(this.transform);
    }

    public virtual void EnableComponents(Transform parent)
    {
        foreach (var item in parent.GetComponents<UnityEngine.Component>()) // Enables all components exept the Script "KeyItem" on the current Object
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

        foreach (Transform child in parent) //Enables them for all childreen too
        {
            EnableComponents(child);
        }
    }

    public virtual void Delete() // Do not use this function directly. Use InventoryManager.DeleteItem instead.
    {
        Destroy(this.gameObject);
    }

    public abstract void UseItem(); // Has to be overwritten in derived classes
    
}
#endregion
