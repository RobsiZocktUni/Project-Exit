using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string ObjectName;
    public virtual void Interact()
    {
        Debug.Log("Das ist ein " + ObjectName);
    }
}
