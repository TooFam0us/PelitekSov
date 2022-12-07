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
        source = GetComponent<AudioSource>();
    }

    public void PlayClipAtPoint(int index)
    {
        source.PlayOneShot(audioClips[index]);
    }
}
