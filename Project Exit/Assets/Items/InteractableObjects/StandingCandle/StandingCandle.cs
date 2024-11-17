using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingCandle : InteractableObject
{
    GameObject LightEffect;
    // Start is called before the first frame update
    void Start()
    {
        LightEffect = transform.Find("CandleFlameParticle").gameObject;
    }

    //Lights the Candle when the player interacts with it while having matchsticks in the inventory
    public override void Interact()
    {
        bool itemInInventory = false;
        foreach (var item in InventoryManager.Instance.InventoryItems)
        {
            if (item.ItemName == "Matchsticks")
            {
                Debug.Log("You used a match to light the candle");
                itemInInventory = true;
                LightEffect.SetActive(true);
                break;
            }
        }
        if (itemInInventory == false)
        {
            Debug.Log("You need something to light it with");
        }
    }
}