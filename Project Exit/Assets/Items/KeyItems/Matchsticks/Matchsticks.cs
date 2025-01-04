using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#region CodeFrom: Beck Jonas
public class Matchsticks : KeyItem
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
        Debug.Log("A Box of matches");
    }
}
#endregion