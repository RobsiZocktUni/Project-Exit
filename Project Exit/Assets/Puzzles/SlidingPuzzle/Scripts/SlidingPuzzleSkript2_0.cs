using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main Code of the SlidingPuzzleSkript2_0 was written by: Wendt Hendrik
/// The logic of the SlidingPuzzle_Game script was overworked for this script with the help of Tim Greinus
/// </summary>
public class SlidingPuzzleSkript2_0 : MonoBehaviour
{
    public TileSkript2_0[] tiles;  // Array of tiles that will be used in the sliding puzzle

    public TileSkript2_0 emptySpace;  // Reference to the empty space in the puzzle

    public float tileMoveThreshold = 0.1f;  // Treshold distance: used to check if a tile is close enough to the empty space to be swapped

    public float tileMoveDuration = 0.25f;  // Duration of tile movement animation (in seconds)

    public BoxAnimationSkript boxAnimation;  // Reference to the box animation script

    private bool isMovingTile = false;

    // Start is called before the first frame update
    void Start()
    {
        // Assign initial positions of tiles in the array
        for (int i = 0; i <= tiles.Length - 1; i++)
        {
            tiles[i].myPosInArray = i;
        }
        // Find and assign the empty space tile based on its tag in the scene
        emptySpace = GameObject.FindGameObjectWithTag("emptySpace").GetComponent<TileSkript2_0>();
    }

    // Update is called once per frame
    void Update()
    {
        // Handle player input: when the player clicks the screen
        if (Input.GetMouseButtonDown(0) && !isMovingTile)  // // Only respond if not currently moving a tile
        {
            // Cast a ray from the mouse position to check if it hits a tile
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If a tile is hit, check if it's adjacent to the empty space
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Tiles"))
            {
                // If the distance between the empty space and the clicked tile is within the allowed threshold
                if (Vector3.Distance(emptySpace.gameObject.transform.position, hit.transform.position) <= tileMoveThreshold)
                {
                    // Swap the clicked tile with the empty space
                    StartCoroutine(SwapObjectsInBlocks(hit.collider.gameObject.GetComponent<TileSkript2_0>().myPosInArray, emptySpace.myPosInArray, 3.0f));
                }
            }
        }
    }

    /// <summary>
    /// Checks if the puzzle is solved by comparing each tile's position with its correct position
    /// </summary>
    private void CheckPuzzleSolved()
    {
        // Loop through each tile to verify if its current position matches the expected position
        foreach (var tile in tiles)
        {
            // If a tile is not in the correcr position, puzzle is not solved
            if(tile.myNumber != (tile.myPosInArray + 1))
            {
                return;
            }
        }

        // If all tiles are in correct position the puzzle is solved
        Debug.Log("Gewonnen");
        // Trigger box opening animation
        StartCoroutine(DelayBeforeOpeningBox());
    }


    /// <summary>
    /// Bewegt ein Objekt in einer bestimmten Zeit zu einem Punkt
    /// </summary>
    /// <param name="moveObject">Objekt das bewegt wird</param>
    /// <param name="moveto">Ziel</param>
    /// <param name="secondsUntilArrival">Zeit bis zur Ankunft</param>
    /// <returns></returns>
    private IEnumerator MoveTo(GameObject moveObject, Vector3 moveto, float secondsUntilArrival)
    {
        secondsUntilArrival = 0.75f;
        float elapsed = 0.0f;

        // Gradually move the object using a smooth interpolation (Slerp)
        do
        {
           moveObject.transform.position = Vector3.Lerp(moveObject.transform.position, moveto, elapsed);
           elapsed += Time.deltaTime / secondsUntilArrival;  // Increment the elapsed time

            yield return null;  // Wait for the next frame
        } while (Vector3.Distance(moveObject.transform.position, moveto) >= 0.00001);  // Continue until the object reaches the target position
    }

    /// <summary>
    /// Swaps the positions of two game objects with smooth movement
    /// </summary>
    /// <param name="objectOne">First object to swap</param>
    /// <param name="objectTwo">Second object to swap</param>
    /// <param name="t">Time for the swap animation</param>
    private void SwapObjectsPos(GameObject objectOne, GameObject objectTwo, float t)
    {
        Vector3 objectOnePos = objectOne.transform.position;

        // Start coroutines to move both objects to each other's positions
        StartCoroutine(MoveTo(objectOne, objectTwo.transform.position, t));
        StartCoroutine(MoveTo(objectTwo, objectOne.transform.position, t));

        // Once the positions are swapped, set the second object's position to the first object's original position
        objectTwo.transform.position = objectOnePos;
    }

    /// <summary>
    /// Swap the positions of two tiles in the puzzle
    /// </summary>
    /// <param name="obj">Index of the first tile in the tiles array</param>
    /// <param name="obj2">Index of the second tile in the tiles array</param>
    /// <param name="t">Duration for the movement animation</param>
    private IEnumerator SwapObjectsInBlocks(int obj, int obj2, float t)
    {
        isMovingTile = true;  // Tile is being moved

        // Create temporary variables to store the tiles being swapped
        TileSkript2_0 objectOneCopy = tiles[obj];
        TileSkript2_0 objectTwoCopy = tiles[obj2];

        // Swap the objects' positions using the helper function
        SwapObjectsPos(tiles[obj].gameObject, tiles[obj2].gameObject, t);

        // Update the tiles array with the new positions
        tiles[obj] = objectTwoCopy;
        tiles[obj2] = objectOneCopy;

        // Update the position index for each tile
        tiles[obj].myPosInArray = obj;
        tiles[obj2].myPosInArray = obj2;

        // Wait until animation is complete before continuing
        yield return new WaitForSeconds(0.5f);

        isMovingTile = false;  // Tile movement is complete

        // Check if the puzzle is solved after the move
        CheckPuzzleSolved();
    }

    /// <summary>
    /// Wait for a short period before triggering the box to open when the puzzle is solved
    /// </summary>
    /// <returns>IEunumerator for coroutine</returns>
    private IEnumerator DelayBeforeOpeningBox()
    {
        yield return new WaitForSeconds(0.25f);  // Wait 0.25 seconds

        boxAnimation.BoxOpen();  // Trigger the box opening animation
    }

}
