using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace Controllers
{
    public class TowerController : MonoBehaviour
    {
        public Tower Config { get; set; }
        public GameObject ThisGameObject;
        public SpriteRenderer TowerRenderer;
        public CircleCollider2D CircleCollider;

        private List<EnemyController> _enemiesInRange = new List<EnemyController>();

        public void SetUpTower(Tower tower)
        {
            Config = tower;
            TowerRenderer.sprite = Config.Image;
            CircleCollider.radius = Config.Range;
            EnableTower(true);
        }

        private void EnableTower(bool isEnabled)
        {
            ThisGameObject.SetActive(isEnabled);
        }

        public void HideTower()
        {
            EnableTower(false);
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
