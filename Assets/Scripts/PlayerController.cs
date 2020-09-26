using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Conductor conductor;
    [SerializeField] private float leniency;
    
    private Vector2 movement;

    private void Start()
    {
        Conductor.OnBeat += _OnBeat;
    }

    public void OnMove(InputValue movementValue)
    {
        movement = movementValue.Get<Vector2>();
        float distance = conductor.DistanceToNearestBeat();
        Debug.Log(distance);
        if (Math.Abs(distance) <= leniency)
        {
            transform.position += new Vector3(movement.x, movement.y);
        }
    }

    private void Update()
    {
        // transform.position += new Vector3(movement.x, movement.y, 0);
    }

    private void _OnBeat()
    {
        
    }
}
