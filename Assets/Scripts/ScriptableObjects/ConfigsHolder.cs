using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu (fileName = "Configs", menuName = "Game/Configs")]
    public class ConfigsHolder : ScriptableObject
    {
        public Player PlayerConfig;
        public List<Enemy> EnemiesConfig;
        public List<Tower> TowersConfig;
        public List<Wave> WavesConfig;
    }
}
