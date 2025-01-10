using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#region CodeFrom: Beck Jonas
public class Candleholder : InteractableObject
{
    #region CodeFromLennart
    //Object that needs to be triggerd in order to play steps
    public AK.Wwise.Event triggerlightCandle;
    private bool isAktive = false;
    #endregion
    public GameObject LightEffectLeftCandle;
    public GameObject LightEffectMiddleCandle;
    public GameObject LightEffectRightCandle;
    //Lights the Candle when the player interacts with it while having matchsticks in the inventory
    public override void Interact()
    {
        if (isAktive)
        {
            //uiText.SetText("The candles are already lit");
        }

        bool itemInInventory = false;
        foreach (var item in InventoryManager.Instance.InventoryItems)
        {
            if (item.ItemName == "Matchsticks")
            {
                #region CodeFromLennart
                if (!isAktive)
                {
                    triggerlightCandle.Post(gameObject);
                    #region CodeFrom: Beck Jonas
                    //uiText.SetText("You used a match to light the candles");
                    #endregion
                    isAktive = true;
                }
                #endregion
                //Debug.Log("You used a match to light the candle");
                itemInInventory = true;
                LightEffectLeftCandle.SetActive(true);
                LightEffectMiddleCandle.SetActive(true);
                LightEffectRightCandle.SetActive(true);
                break;
            }
        }
        if (itemInInventory == false)
        {
            if (!isAktive)
            {
                //uiText.SetText("You need something to light it with");
                //Debug.Log("You need something to light it with");
            }
        }
    }
}
#endregion
