using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main Code of the TileSkript2_0 was written by: Wendt Hendrik
/// </summary>
public class TileSkript2_0 : MonoBehaviour
{
    public int myNumber;  // The number assigned to this tile. This is used to track its correct position in the puzzle.
    public int myPosInArray;  // The position of this tile in the array. It indicates the tile's current location in the puzzle layout

    /// <summary>
    /// Checks if the current tile's number matches the provided check value
    /// Used to verify if the tile is in its correct position in the puzzle
    /// </summary>
    /// <param name="check">The number to check against the tile's current number</param>
    /// <returns>Returns true if the tile's number matches the check value, otherwise false</returns>
    public bool RightPosition(int check)
    {
        // If the tile's number is equal to the check value, return true.
        if (myNumber == check)
        {
            return true;
        }
        // If the tile's number does not match the check value, return false
        return false;
    }
}
