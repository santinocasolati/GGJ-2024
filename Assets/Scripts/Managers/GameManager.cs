using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] cameras;
    public bool[] testCameras;

    public void ChangeArea(int area)
    {
        cameras[area].SetActive(true);

        for (int i = 0; i < cameras.Length; i++)
        {
            if (i == area) continue;

            cameras[i].SetActive(false);
        }
    }

    public void EndGame()
    {
        LevelManager.instance.ChangeScene(5);
    }

    private void Update()
    {
#if UNITY_EDITOR
        for (int i = 0; i < testCameras.Length; i++)
        {
            if (testCameras[i])
            {
                testCameras[i] = false;
                ChangeArea(i);
            }
        }
#endif
    }
}
