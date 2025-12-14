using System;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public event Action<Floor> PlayerPassedCurrentFloor; 
    
    private BoxCollider2D _boxCollider2D;

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public Vector2 GetSize()
    {
        return _boxCollider2D.size;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            PlayerPassedCurrentFloor?.Invoke(this);
        }
    }
}