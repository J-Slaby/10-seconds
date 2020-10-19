using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

public class Conductor : Singleton<Conductor>
{
    // Adapted from https://www.gamasutra.com/blogs/GrahamTattersall/20190515/342454/Coding_to_the_Beat__Under_the_Hood_of_a_Rhythm_Game_in_Unity.php
    // This script should be attached to the GameObject that is playing the music
    
    // Public variables that will need to be accessed by other scripts
    // The song's BPM
    public float songBpm;
    
    // The number of beats per loop
    public float beatsPerLoop;
    
    // The offset to the first beat of the song in seconds
    public float firstBeatOffset;
    
    public AudioSource musicSource;
    
    // Calculated from BPM
    private float secPerBeat;
    
    // Current song position in seconds
    private float songPosition;
    
    // Current song position in beats
    // Note: Starts at 0
    private float songPositionInBeats;

    // The total number of loops completed since the looping clip first started
    private float completedLoops = 0;

    // The current position of hte song within the loop in beats
    private float loopPositionInBeats;
    
    // Used to trigger OnBeat Event
    private float lastBeatPosition;
    
    // Number of seconds since the song started
    private float dspSongTime;

    public delegate void ComposerAction();
    public static event ComposerAction OnBeat;

    private void Start()
    {
        musicSource = GameObject.Find("Conductor").GetComponent<AudioSource>();
        secPerBeat = 60f / songBpm;
        dspSongTime = (float) AudioSettings.dspTime;
        musicSource.Play();

        lastBeatPosition = (float) (AudioSettings.dspTime - dspSongTime - firstBeatOffset) / secPerBeat;
        Conductor[] conductors = GameObject.FindObjectsOfType<Conductor>();
        if (conductors.Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void FixedUpdate()
    {
        songPosition = (float) (AudioSettings.dspTime - dspSongTime - firstBeatOffset);
        songPositionInBeats = songPosition / secPerBeat;

        if (songPositionInBeats >= (completedLoops + 1) * beatsPerLoop)
        {
            completedLoops += 1;
        }

        loopPositionInBeats = songPositionInBeats - completedLoops * beatsPerLoop;

        if (loopPositionInBeats % 1f - lastBeatPosition < 0)
        {
            if (OnBeat != null)
            {
                OnBeat();
            }
        }
        lastBeatPosition = loopPositionInBeats % 1f;
    }

    public float DistanceToNearestBeat()
    {
        float beatDistance = loopPositionInBeats % 1f;
        if (beatDistance > 0.5f)
        {
            beatDistance -= 1;
        }
        float distance = beatDistance * secPerBeat;
        return distance;
    }
}
