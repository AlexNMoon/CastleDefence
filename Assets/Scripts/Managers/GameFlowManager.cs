using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace Managers
{
    public class GameFlowManager : MonoBehaviour
    {
        public static event Action<List<Tower>> SetTowers;
        public static event Action<Player> SetPlayerParams;
        public static event Action<List<Wave>> SetWaves;
        public static event Action<List<Enemy>> SetEnemies;
        
        public ConfigsHolder Configs;

        private void Start()
        {
            SetUpGame();
        }

        private void SetUpGame()
        {
            SetTowers?.Invoke(Configs.TowersConfig);
            SetPlayerParams?.Invoke(Configs.PlayerConfig);
            SetWaves?.Invoke(Configs.WavesConfig);
            SetEnemies?.Invoke(Configs.EnemiesConfig);
        }
    }
}
