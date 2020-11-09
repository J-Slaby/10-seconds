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

    private PlayerActions nextAction;
    
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
        if (movement.x < 0 && nextAction == PlayerActions.None)
        {
            if (isOnBeat(distance) == -1)
            {
                nextAction = PlayerActions.MoveLeft;
            } 
            else if (isOnBeat(distance) == 1)
            {
                nextAction = PlayerActions.MoveLeft;
                MoveLeft();
            }
        }
        else if (movement.x > 0 && nextAction == PlayerActions.None)
        {
            if (isOnBeat(distance) == -1)
            {
                nextAction = PlayerActions.MoveRight;
            } 
            else if (isOnBeat(distance) == 1)
            {
                nextAction = PlayerActions.MoveRight;
                MoveRight();
            }
        }
    }

    public void OnFire()
    {
        float distance = Conductor.instance.DistanceToNearestBeat();
        if (nextAction == PlayerActions.None)
        {
            if (isOnBeat(distance) == -1)
            {
                nextAction = PlayerActions.Attack;
            }
            else if (isOnBeat(distance) == 1)
            {
                nextAction = PlayerActions.Attack;
                Attack();
            }
        }
    }

    public void OnParry()
    {
        float distance = Conductor.instance.DistanceToNearestBeat();
        if (nextAction == PlayerActions.None)
        {
            if (isOnBeat(distance) == -1)
            {
                nextAction = PlayerActions.Parry;
            }
            else if (isOnBeat(distance) == 1)
            {
                nextAction = PlayerActions.Parry;
                Parry();
            }
        }
    }
    
    private int isOnBeat(float distance)
    {
        if (-leniency < distance && distance < 0f)
        {
            return -1;
        } 
        if (0 <= distance && distance < leniency)
        {
            return 1;
        }
        return 0;
    }

    public void TakeDamage()
    {
        playerHealth -= 1;
    }

    public void OnHit()
    {
        if (nextAction != PlayerActions.Parry && nextAction != PlayerActions.Attack)
        {
            TakeDamage();
        }
    }

    private void Update()
    {
        float xPos = LaneManager.instance.GetXPositionFromLane(currentLane);
        transform.position = new Vector3(xPos, 0f);
        if (playerHealth == 0)
        {
            RestartScene.StartScene();
        }
    }

    private void _OnBeat()
    {
        switch (nextAction)
        {
            case PlayerActions.Attack:
                Attack();
                break;
            case PlayerActions.Parry:
                Parry();
                break;
            case PlayerActions.MoveLeft:
                MoveLeft();
                break;
            case PlayerActions.MoveRight:
                MoveRight();
                break;
        }

        StartCoroutine(ClearBuffer(leniency));
    }

    IEnumerator ClearBuffer(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        nextAction = PlayerActions.None;
    }

    private void MoveLeft()
    {
        currentLane -= 1;
        if (currentLane < 1)
        {
            currentLane = laneManager.numberOfLanes;
        }
    }

    private void MoveRight()
    {
        currentLane += 1;
        if (currentLane > laneManager.numberOfLanes)
        {
            currentLane = 1;
        }
    }

    private void Attack()
    {
        foreach (GameObject enemy in enemySpawner.enemies)
        {
            EnemyController controller = enemy.GetComponent<EnemyController>();
            if (controller.lane == currentLane && (controller.row == 1 || controller.row == 0))
            {
                Destroy(enemy);
            }
        }
    }

    private void Parry()
    {
        
    }

    private void OnDestroy()
    {
        Conductor.OnBeat -= _OnBeat;
    }
}

public enum PlayerActions
{
    None,
    MoveLeft,
    MoveRight,
    Attack,
    Parry
}
