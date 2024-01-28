using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarpinchoDie : MonoBehaviour
{
    public List<AudioClip> soundList;

    public void PlaySound()
    {
        AudioClip clip = PickRandomItem();
        AudioManager.instance.PlaySound(clip);
    }

    AudioClip PickRandomItem()
    {
        if (soundList.Count > 0)
        {
            int randomIndex = Random.Range(0, soundList.Count);
            return soundList[randomIndex];
        }
        else
        {
            Debug.LogWarning("Item list is empty!");
            return null;
        }
    }
}
