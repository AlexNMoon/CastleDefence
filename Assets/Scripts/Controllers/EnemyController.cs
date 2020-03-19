using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class EnemyController : MonoBehaviour
    {
        public static event Action<int> Attacked;
        public static event Action<int> DroppedCoins;
        public event Action<EnemyController> Destroyed;
    
        public Animator EnemyAnimator;
        public Enemy Config;
        public Transform ThisTransform;
        public List<GameObject> Waypoints { get; set; }

        private int _currentWaypoint;
        private float _lastWaypointSwitchTime;
        private int _currentHealth;

        public float DistanceToCastle()
        {
            float distance = 0;
            distance += Vector2.Distance(ThisTransform.position, 
                Waypoints [_currentWaypoint + 1].transform.position);
            for (int i = _currentWaypoint + 1; i < Waypoints.Count - 1; i++)
            {
                Vector3 startPosition = Waypoints[i].transform.position;
                Vector3 endPosition = Waypoints[i + 1].transform.position;
                distance += Vector2.Distance(startPosition, endPosition);
            }
            return distance;
        }

        public void ReceiveArrow(int damage)
        {
            _currentHealth -= damage;
            if(_currentHealth <= 0)
                StartCoroutine(Die());
        }

        private IEnumerator Die()
        {
            EnemyAnimator.SetBool("IsAlive", false);
            while (EnemyAnimator.GetCurrentAnimatorStateInfo(0).length >
                   EnemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime)
            {
                yield return new WaitForFixedUpdate();
            }
            DropCoins();
        }

        private void DropCoins()
        {
            DroppedCoins?.Invoke(Random.Range(Config.MinCoins, Config.MaxCoins));
            Destroy(gameObject);
        }

        private void Start()
        {
            SetUp();
        }

        private void SetUp()
        {
            _lastWaypointSwitchTime = Time.time;
            _currentHealth = Config.Health;
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
            ThisTransform.position = Vector2.Lerp (startPosition, endPosition, currentTimeOnPath / totalTimeForPath);
            if (ThisTransform.position.Equals(endPosition)) 
            {
                if (_currentWaypoint < Waypoints.Count - 2)
                {
                    _currentWaypoint++;
                    _lastWaypointSwitchTime = Time.time;
                }
                else
                {
                    Attacked?.Invoke(Config.Damage);
                    Destroy(gameObject);
                }
            }
        }
    
        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}
