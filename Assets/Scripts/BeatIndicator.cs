using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatIndicator : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color onColor;
    [SerializeField] private Color offColor;
    private bool isOn = false;
    private void Start()
    {
        Conductor.OnBeat += _OnBeat;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void _OnBeat()
    {
        isOn = !isOn;
        if (isOn)
        {
            _spriteRenderer.color = onColor;
        }
        else
        {
            _spriteRenderer.color = offColor;
        }
    }
}
