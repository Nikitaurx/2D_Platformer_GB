using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAIModel
{
    private Transform[] _waypoints;
    private int _currentPointIndex;

    public PatrolAIModel(Transform[] waypoints)
    {
        _waypoints = waypoints;
        _currentPointIndex = 0;
    }

    public Transform GetNextTarget()
    {
        if (_waypoints == null)
            return null;

        _currentPointIndex = (_currentPointIndex + 1) % _waypoints.Length;
        return _waypoints[_currentPointIndex];
    }

    public Transform GetClosestTarget(Vector2 fromPosition)
    {
        if (_waypoints == null)
            return null;

        var closestIndex = 0;
        var closestDistance = float.PositiveInfinity;

        for (var i = 1; i < _waypoints.Length; i++)
        {
            var distance = Vector2.Distance(fromPosition, _waypoints[i].position);

            if (closestDistance > distance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }

        _currentPointIndex = closestIndex;
        return _waypoints[_currentPointIndex];
    }
}
