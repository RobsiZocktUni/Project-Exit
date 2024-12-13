using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack_Script : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.0f;


    public IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float timer = 0.0f;

        Color color = fadeImage.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timer / fadeDuration);

            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
        fadeImage.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}
