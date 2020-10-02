using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Conductor conductor;
    [SerializeField] private float speed = 0.5f;

    private void Start()
    {
        Conductor.OnBeat += _OnBeat;
    }


    private void _OnBeat()
    {
        transform.position -= new Vector3(0, speed, 0);
    }
}
