using Controllers;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu (fileName = "Tower", menuName = "Game/Tower")]
    public class Tower : ScriptableObject
    {
        public int BuildPrice;
        public float Range;
        public Sprite Image;
        public float ShootInterval;
        public int Damage;
        public ArrowController ArrowPrefab;
    }
}
