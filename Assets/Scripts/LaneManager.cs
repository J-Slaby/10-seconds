using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LaneManager : ScriptableSingleton<LaneManager>
{
    public int numberOfLanes = 3;
    [SerializeField] private float distanceBetweenLanes;
    [SerializeField] private float distanceBetweenRows;

    public float GetXPositionFromLane(int lane)
    {
        float center = (numberOfLanes + 1) / 2f;
        float distFromCenter = lane - center;
        return distFromCenter * distanceBetweenLanes;
    }

    public float GetYPositionFromRow(int row)
    {
        float position = row * distanceBetweenRows;
        return position;
    }
}
