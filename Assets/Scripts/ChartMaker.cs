using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartMaker : MonoBehaviour
{
    public TextAsset textChart;
    public string chartName;
    public Chart MakeChart()
    {
        string chartString = textChart.text;
        List<string> beats = new List<string>();
        beats.AddRange(chartString.Split("\n"[0]));
        Chart chart = ScriptableObject.CreateInstance<Chart>();

        foreach (string beat in beats)
        {
            bool[] beatArray = new bool[5];
            int iterator = 0;
            foreach (char c in beat)
            {
                if (iterator < 5)
                {
                    if (c == '0')
                    {
                        beatArray[iterator] = false;
                    }
                    else
                    {
                        beatArray[iterator] = true;
                    }
                    iterator++;
                }

            }
        }
        return chart;
    }
}
