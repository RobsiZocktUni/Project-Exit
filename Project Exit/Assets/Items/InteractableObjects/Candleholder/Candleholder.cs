using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candleholder : InteractableObject
{
    public GameObject LightEffectLeftCandle;
    public GameObject LightEffectMiddleCandle;
    public GameObject LightEffectRightCandle;
    // Start is called before the first frame update
    void Start()
    {
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
                LightEffectLeftCandle.SetActive(true);
                LightEffectMiddleCandle.SetActive(true);
                LightEffectRightCandle.SetActive(true);
                break;
            }
        }
        if (itemInInventory == false)
        {
            Debug.Log("You need something to light it with");
        }
    }
}