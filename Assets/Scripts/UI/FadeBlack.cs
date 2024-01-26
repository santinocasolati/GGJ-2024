using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeBlack : MonoBehaviour
{
    public float fadeSpeed;

    private Image image;

    private void Start()
    {
        CursorManager.instance.ShowCursor();
        image = GetComponent<Image>();
        image.color = new Color(0, 0, 0, 1);
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        Color currentColor = image.color;

        while (currentColor.a > 0)
        {
            currentColor.a -= fadeSpeed * Time.deltaTime;
            image.color = currentColor;
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
