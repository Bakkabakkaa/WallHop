using System;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class PointController : MonoBehaviour
{
    public event Action<int> RewardAdded;

    [SerializeField] private Point _pointPrefab;
    [SerializeField] private float _pointPositionY;
    [SerializeField] private int _rewardPerPoint = 1;

    private float _destroyPointDuration = 0.3f;
    private readonly List<Point> _points = new List<Point>();

    public void DestroAllPoints()
    {
        foreach (var point in _points)
        {
            point.PointCollected -= OnPointCollected;

            point.transform
                .DOScaleX(0f, _destroyPointDuration)
                .SetEase(Ease.Linear)
                .OnComplete(() => Destroy(point.gameObject));
        }
        
        _points.Clear();
    }

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