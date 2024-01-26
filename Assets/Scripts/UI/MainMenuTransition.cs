using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuTransition : MonoBehaviour
{
    public CarpinchoLook cLook;
    public Transform bone;
    public float camMoveDuration = 0.25f;
    public Vector3 targetCameraPosition;
    public float waitTimeAfterFade = 1.0f;
    public float fadeDuration = 1.0f;
    public Image[] imgs;
    public TMP_Text[] texts;
    public Image blackScreen;

    public void StartTransition(int sceneIndex)
    {
        if (cLook != null)
        {
            cLook.enabled = false;
        }
        
        StartCoroutine(FadeCanvas(sceneIndex));
    }

    private IEnumerator FadeCanvas(int sceneIndex)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            SetAlpha(alpha);

            if (bone != null)
            {
                Quaternion boneRot = Quaternion.Lerp(bone.localRotation, Quaternion.Euler(Vector3.zero), elapsedTime / fadeDuration);
                bone.localRotation = boneRot;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SetAlpha(0f);

        yield return new WaitForSeconds(waitTimeAfterFade);

        yield return StartCoroutine(MoveCameraToTargetPosition(sceneIndex));
    }

    private void SetAlpha(float alpha)
    {
        foreach (Image img in imgs)
        {
            Color currentColor = img.color;
            currentColor.a = alpha;
            img.color = currentColor;
        }

        foreach (TMP_Text text in texts)
        {
            Color currentColor = text.color;
            currentColor.a = alpha;
            text.color = currentColor;
        }
    }

    private IEnumerator MoveCameraToTargetPosition(int sceneIndex)
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = Camera.main.transform.position;
        blackScreen.color = new Color(0, 0, 0, 0);
        blackScreen.gameObject.SetActive(true);

        while (elapsedTime < camMoveDuration)
        {
            Camera.main.transform.position = Vector3.Lerp(startingPosition, targetCameraPosition, elapsedTime / camMoveDuration);

            Color bsColor = blackScreen.color;
            bsColor.a = Mathf.Lerp(0f, 1f, elapsedTime / camMoveDuration);
            blackScreen.color = bsColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.position = targetCameraPosition;
        blackScreen.color = new Color(0, 0, 0, 1);
        LevelManager.instance.ChangeScene(sceneIndex);
    }
}
