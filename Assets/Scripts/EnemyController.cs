using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public int lane;
    [SerializeField] public int row;
    [SerializeField] private int startingRow;
    private PlayerController player;

    private void Start()
    {
        Conductor.OnBeat += _OnBeat;
        lane = Random.Range(1, 4);
        row = startingRow;
        float yPos = LaneManager.instance.GetYPositionFromRow(row);
        float xPos = LaneManager.instance.GetXPositionFromLane(lane);
        transform.position = new Vector3(xPos, yPos, 0);
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    private void _OnBeat()
    {
        row -= 1;
        if (row == 0)
        {
            player.TakeDamage();
            Destroy(gameObject);
        }
        float yPos = LaneManager.instance.GetYPositionFromRow(row);
        StartCoroutine(MoveSmoothly(new Vector3(transform.position.x, yPos, 0), 3));
        // SwordAttack();
    }

    private void SwordAttack()
    {
        if (player.currentLane == lane && row == 1)
        {
            player.OnHit();
        }
    }

    private void OnDestroy()
    {
        Conductor.OnBeat -= _OnBeat;
        GameObject spawner = GameObject.Find("EnemySpawner");
        if (spawner != null)
        {
            spawner.GetComponent<EnemySpawner>().enemies.Remove(gameObject);
        }
    }

    IEnumerator MoveSmoothly(Vector3 newPosition, int numberOfFrames)
    {
        int count = 0;
        Vector3 oldPosition = transform.position;
        while (count <= numberOfFrames)
        {
            transform.position = Vector3.Lerp(oldPosition, newPosition, (float) count / numberOfFrames);
            yield return new WaitForSecondsRealtime(.05f);
            count += 1;
        }
    }
}
