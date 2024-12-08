using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollhousePuzzleManager : MonoBehaviour
{
    public List<Mannequin> mannequins = new List<Mannequin>();
    
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
                mannequin.Interactable = false;
            }
        }
    }
}
