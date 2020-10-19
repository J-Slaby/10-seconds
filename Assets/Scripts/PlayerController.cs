using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private LaneManager laneManager;
    [SerializeField] private float leniency;
    [SerializeField] private EnemySpawner enemySpawner;

    private PlayerActions nextAction; // do things with this
    
    private Vector2 movement;
    public int currentLane = 2;
    public int playerHealth = 3;

    private void Start()
    {
        Conductor.OnBeat += _OnBeat;
        laneManager = LaneManager.instance;
    }

    public void OnMove(InputValue movementValue)
    {
        movement = movementValue.Get<Vector2>();
        float distance = Conductor.instance.DistanceToNearestBeat();
        Debug.Log(distance);
        if (Math.Abs(distance) <= leniency)
        {
            currentLane += (int)movement.x;
            if (currentLane > laneManager.numberOfLanes)
            {
                currentLane = 1;
            }
            if (currentLane < 1)
            {
                currentLane = laneManager.numberOfLanes;
            }
            float xPos = laneManager.GetXPositionFromLane(currentLane);
            StartCoroutine(MoveSmoothly(new Vector3(xPos, 0f), 10));
        }
    }

    public void OnFire()
    {
        foreach (GameObject enemy in enemySpawner.enemies)
        {
            EnemyController controller = enemy.GetComponent<EnemyController>();
            if (controller.lane == currentLane && controller.row == 1)
            {
                Destroy(enemy);
            }
        }
    }

    public void OnParry()
    {
        
    }

    public void OnHit()
    {
        if (nextAction != PlayerActions.Parry)
        {
            playerHealth -= 1;
        }

    }

    private void Update()
    {
        if (playerHealth == 0)
        {
            RestartScene.StartScene();
        }
    }

    private void _OnBeat()
    {
       
    }
    
    IEnumerator MoveSmoothly(Vector3 newPosition, int numberOfFrames)
    {
        int count = 0;
        Vector3 oldPosition = transform.position;
        while (count <= numberOfFrames)
        {
            transform.position = Vector3.Lerp(oldPosition, newPosition, (float) count / numberOfFrames);
            yield return null;
            count += 1;
        }
    }
}

public enum PlayerActions
{
    MoveLeft,
    MoveRight,
    Attack,
    Parry
}
