using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "chart")]
[Serializable]
public class Chart : ScriptableObject
{
    [SerializeField] public List<bool[]> chartList = new List<bool[]>();
}