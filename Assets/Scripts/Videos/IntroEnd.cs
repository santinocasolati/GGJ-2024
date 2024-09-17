using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroEnd : MonoBehaviour
{
    [SerializeField] private string videoFile;

    private VideoPlayer vid;
    public int nextScene;

    private void Start()
    {
        vid = GetComponent<VideoPlayer>();
        vid.loopPointReached += OnVideoEnd;
        vid.source = VideoSource.Url;
        vid.url = Application.streamingAssetsPath + "/" + videoFile;
        vid.Play();
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        LevelManager.instance.ChangeScene(nextScene);
    }
}
