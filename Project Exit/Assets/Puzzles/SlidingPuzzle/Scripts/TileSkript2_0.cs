using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSkript2_0 : MonoBehaviour
{
    public int myNumber;
    public int myPosInArray;
    
    public bool AmIRight(int check)
    {
        if (myNumber == check)
        {
            return true;
        }

        return false;

    }


}
