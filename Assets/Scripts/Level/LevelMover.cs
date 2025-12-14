using System;
using UnityEngine;

public class LevelMover : MonoBehaviour
{
    [SerializeField] private float _floorSpeed;

    private void Update()
    {
        transform.Translate(-Time.deltaTime * _floorSpeed, 0, 0);
    }
}