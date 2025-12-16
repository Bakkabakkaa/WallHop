using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 1)] private float _pointSpawnProbability = 0.7f;
    [SerializeField] private PointController _pointController;
    [SerializeField] private ObstacleController _obstacleController;
    [SerializeField] private PlayerController _player;
    [SerializeField] private LevelMover _levelMover;

    private void Awake()
    {
        _obstacleController.ObstacleChangedPosition += OnObstacleChangePosition;
        _pointController.RewardAdded += OnRewardAdded;
        _player.PlayerDied += OnPlayerDied;
    }

    private void OnDestroy()
    {
        _obstacleController.ObstacleChangedPosition -= OnObstacleChangePosition;
        _pointController.RewardAdded -= OnRewardAdded;
        _player.PlayerDied -= OnPlayerDied;
    }

    private void OnObstacleChangePosition(Vector3 position)
    {
        var randomValue = Random.value;

        if (randomValue <= _pointSpawnProbability)
        {
            _pointController.SpawnPoint(position);
        }
    }

    private void OnRewardAdded(int rewardPerPoint)
    {
        Debug.Log(rewardPerPoint);
    }

    private void OnPlayerDied()
    {
        _levelMover.enabled = false;
        _obstacleController.DestroyObstacles();
        _pointController.DestroAllPoints();
    }
}
