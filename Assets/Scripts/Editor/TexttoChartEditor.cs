using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ChartMaker))]
public class TexttoChartEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ChartMaker chart = (ChartMaker)target;

        if (GUILayout.Button("Make Chart!!!"))
        {
            Chart newChart = chart.MakeChart();
            AssetDatabase.CreateAsset(newChart, $"Assets/Resources/Charts/{chart.chartName}.asset");
            AssetDatabase.SaveAssets();
        }
    }
}
