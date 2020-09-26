using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingIndicator : MonoBehaviour
{
    [SerializeField] private Conductor conductor;
    [SerializeField] private float leniency;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float distance = conductor.DistanceToNearestBeat();
        if (Math.Abs(distance) <= leniency)
        {
            spriteRenderer.color = Color.green;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }
}
