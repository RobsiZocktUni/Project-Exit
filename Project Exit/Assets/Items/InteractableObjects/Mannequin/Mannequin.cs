using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mannequin : InteractableObject
{
    public string CorrectOutfit;
    public Doll_Clothing CurrentClothing = null;
    private Character_Controller Player;


    private void Start()
    {
        Player = GameObject.Find("Player").GetComponent<Character_Controller>();
    }

    public override void Interact() 
    {
        if (CurrentClothing == null)    //Open Inventory if the Mannequin doesnt have a clothing item
        {
            Player.DisableControls();
            Player.InventoryUi.SetActive(true);
        }
        else
        {
            InventoryManager.Instance.AddItem(CurrentClothing);     //Pick up the clothing of the Mannequin if it already has one
            CurrentClothing = null;
        }
    }

    public bool AddClothingPossible(Doll_Clothing NewClothing)      //Function for Doll_Clothing.cs to put Clothing on the Mannequin if possible
    {
        if (CurrentClothing == null)
        {
            CurrentClothing = NewClothing;
            return true;
        }
        else
        {
            Debug.Log("The Mannequin already has clothing");
            return false;
        }
    }
}
