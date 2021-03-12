using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LaneManager : MonoBehaviour
{
    public int numberOfLanes = 5;
    [SerializeField] private float distanceBetweenLanes;
    [SerializeField] private float distanceBetweenRows;
    [SerializeField] private float offset = -0.5f;

    public static LaneManager instance
    {
        get => FindObjectOfType<LaneManager>();
    }

    public float GetXPositionFromLane(int lane)
    {
        float center = (numberOfLanes + 1) / 2f;
        float distFromCenter = lane - center;
        return distFromCenter * distanceBetweenLanes;
    }

    public float GetYPositionFromRow(int row)
    {
        float position = row * distanceBetweenRows + offset;
        return position;
    }
}
