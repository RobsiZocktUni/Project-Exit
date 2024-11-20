using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSkript : MonoBehaviour
{
    public Vector3 targetPosition;  //position the tile is moving towards (this is updated during the game)
    public Vector3 correctPosition;  //position where the tile should be in the solved puzzle
    public int number;  //number that identifies the tile (used for checking puzzle state)

    public bool inRightPlace;  //Boolean to check if the tile is in the correct position

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        //set the initial target and correct positions to the current position of the tile
        targetPosition = transform.position;
        correctPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //move the tile gradually towards the target position using Lerp for smooth movement
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.05f);

        //check if the tile has reached its correct position in the solved puzzle
        if (Vector3.Distance(targetPosition, correctPosition) < 0.01)//targetPosition == correctPosition)
        {
            inRightPlace = true;  // Tile is in the correct place
        }
        else
        {
            inRightPlace = false;  //Tile is not in the correct place
        }


    }
}
