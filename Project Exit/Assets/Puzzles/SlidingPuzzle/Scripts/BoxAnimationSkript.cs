using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxAnimationSkript : MonoBehaviour
{
    private Vector3 from = new Vector3(0, 0.671f, 0);
    private Vector3 to = new Vector3(0, 0.671f, 0.35f);

    //private float moved = 0f;
    private float timeInSec = 2.0f;

    public bool isAnimating = false;

    public void BoxOpen()
    {
        //do
        //{
        //    transform.position = Vector3.Lerp(from, to, moved);
        //    moved += Time.deltaTime / timeInSec;
        //} while (Vector3.Distance(transform.position, to) >= 0.001);

        if (isAnimating) return; // Prevent animation if already running
        StartCoroutine(MoveToPos(to, timeInSec));
    }
    private IEnumerator MoveToPos(Vector3 moveto, float duration)
    {
        isAnimating = true;

        Vector3 startPosition = transform.position;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPosition, moveto, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = moveto;

        isAnimating = false;
    }
}
