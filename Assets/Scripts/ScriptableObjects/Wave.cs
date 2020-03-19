using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu (fileName = "Wave", menuName = "Game/Wave")]
    public class Wave : ScriptableObject
    {
        public float Duration;
        public int EnemiesCount;
        public float SpawnInterval;
        public List<EnemyController> EnemiesPrefabs;
    }
}
