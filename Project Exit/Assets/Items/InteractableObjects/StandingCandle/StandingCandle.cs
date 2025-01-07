using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#region CodeFrom: Beck Jonas
public class StandingCandle : InteractableObject
{
    #region CodeFromLennart
    //Object that needs to be triggerd in order to play steps
    public AK.Wwise.Event triggerlightCandle;
    public bool isAktive = false;
    #endregion

    GameObject LightEffect;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        LightEffect = transform.Find("CandleFlameParticle").gameObject;
    }

    //Lights the Candle when the player interacts with it while having matchsticks in the inventory
    public override void Interact()
    {
        if (isAktive)
        {
            uiText.SetText("The candle is already lit");
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
                    isAktive=true;
                    #region CodeFrom: Beck Jonas
                    uiText.SetText("You used a match to light the candle");
                    #endregion
                }
                #endregion
                Debug.Log("You used a match to light the candle");
                itemInInventory = true;
                LightEffect.SetActive(true);
                break;
            }
        }
        if (itemInInventory == false)
        {
            if (!isAktive)
            {
                uiText.SetText("You need something to light it with");
                Debug.Log("You need something to light it with");
            }
        }
    }
}
#endregion
