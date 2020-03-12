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
        public float Damage;
    }
}
