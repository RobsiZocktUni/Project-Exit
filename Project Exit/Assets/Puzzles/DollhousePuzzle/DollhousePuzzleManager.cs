using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#region CodeFrom: Beck Jonas
public class DollhousePuzzleManager : MonoBehaviour
{
    public List<Mannequin> mannequins = new List<Mannequin>();
    public DollhouseHD_AnimationScript DoorToUnlock;
    public void CheckIfDone()
    {
        bool done = true;
        foreach (var mannequin in mannequins)
        {
        
            if (mannequin.CurrentClothing != mannequin.CorrectOutfit )
            {
                done = false;
                break;
            }
        }

        if (done == true)
        {
            Debug.Log("You solved the Puzzle");
            foreach (var mannequin in mannequins)
            {
                DoorToUnlock.Open();
                mannequin.Interactable = false;
            }
        }
    }
}
#endregion
