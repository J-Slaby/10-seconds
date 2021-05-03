using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using Random = System.Random;

public class ChartReader : MonoBehaviour
{
    public List<string> ReadChart(string chartfile)
    {
        if (File.Exists(chartfile))
        {
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                List<string> chart = new List<string>();
                StreamReader sr = new StreamReader(chartfile);
                //Read the first line of text
                string line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the line to console window
                    Console.WriteLine(line);
                    //Read the next line
                    line = sr.ReadLine();
                    chart.Add(line);
                }
                //close the file
                sr.Close();
                Console.ReadLine();
                return chart;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return null;
            }
        }
        else
        {
            Debug.LogError("Specified File Does Not Exist!!!!");
            Application.Quit();
            return null;
        }
    }
}