using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingPuzzleSkript2_0 : MonoBehaviour
{


    public TileSkript2_0[] tiles;
    public TileSkript2_0 emptySpace;

    public float tileMoveThreshold = 0.1f;

    public BoxAnimationSkript boxAnimation;

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i <= tiles.Length - 1; i++)
        {
            tiles[i].myPosInArray = i;
        }
        emptySpace = GameObject.FindGameObjectWithTag("emptySpace").GetComponent<TileSkript2_0>();
    }

    // Update is called once per frame
    void Update()
    {
        //handle player input: when the player clicks the screen
        if (Input.GetMouseButtonDown(0))
        {
            //cast a ray from the mouse position to check if it hits a tile
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //if a tile is hit, check if it's adjacent to the empty space
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Tiles"))
            {

                if (Vector3.Distance(emptySpace.gameObject.transform.position, hit.transform.position) <= tileMoveThreshold)
                {
                    SwapObjectsInBlocks(hit.collider.gameObject.GetComponent<TileSkript2_0>().myPosInArray, emptySpace.myPosInArray, 3.0f);
                    

                    CheckPuzzleSolved();
                }
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    private void CheckPuzzleSolved()
    {

        foreach (var tile in tiles)
        {
            if(tile.myNumber != (tile.myPosInArray + 1))
            {
                return;
            }
        }
        Debug.Log("Gewonnen");
        boxAnimation.BoxOpen();
    }


    /// <summary>
    /// Bewegt ein Objekt in einer bestimmten Zeit zu einem Punkt
    /// </summary>
    /// <param name="moveObject">Objekt das bewegt wird</param>
    /// <param name="moveto">Ziel</param>
    /// <param name="SekundenBisAnkunft">Zeit bis zur Ankunft</param>
    /// <returns></returns>
    private IEnumerator MoveTo(GameObject moveObject, Vector3 moveto, float SekundenBisAnkunft)
    {

        float gelaufen = 0.0f;
        do
        {
           moveObject.transform.position = Vector3.Slerp(moveObject.transform.position, moveto, gelaufen);
            gelaufen += Time.deltaTime / SekundenBisAnkunft;

            yield return null;
        } while (Vector3.Distance(moveObject.transform.position, moveto) >= 0.001);


    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="obj2"></param>
    /// <param name="t"></param>
    private void SwapObjectsPos(GameObject obj, GameObject obj2, float t)
    {
        Vector3 objectOnePos = obj.transform.position;
        StartCoroutine(MoveTo(obj, obj2.transform.position, t));
        obj2.transform.position = objectOnePos;
    }
    private void SwapObjectsInBlocks(int obj, int obj2, float t)
    {
        TileSkript2_0 objectOneCopy = tiles[obj];
        TileSkript2_0 objectTwoCopy = tiles[obj2];
        SwapObjectsPos(tiles[obj].gameObject, tiles[obj2].gameObject, t);
        tiles[obj] = objectTwoCopy;
        tiles[obj2] = objectOneCopy;

        tiles[obj].myPosInArray = obj;
        tiles[obj2].myPosInArray = obj2;

    }
}
