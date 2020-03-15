using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu (fileName = "Wave", menuName = "Game/Wave")]
    public class Wave : ScriptableObject
    {
        public float Duration;
        public int EnemiesCount;
    }
}
