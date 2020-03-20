using System;
using System.Collections.Generic;
using Controllers.UI;
using ScriptableObjects;
using UnityEngine;

namespace Controllers
{
    public class TowerController : MonoBehaviour
    {
        public Tower Config { get; set; }
        public Transform ThisTranform;
        public SpriteRenderer TowerRenderer;
        public CircleCollider2D CircleCollider;

        private List<EnemyController> _enemiesInRange = new List<EnemyController>();
        private List<ArrowController> _arrowsPool = new List<ArrowController>();
        private float _lastShotTime;
        private EnemyController _target = null;

        public void SetUpTower(Tower tower)
        {
            Config = tower;
            TowerRenderer.sprite = Config.Image;
            CircleCollider.radius = Config.Range;
            _lastShotTime = Time.time;
            EnableTower(true);
        }

        private void EnableTower(bool isEnabled)
        {
            gameObject.SetActive(isEnabled);
        }

        public void HideTower()
        {
            EnableTower(false);
            CleanArrowsPool();
        }

        private void CleanArrowsPool()
        {
            for (int i = 0; i < _arrowsPool.Count; i++)
            {
                Destroy(_arrowsPool[i].gameObject);
            }
            _arrowsPool.Clear();
        }

        private void Update()
        {
            Attack();
        }

        private void Attack()
        {
            float minimalEnemyDistance = float.MaxValue;
            //Select the enemy closest to the castle
            for (int i = 0; i < _enemiesInRange.Count; i++)
            {
                float distance = _enemiesInRange[i].DistanceToCastle();
                if (distance < minimalEnemyDistance)
                {
                    _target = _enemiesInRange[i];
                    minimalEnemyDistance = distance;
                }
            }
            
            if (_target != null)
            {
                if (Time.time - _lastShotTime > Config.ShootInterval)
                {
                    Shoot(_target);
                    _lastShotTime = Time.time;
                }
            }
        }
        
        void Shoot(EnemyController target)
        {
            Vector3 startPosition = ThisTranform.transform.position;
            ArrowController arrow = GetArrowFromPool();
            arrow.ActivateArrow(Config.Damage, target, startPosition);
        }

        private ArrowController GetArrowFromPool()
        {
            //Get arrow from pool or create new 
            for (int i = 0; i < _arrowsPool.Count; i++)
            {
                if (_arrowsPool[i].gameObject.activeInHierarchy.Equals(false))
                    return _arrowsPool[i];
            }

            ArrowController newArrow = Instantiate(Config.ArrowPrefab);
            _arrowsPool.Add(newArrow);
            return newArrow;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.tag.Equals("Enemy"))
            {
                EnemyController enemyController = collider.GetComponent<EnemyController>();
                _enemiesInRange.Add(enemyController);
                enemyController.Destroyed += OnEnemyDestroyed;
            }
        }

        private void OnEnemyDestroyed(EnemyController enemyController)
        {
            _enemiesInRange.Remove(enemyController);
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.gameObject.tag.Equals("Enemy"))
            {
                EnemyController enemyController = collider.GetComponent<EnemyController>();
                _enemiesInRange.Remove(enemyController);
                enemyController.Destroyed -= OnEnemyDestroyed;
            }
        }
    }
}
