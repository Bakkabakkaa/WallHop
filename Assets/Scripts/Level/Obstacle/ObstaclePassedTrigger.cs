using System;
using UnityEngine;

public class ObstaclePassedTrigger : MonoBehaviour
{
    public event Action PlayerPassedObstacle;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            PlayerPassedObstacle?.Invoke();
        }
    }
}