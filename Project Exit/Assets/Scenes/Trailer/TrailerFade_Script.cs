using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class TrailerFade_Script : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;  // Reference for the TextMeshPro-Objekt
    public float fadeDuration = 1.0f;   // Length of Time (in seconds)
    public float delayBetweenFades = 2.0f; // Delay between threading in and out (in seconds)
    public float initialDelay = 3.0f;   // Initial delay before start (in seconds)

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            // Start the fading cycle with an initial delay
            StartCoroutine(FadeLoop());
        }
    }

    private IEnumerator FadeLoop()
    {
        // Initial delay
        yield return new WaitForSeconds(initialDelay);

        //while (true)
        //{
            // Fade in
            yield return StartCoroutine(FadeText(0, 0.6f, fadeDuration));
            yield return new WaitForSeconds(delayBetweenFades);

            // Fade out
            yield return StartCoroutine(FadeText(0.6f, 0, 1.0f));
            yield return new WaitForSeconds(delayBetweenFades);
        //}
    }

    private IEnumerator FadeText(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        Color color = textMeshPro.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            textMeshPro.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // Ensure that the final value is achieved
        textMeshPro.color = new Color(color.r, color.g, color.b, endAlpha);
    }



}
