using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Conductor conductor;
    [SerializeField] private LaneManager laneManager;
    [SerializeField] private float leniency;
    
    private Vector2 movement;
    private int currentLane = 2;

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
            currentLane += (int)movement.x;
        }
    }

    private void Update()
    {
        float xPos = laneManager.GetPositionFromLane(currentLane);
        transform.position = new Vector3(xPos, 0f);
    }

    private void _OnBeat()
    {
        
    }
}
