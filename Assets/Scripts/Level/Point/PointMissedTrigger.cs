using System;
using UnityEngine;

public class PointMissedTrigger : MonoBehaviour
{
    public event Action PointMissed;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            PointMissed?.Invoke();
        }
    }
}