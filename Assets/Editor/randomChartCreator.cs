#region Libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Text;
using System.Threading;
//using System.Threading.Tasks;
using UnityEngine.Networking;
using System.Linq;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Collections.Specialized;
using System.Drawing;
using System.IO.Ports;
//using System.Windows;
using Microsoft.Win32;
#endregion

[ExecuteInEditMode]
[CustomEditor(typeof(ScriptableObject))]
[CanEditMultipleObjects]
public class randomChartCreator : EditorWindow
{
    int laneCount;
    int chartLength;
    List<string> chartOut;
    string chartLine;
    //false = boolean. true = integer.
    bool chartMode = false;

    #region INIT
    [MenuItem("Window/Custom/Random Chart Creator")]
    static void Init()
    {
        Debug.Log("Init");
        randomChartCreator vR = (randomChartCreator)GetWindow(typeof(randomChartCreator));
        vR.maxSize = new Vector2(Screen.width, Screen.height);
        EditorUtility.SetDirty(vR);
        vR.Show();
    }
    #endregion

    #region On Enable
    private void OnEnable()
    {
        chartOut = new List<string>();
        chartLine = "";
    }
    #endregion

    #region GUI
    private void OnGUI()
    {
        GUI.skin.button.stretchWidth = true;
        GUI.skin.box.stretchWidth = true;
        GUILayout.BeginVertical();

        GUILayout.BeginHorizontal();
        GUILayout.Box("Chart Length: ");
        chartLength = EditorGUILayout.IntField(chartLength, GUILayout.MinWidth(Screen.width * .5f));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Box("Lane Count: ");
        laneCount = EditorGUILayout.IntField(laneCount, GUILayout.MinWidth(Screen.width * .5f));
        GUILayout.EndHorizontal();

        chartMode = GUILayout.Toggle(chartMode, "Create Integer Based Chart?", GUILayout.MinWidth(Screen.width * .5f));
        
        if (GUILayout.Button("Create Random Chart"))
        {
            if (chartLength > 0 && laneCount > 0)
            {
                createChart();
            }
            else
            {
                Debug.LogError("Chart length and lane count must be greater than 0!");
            }
        }
        GUILayout.EndVertical();
    }
    #endregion

    #region Random Chart Creator
    void createChart()
    {
        chartOut = new List<string>();
        if (!chartMode)
        {
            for (int n = 0; n < chartLength; n++)
            {
                chartLine = "";
                for (int j = 0; j < laneCount; j++)
                {
                    int rnd = UnityEngine.Random.Range(0, 20);

                    if (rnd % 2 == 0)
                    {
                        chartLine += "1";
                    }
                    else
                    {
                        chartLine += "0";
                    }
                }
                chartOut.Add(chartLine);
                Repaint();
            }
        }
        else
        {
            for (int n = 0; n < chartLength; n++)
            {
                int rnd = UnityEngine.Random.Range(1, laneCount);
                chartOut.Add(rnd + "");
                Repaint();
            }
        }

        string chartDebug = "";
        for (int n = 0; n < chartOut.Count; n++)
        {
            chartDebug += chartOut[n];
            if (n < chartOut.Count - 1)
            {
                chartDebug += "\n";
            }
            Repaint();
        }
        Debug.Log(chartDebug);
    }
    #endregion
}
