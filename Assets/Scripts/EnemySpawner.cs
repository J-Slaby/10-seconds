using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy = null;
    [SerializeField] public GameObject chartReaderObj;
    [SerializeField] private ChartReader chartReader;
    [SerializeField] public string chartFileLocation;
    [SerializeField] private List<string> chart;
    
    private int beat = 0;
    
    public List<GameObject> enemies;

    private void Start()
    {
        Conductor.OnBeat += _OnBeat;
        if (chartReaderObj != null)
        {
            chartReader = chartReaderObj.GetComponent<ChartReader>();
        }
        else
        {
            Debug.LogError("Chart Reader Object Not Specified");
            Application.Quit();
        }

        if (chartFileLocation != "" && chartFileLocation.IndexOf(".txt") > 0)
        {
            if (chart == null)
            {
                Debug.LogError("Chart list null");
            }

            if (chartReader == null)
            {
                Debug.LogError("Chart Reader component is null");
            }

            chart = chartReader.ReadChart(chartFileLocation);
        }
    }

    public void Spawn(GameObject prefab, int location)
    {
        GameObject enemy = Instantiate(prefab, transform.position, Quaternion.identity);
        //enemy.GetComponent<EnemyController>().setLanePosition(location);
        enemy.GetComponent<EnemyController>().lane = location;
        enemies.Add(enemy);
    }

    private void _OnBeat()
    {
        //Debug.Log("Spawned on beat");
        if (beat < chart.Count)
        {
            string chartLine = chart[beat];
            if (chartLine != "" && chartLine != null)
            {
                //for (int n = 0; n < chartLine.Length; n++)
                //{
                //    if (chartLine[n] != '0')
                //    {
               //         Spawn(enemy, n);
               //     }
                //}

                //This is code for the chart redesign
                //chartLine = chartLine.Substring(0, chartLine.IndexOf("\n"));
                int loc = 0;
                if (!int.TryParse(chartLine, out loc))
                {
                    Debug.LogError("Failure in parsing lane designation from chartline. Chartline was: " + chartLine);
                }
                else
                {
                    Spawn(enemy, loc);
                }
            }
        }


        //if (beat % 4 == 0)
        //{
        //    Spawn(enemy);
        //}
        beat++;
    }

    private void OnDestroy()
    {
        Conductor.OnBeat -= _OnBeat;
    }
}
