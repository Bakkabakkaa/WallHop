using System;
using UnityEngine;

public class Point : MonoBehaviour
{
    public event Action<Point> PointCollected;
    public event Action<Point> PointMissed;
    
    [SerializeField] private PointMissedTrigger _pointMissedTrigger;
    
    public int Reward { get; set; }

    private void Start()
    {
        _pointMissedTrigger.PointMissed += OnPointMissed;
    }

    private void OnDestroy()
    {
        _pointMissedTrigger.PointMissed -= OnPointMissed;
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            PointCollected?.Invoke(this);
        }
    }

    private void OnPointMissed()
    {
        PointMissed?.Invoke(this);
    }
}