using System;
using System.Collections.Generic;
using Controllers.UI;
using ScriptableObjects;
using UnityEngine;

namespace Managers
{
    public class GameFlowManager : MonoBehaviour
    {
        public static event Action<List<Tower>> SetTowers;
        public static event Action<Player> SetPlayerParams;
        public static event Action<List<Wave>> SetWaves;
        
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
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            EndScreenController.Restart += OnRestart;
        }

        private void OnRestart()
        {
            SetPlayerParams?.Invoke(Configs.PlayerConfig);
            SetWaves?.Invoke(Configs.WavesConfig);
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void UnsubscribeEvents()
        {
            EndScreenController.Restart -= OnRestart;
        }
    }
}
