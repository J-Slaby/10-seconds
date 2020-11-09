using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private bool started = false;
    private AudioSource source = null;

    public void PlaySound(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
        started = true;
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (started && !source.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
