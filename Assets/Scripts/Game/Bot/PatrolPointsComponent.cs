using System;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPointsComponent
{
    private IReadOnlyList<Transform> _points = null;
    private int _currentPointIndex = 0;

    public Transform CurrentPoint => _points?[_currentPointIndex];

    public void SetPoints(IReadOnlyList<Transform> points)
    {
        _points = points;
        _currentPointIndex = 0;
    }

    public void NextPoint()
    {
        if (_points?.Count > 0)
        {
            _currentPointIndex++;
            if (_currentPointIndex >= _points.Count)
                _currentPointIndex = 0;
        }
        else
            throw new InvalidOperationException("No patrol points!");
    }
}