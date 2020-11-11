using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource source;
    private bool started = false;

    private void Update()
    {
        if (started && !source.isPlaying)
        {
            Destroy(gameObject);
        }
    }
    
    public void PlaySound(AudioClip sourceClip)
    {
        source = GetComponent<AudioSource>();
        source.clip = sourceClip;
        source.Play();
        started = true;
    }
}
