using UnityEngine;
using System;

[Serializable]
public struct AIConfig
{
    public float Speed;
    public float MinDistanceToTarget;
    public Transform[] Waypoints;

}
