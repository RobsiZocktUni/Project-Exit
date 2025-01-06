using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region CodeFromBeckJonas
public class InteractableObject : MonoBehaviour
{
    public string ObjectName;
    public UiInfoText uiText;

    public virtual void Start()
    {
        uiText = GameObject.Find("InfoText").GetComponent<UiInfoText>();
    }
    public virtual void Interact()
    {
        #region CodeFrom: Wendt Hendrik
        // Only allow interaction when the game is not paused
        if(PauseMenu_Script.IsPaused || EndAnimation_Script.gameEnded)
        {
            return;
        }
        #endregion

        Debug.Log("Das ist ein " + ObjectName);
    }
}
#endregion
