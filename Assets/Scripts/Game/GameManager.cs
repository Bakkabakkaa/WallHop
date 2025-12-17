using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] private BackgroundColorController _backgroundColorController;
    [SerializeField] private int _difficultyIncreasePeriodInPoints = 10;
    [SerializeField] private float _sceneChangeDelay = 1f;
    [Tooltip("Points required to change background color")] [SerializeField]
    private int _colorChangePeriodInPoints = 5;

    private void Awake()
    {
        _obstacleController.ObstacleChangedPosition += OnObstacleChangePosition;
        _pointController.RewardAdded += _scoreController.AddScore;
        _player.PlayerDied += OnPlayerDied;
        _scoreController.ScoreChanged += _scoreView.UpdateScoreLabel;
        _scoreController.ScoreChanged += OnScoreChanged;
    }

    private void OnDestroy()
    {
        _obstacleController.ObstacleChangedPosition -= OnObstacleChangePosition;
        _pointController.RewardAdded -= _scoreController.AddScore;
        _player.PlayerDied -= OnPlayerDied;
        _scoreController.ScoreChanged -= _scoreView.UpdateScoreLabel;
        _scoreController.ScoreChanged -= OnScoreChanged;
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

        StartCoroutine(LoadGameOverSceneWithDelay());
    }

    private void OnScoreChanged(int score)
    {
        _scoreView.UpdateScoreLabel(score);

        if (score % _colorChangePeriodInPoints == 0)
        {
            _backgroundColorController.ChangeColor();
        }

        if (score % _difficultyIncreasePeriodInPoints == 0)
        {
            _levelMover.IncreaseSpeed();
        }
    }

    private IEnumerator LoadGameOverSceneWithDelay()
    {
        yield return new WaitForSeconds(_sceneChangeDelay);
        SceneManager.LoadSceneAsync(GlobalConstants.GAME_OVER_SCENE);
    }
}


