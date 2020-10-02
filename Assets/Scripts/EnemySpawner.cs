using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy = null;
    [SerializeField] private Conductor conductor;
    private int beat = 0;

    private void Start()
    {
        Conductor.OnBeat += _OnBeat;
    }

    public void Spawn(GameObject prefab) 
    {
        Instantiate(prefab, transform.position, Quaternion.identity);

    }

    private void _OnBeat()
    {
        if(beat == 0)
        {
            Spawn(enemy);
        }
        beat++;
        if(beat == 4)
        {
            beat = 0;
        }
    }
}
