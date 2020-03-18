using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    public static event Action<int> Attacked;
    public static event Action<int> DroppedCoins;
    public event Action<EnemyController> Destroyed;
    
    public Animator EnemyAnimator;
    public Enemy Config;
    public List<GameObject> Waypoints { get; set; }

    private int _currentWaypoint;
    private float _lastWaypointSwitchTime;

    private void Start()
    {
        SetUp();
    }

    private void SetUp()
    {
        _lastWaypointSwitchTime = Time.time;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    { 
        Vector3 startPosition = Waypoints[_currentWaypoint].transform.position;
        Vector3 endPosition = Waypoints[_currentWaypoint + 1].transform.position;
        float pathLength = Vector3.Distance (startPosition, endPosition);
        float totalTimeForPath = pathLength / Config.Speed;
        float currentTimeOnPath = Time.time - _lastWaypointSwitchTime;
        gameObject.transform.position = Vector2.Lerp (startPosition, endPosition, currentTimeOnPath / totalTimeForPath);
        if (gameObject.transform.position.Equals(endPosition)) 
        {
            if (_currentWaypoint < Waypoints.Count - 2)
            {
                _currentWaypoint++;
                _lastWaypointSwitchTime = Time.time;
            }
            else
            {
                Destroy(gameObject);
                Attacked?.Invoke(Config.Damage);
            }
        }
    }

    private void DropCoins()
    {
        DroppedCoins?.Invoke(Random.Range(Config.MinCoins, Config.MaxCoins));
    }
    
    private void OnDestroy()
    {
        
    }
}
