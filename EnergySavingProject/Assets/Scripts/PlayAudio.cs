using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioClip[] audioClips;
    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<AudioSource>())
            source = GetComponent<AudioSource>();
        else
            source = gameObject.AddComponent<AudioSource>();
    }

    public void PlayClipAtPoint(int index)
    {
        if(!source.isPlaying)
            source.PlayOneShot(audioClips[index]);
    }
}
