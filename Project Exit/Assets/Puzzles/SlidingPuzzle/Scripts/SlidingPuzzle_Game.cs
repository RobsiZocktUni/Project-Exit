using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SlidingPuzzle_Game : MonoBehaviour
{
    public Transform emptySpace;// = null;  //reference to the empty space transform, used for tile movement
    private Camera _camera;

    public TileSkript[] tiles;  //array of tiles that make up the puzzle
    private int emptySpaceIndex = 15;  //the index of the empty space (initially the last tile)

    public bool puzzleSolved = false;

    public float tileMoveThreshold = 0.1f; //neighbor check threshold

    //Layer
    public GameObject boxFrame;
    //Animation
    public BoxAnimationSkript boxAnimation;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        boxFrame.layer = 2;
        // check the solvability of the initial puzzle configuration
        //CheckSolvability();
    }

    // Update is called once per frame
    void Update()
    {
        //handle player input: when the player clicks the screen
        if (Input.GetMouseButtonDown(0))
        {
            //cast a ray from the mouse position to check if it hits a tile

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            RaycastHit hit;
            //if a tile is hit, check if it's adjacent to the empty space
            if (Physics.Raycast(ray, out hit))
            {
                
                if (Vector3.Distance(emptySpace.position, hit.transform.position) <= tileMoveThreshold)
                {

                    //store the position of the empty space before the move
                    Vector3 lastEmptySpacePosition = emptySpace.position;

                    //get the clicked tile and swap positions with the empty space
                    TileSkript thisTile = hit.transform.GetComponent<TileSkript>();

                    //update the positions of the empty space and the clicked tile
                    thisTile.targetPosition = lastEmptySpacePosition;
                    emptySpace.position = thisTile.transform.position;

                    //update the array of tiles to reflect the movement
                    int tileIndex = FindTileIndex(thisTile);
                    tiles[emptySpaceIndex] = tiles[tileIndex];
                    tiles[tileIndex] = null;
                    emptySpaceIndex = tileIndex;

                    CheckPuzzleSolved();
                }
            }
        }
    }

    //find the index of a specific tile in the tiles array
    public int FindTileIndex(TileSkript ts)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] != null && tiles[i] == ts)
            {
                return i;
            }
        }
        return -1;
    }

    private void CheckPuzzleSolved()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] != null && tiles[i].number != i + 1)
            {
                puzzleSolved = false;
                return;
            }
        }


            Debug.Log("Puzzle gelöst!");
            boxFrame.layer = 0;
            boxAnimation.BoxOpen();
            puzzleSolved = true;
        //StartCoroutine(WaitForLastTileAnimation());
    }

    public void LockTilePositions()
    {
        //disable further changes to the target postitions of the tiles while animation is playing
        foreach(TileSkript tile in tiles)
        {
            if(tile != null)
            {
                if (tile.targetPosition != emptySpace.position)
                {
                    tile.targetPosition = tile.transform.position; //keep the tile in its current position
                }
            }
        }

    }

    private IEnumerator WaitForLastTileAnimation()
    {
        while (!IsTileAtTargetPosition())
        {
            yield return null;
        }
        boxAnimation.BoxOpen();
        //LockTilePositions();
    }

    private bool IsTileAtTargetPosition()
    {
        foreach (TileSkript tile in tiles)
        {
            if (tile != null && tile.transform.position != tile.targetPosition)
            {
                return false; 
            }
        }
        return true; 
    }



    //checks if the puzzle is solvable
    //not necessarily needed; only if you want to check new tile-placement variations

    //------------------------------------------------------------------------------------------
    //check if the puzzle is solvable based on the inversion count and the empty space position
    //public bool IsSolvable()
    //{
    //    int inversions = GetInversions();
    //    int emptyRowFromBottom = GetEmptyRowFromBottom();

    //    //return true if the puzzle is solvable, false otherwise
    //    return (inversions + emptyRowFromBottom) % 2 == 0;

    //    }

    //// Display the solvability check result
    //public void CheckSolvability()
    //{
    //    bool solvable = IsSolvable();
    //    if (solvable)
    //    {
    //        Debug.Log("The puzzle is solvable.");
    //    }
    //    else
    //    {
    //        Debug.Log("The puzzle is not solvable.");
    //    }
    //}

    //calculate the number of inversions in the puzzle
    //an inversion occurs when a higher-numbered tile precedes a lower-numbered one
    //int GetInversions()
    //{
    //    int inversionsSum = 0;

    //    // Flatten the tiles array and count inversions (ignoring the empty space)
    //    List<int> tileNumbers = new List<int>();
    //    foreach (TileSkript tile in tiles)
    //    {
    //        if (tile != null)
    //        {
    //            tileNumbers.Add(tile.number);
    //        }
    //    }

    //    // Count inversions in the 1D list of tile numbers
    //    for (int i = 0; i < tileNumbers.Count; i++)
    //    {
    //        for (int j = i + 1; j < tileNumbers.Count; j++)
    //        {
    //            if (tileNumbers[i] > tileNumbers[j])
    //            {
    //                inversionsSum++;
    //            }
    //        }
    //    }

    //    return inversionsSum;  //return the total number of inversions
    //}

    //int GetEmptyRowFromBottom()
    //{
    //    //int emptyIndex = Array.IndexOf(tiles, null); // Find the index of the empty tile in array
    //    int emptyIndex = emptySpaceIndex;

    //    // Calculate the row index from the top (assuming a square grid)
    //    int gridSize = (int)Mathf.Sqrt(tiles.Length); 
    //    int emptyRow = emptyIndex / gridSize; 

    //    return gridSize - 1 - emptyRow; // Return the row from the bottom, corrected for zero-index
    //}
    //---------------------------------------------------------------------------------------------------
}
