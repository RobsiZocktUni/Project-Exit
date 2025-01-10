using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#region CodeFrom: Beck Jonas
public class Doll_Clothing : KeyItem
{

    #region CodeFromLennart
    public AK.Wwise.Event triggerpickup;
    #endregion

    public BoxCollider Hitbox;

    private Camera mainCamera;
    private Character_Controller player;
    public override void Start()
    {
        base.Start();
        mainCamera = Camera.main;
        //player = GameObject.Find("Player").GetComponent<Character_Controller>();
        player = GameObject.Find("Player(withstartanimation)").GetComponent<Character_Controller>();
    }
    public override void Pickup(Transform parent)
    {
        base.Pickup(parent);
        //uiText.SetText("You picked up a " + ItemName);
        #region CodeFromLennart
        triggerpickup.Post(gameObject);
        #endregion
    }
    public override void UseItem() 
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0);

        Ray ray = mainCamera.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))   //Shots a ray to check if the object infront of the Player is a Mannequin
        {
            if (hit.collider.gameObject.GetComponent<Mannequin>())     
            {
                Mannequin mannequin = hit.collider.gameObject.GetComponent<Mannequin>();
                if (mannequin.AddClothingPossible(this))    //Calls the function in Mannequin.cs to add the clothing to it if possible
                {
                    InventoryManager.DropItem(this, mannequin.transform.position);  // "Drops" the item ontop of the Mannequin
                    this.transform.eulerAngles = new Vector3(0, -40.8f, 0);        
                    Hitbox.enabled = false;                                         // Disables the clothings hitbox so it cant block the hitbox of the Mannequin
                    player.InventoryUi.SetActive(false);                      // Closes the inventory  
                    player.EnableControls();

                    //uiText.SetText("You put the " + ItemName + " on the mannequin");
                }
            }
        }
    }
}
#endregion
