using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiInfoText : MonoBehaviour
{
    public TextMeshProUGUI UiText;
    private float fadeDuration = 1f;
    private float textOnScreenTime = 2f;
    private Coroutine currentCoroutine;
    private void Start()
    {
        UiText = transform.GetComponent<TextMeshProUGUI>();
    }
    public void SetText(string Text)
    {
        if (currentCoroutine != null)   //Stop current Courutine when new Text is set
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(TextFade(Text));

    }
 
    private IEnumerator TextFade(string Text)
    {
        UiText.text = Text;
        Color originalColor = UiText.color;
        float elapsedTime = 0f;
        //FadeIn
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 0.3f, elapsedTime / fadeDuration);
            UiText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        UiText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.3f);
        //---------------------------
        //ShowTextOnScreen
        elapsedTime = 0;
        while(elapsedTime < textOnScreenTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        //---------------------------
        //FadeOut
        elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0.3f, 0, elapsedTime / fadeDuration);
            UiText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        UiText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
        //---------------------------
        currentCoroutine = null;
    }

}
