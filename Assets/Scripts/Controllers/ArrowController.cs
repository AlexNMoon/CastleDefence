using System;
using UnityEngine;

namespace Controllers
{
    public class ArrowController : MonoBehaviour
    {
        public float Speed = 5;
        public Transform CurrentTransform;
        
        private int _damage;
        private EnemyController _target;
        private Vector3 _startPosition;
        private Vector3 _targetPosition;
        private float _distance;
        private float _startTime;

        public void ActivateArrow(int damage, EnemyController target, Vector3 startPosition)
        {
            _damage = damage;
            _target = target;
            _targetPosition = _target.ThisTransform.position;
            _startPosition = startPosition;
            CurrentTransform.position = _startPosition;
            gameObject.SetActive(true);
        }
        
        private void OnEnable()
        {
            SetUpArrow();
        }

        private void SetUpArrow()
        {
            _startTime = Time.time;
            _distance = Vector2.Distance (_startPosition, _targetPosition);
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if (gameObject.activeSelf)
            {
                float timeInterval = Time.time - _startTime;
                //Rotate arrow to look at the target
                Vector3 direction = gameObject.transform.position - _targetPosition;
                CurrentTransform.rotation = Quaternion.AngleAxis(
                    Mathf.Atan2 (direction.y, direction.x) * 180 / Mathf.PI,
                    new Vector3 (0, 0, 1));
                //Move arrow to the target in time that depends on speed and distance to target
                CurrentTransform.position = Vector3.Lerp(_startPosition, _targetPosition, 
                    timeInterval * Speed / _distance);
                //CHeck if arrow got to the target position
                if (CurrentTransform.position.Equals(_targetPosition))
                {
                    if (_target != null)
                    {
                        _target.ReceiveArrow(_damage);
                    }
                    gameObject.SetActive(false);
                }

            }
        }
    }
}
