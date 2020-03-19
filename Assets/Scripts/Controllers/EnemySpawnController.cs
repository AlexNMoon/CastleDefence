using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Controllers.UI;
using Managers;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnController : MonoBehaviour
{
    public static event Action<float> TimeChanged;
    public static event Action<int, int> WaveStarted;
    public static event Action GameWin;
    
    public List<GameObject> Waypoints;

    private List<Wave> _waves;
    private int _currentWave;
    private List<EnemyController> _spawnendEnemies = new List<EnemyController>();

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        GameFlowManager.SetWaves += OnSetWaves;
        PanelsController.StartButtonClick += OnStartButtonClick;
        PlayerManager.GameEnd += OnGameEnd;
    }

    private void OnSetWaves(List<Wave> waves)
    {
        _waves = waves;
    }

    private void OnStartButtonClick()
    {
        _currentWave = 0;
        StartWave();
    }

    private void StartWave()
    {
        WaveStarted?.Invoke(_currentWave + 1, _waves.Count);
        StartCoroutine(Timer(_waves[_currentWave].Duration));
        StartCoroutine(Spawn());
    }
    
    private IEnumerator Timer(float seconds)
    {
        float secondsLeft = seconds;
        while (secondsLeft > 0)
        {
            yield return new WaitForSeconds(1f);
            secondsLeft--;
            TimeChanged?.Invoke(secondsLeft);
        }
        StartNextWave();
    }

    private void StartNextWave()
    {
        if (_currentWave < _waves.Count - 1)
        {
            _currentWave++;
            StartWave();
        }
        else
        {
            SetGameWin();
        }
    }

    private void SetGameWin()
    {
        StopAllCoroutines();
        GameWin?.Invoke();
    }

    private IEnumerator Spawn()
    {
        for (int i = 0; i < _waves[_currentWave].EnemiesCount; i++)
        {
            EnemyController newEnemy = Instantiate(_waves[_currentWave].
                EnemiesPrefabs[Random.Range(0, _waves.Count - 1)], Waypoints[0].transform.position, 
                Quaternion.Euler(new Vector3(0, 180, 0)));
            newEnemy.Waypoints = Waypoints;
            _spawnendEnemies.Add(newEnemy);
            yield return new WaitForSeconds(_waves[_currentWave].SpawnInterval);
        }
    }

    private void OnGameEnd()
    {
        StopAllCoroutines();
        for (int i = 0; i < _spawnendEnemies.Count; i++)
        {
            if (_spawnendEnemies[i] != null)
                Destroy(_spawnendEnemies[i].gameObject);
        }

        _spawnendEnemies.Clear();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void UnsubscribeEvents()
    {
        PanelsController.StartButtonClick -= OnStartButtonClick;
        PanelsController.StartButtonClick -= OnStartButtonClick;
        PlayerManager.GameEnd += OnGameEnd;
    }
}
