using System;
using UnityEngine;

public class ObstacleMoveTrigger : MonoBehaviour
{
    public event Action PlayerEntered;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            PlayerEntered?.Invoke();
        }
    }
}