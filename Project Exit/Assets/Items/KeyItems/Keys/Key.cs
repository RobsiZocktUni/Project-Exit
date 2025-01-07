using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#region CodeFrom: Beck Jonas
public class Key : KeyItem
{
    #region CodeFromLennart
    public AK.Wwise.Event triggerpickup;
    public override void Pickup(Transform parent)
    {
        base.Pickup(parent);
        uiText.SetText("You picked up a " + ItemName);
        triggerpickup.Post(gameObject);
    }
    #endregion

    public override void UseItem()
    {
        
    }
}
#endregion