using System;
using UnityEngine;
using System.Collections.Generic;

public class PointController : MonoBehaviour
{
    public event Action<int> RewardAdded;

    [SerializeField] private Point _pointPrefab;
    [SerializeField] private float _pointPositionY;
    [SerializeField] private int _rewardPerPoint = 1;

    private readonly List<Point> _points = new List<Point>();

    public void SpawnPoint(Vector3 position)
    {
        var pointPosition = new Vector3(position.x, _pointPositionY);

        var point = Instantiate(_pointPrefab, pointPosition, Quaternion.identity, transform);
        point.Reward = _rewardPerPoint;

        point.PointCollected += OnPointCollected;
        point.PointMissed += OnPointMissed;
        _points.Add(point);
    }

    private void OnPointCollected(Point point)
    {
        RewardAdded?.Invoke(point.Reward);

        point.PointCollected -= OnPointCollected;
        point.PointMissed -= OnPointMissed;

        _points.Remove(point);
        Destroy(point.gameObject);
    }

    private void OnPointMissed(Point point)
    {
        point.PointCollected -= OnPointCollected;
        point.PointMissed -= OnPointMissed;
        _points.Remove(point);
        Destroy(point.gameObject);
    }
    
    
}