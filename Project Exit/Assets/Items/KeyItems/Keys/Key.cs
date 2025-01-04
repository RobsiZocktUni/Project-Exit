using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#region CodeFromBeckJonas
public class Key : KeyItem
{
    #region CodeFromLennart
    public AK.Wwise.Event triggerpickup;
    public override void Pickup(Transform parent)
    {
        base.Pickup(parent);
        triggerpickup.Post(gameObject);
    }
    #endregion

    public override void UseItem()
    {
        
    }
}
#endregion