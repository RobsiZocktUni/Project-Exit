using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#region CodeFrom: Beck Jonas
public class Mannequin : InteractableObject
{
    public Doll_Clothing CorrectOutfit;
    public Doll_Clothing CurrentClothing = null;
    public bool Interactable = true;
    private Character_Controller player;
    private DollhousePuzzleManager manager;

    #region CodeFromLennart
    public AK.Wwise.Event triggerClothdown;
    public AK.Wwise.Event triggerClothup;
    #endregion



    public override void Start()
    {
        base.Start();
        //player = GameObject.Find("Player").GetComponent<Character_Controller>();
        player = GameObject.Find("Player(withstartanimation)").GetComponent<Character_Controller>();
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
                #region CodeFromLennart
                triggerClothup.Post(gameObject);
                #endregion
            }
        }
    }

    public bool AddClothingPossible(Doll_Clothing NewClothing)      //Function for Doll_Clothing.cs to put Clothing on the Mannequin if possible
    {
        if (CurrentClothing == null)
        {
            CurrentClothing = NewClothing;
            #region CodeFromLennart
            triggerClothdown.Post(gameObject);
            #endregion
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
#endregion