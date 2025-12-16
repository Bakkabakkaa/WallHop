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
    [SerializeField] private ScoreController _scoreController;
    [SerializeField] private ScoreView _scoreView;

    private void Awake()
    {
        _obstacleController.ObstacleChangedPosition += OnObstacleChangePosition;
        _pointController.RewardAdded += _scoreController.AddScore;
        _player.PlayerDied += OnPlayerDied;
        _scoreController.ScoreChanged += _scoreView.UpdateScoreLabel;
    }

    private void OnDestroy()
    {
        _obstacleController.ObstacleChangedPosition -= OnObstacleChangePosition;
        _pointController.RewardAdded -= _scoreController.AddScore;
        _player.PlayerDied -= OnPlayerDied;
        _scoreController.ScoreChanged -= _scoreView.UpdateScoreLabel;
    }

    private void OnObstacleChangePosition(Vector3 position)
    {
        var randomValue = Random.value;

        if (randomValue <= _pointSpawnProbability)
        {
            _pointController.SpawnPoint(position);
        }
    }
    private void OnPlayerDied()
    {
        _levelMover.enabled = false;
        _obstacleController.DestroyObstacles();
        _pointController.DestroAllPoints();
    }
}


