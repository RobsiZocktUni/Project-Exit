using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mannequin : InteractableObject
{
    public Doll_Clothing CorrectOutfit;
    public Doll_Clothing CurrentClothing = null;
    public bool Interactable = true;
    private Character_Controller player;
    private DollhousePuzzleManager manager;
    


    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Character_Controller>();
        manager = GameObject.Find("GameManager").GetComponent<DollhousePuzzleManager>();
    }

    public override void Interact() 
    {
        if (Interactable)
        {
            if (CurrentClothing == null)    //Open Inventory if the Mannequin doesnt have a clothing item
            {
                player.DisableControls();
                player.InventoryUi.SetActive(true);
            }
            else
            {
                InventoryManager.Instance.AddItem(CurrentClothing);     //Pick up the clothing of the Mannequin if it already has one
                CurrentClothing = null;
            }
        }
    }

    public bool AddClothingPossible(Doll_Clothing NewClothing)      //Function for Doll_Clothing.cs to put Clothing on the Mannequin if possible
    {
        if (CurrentClothing == null)
        {
            CurrentClothing = NewClothing;
            manager.CheckIfDone();
            return true;
        }
        else
        {
            Debug.Log("The Mannequin already has clothing");
            return false;
        }
    }
}
