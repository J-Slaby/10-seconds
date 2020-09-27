using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneManager : MonoBehaviour
{
    public int numberOfLanes = 3;
    [SerializeField] private float distanceBetweenLanes;

    public float GetPositionFromLane(int lane)
    {
        float center = (numberOfLanes + 1) / 2f;
        float distFromCenter = lane - center;
        return distFromCenter * distanceBetweenLanes;
    }
}
