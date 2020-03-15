using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu (fileName = "Enemy", menuName = "Game/Enemy")]
    public class Enemy : ScriptableObject
    {
        public float Health;
        public float Speed;
        public float Damage;
        public EnemyController Prefab;
        public int MinCoins;
        public int MaxCoins;
    }
}
