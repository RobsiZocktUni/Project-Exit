using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region CodeFromBeckJonas
public class InteractableObject : MonoBehaviour
{
    public string ObjectName;
    public virtual void Interact()
    {
        #region CodeFrom: Wendt Hendrik
        // Only allow interaction when the game is not paused
        if(PauseMenu_Script.IsPaused)
        {
            return;
        }
        #endregion

        Debug.Log("Das ist ein " + ObjectName);
    }
}
#endregion
