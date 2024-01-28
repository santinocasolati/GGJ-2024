using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroEnd : MonoBehaviour
{
    private VideoPlayer vid;
    public int nextScene;

    private void Start()
    {
        vid = GetComponent<VideoPlayer>();
        vid.loopPointReached += OnVideoEnd;
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        LevelManager.instance.ChangeScene(nextScene);
    }
}
