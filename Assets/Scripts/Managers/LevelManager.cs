using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public GameObject loadingScreenPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeScene(int sceneIndex)
    {
        GameObject loadingScreen = Instantiate(loadingScreenPrefab);
        loadingScreen.GetComponent<LoadingScene>().LoadScene(sceneIndex);
    }
}
