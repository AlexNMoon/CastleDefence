using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu (fileName = "Enemy", menuName = "Game/Enemy")]
    public class Enemy : ScriptableObject
    {
        public int Health;
        public float Speed;
        public int Damage;
        public int MinCoins;
        public int MaxCoins;
    }
}
