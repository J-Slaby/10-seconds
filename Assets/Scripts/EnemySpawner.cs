using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy = null;
    private int beat = 0;
    
    public List<GameObject> enemies;

    private void Start()
    {
        Conductor.OnBeat += _OnBeat;
    }

    public void Spawn(GameObject prefab)
    {
        GameObject enemy = Instantiate(prefab, transform.position, Quaternion.identity);
        enemies.Add(enemy);
    }

    private void _OnBeat()
    {
        if(beat % 4 == 0)
        {
            Spawn(enemy);
        }
        beat++;
    }

    private void OnDestroy()
    {
        Conductor.OnBeat -= _OnBeat;
    }
}
