using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Controllers.UI
{
    public class TopPanelController : MonoBehaviour
    {
        public TMP_Text HealthText;
        public TMP_Text CoinsText;
        public TMP_Text WavesText;
        public TMP_Text TimerText;

        private Player _playerConfig;

        public void SetHealthText(int currentHealth, int maxHealth)
        {
            HealthText.text = currentHealth + "/" + maxHealth;
        }

        public void SetCoinsText(int coins)
        {
            CoinsText.text = coins.ToString();
        }

        public void SetWavesText(int currentWave, int waveCount)
        {
            WavesText.text = currentWave + "/" + waveCount;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            GameFlowManager.SetPlayerParams += OnSetPlayerParams;
            GameFlowManager.SetWaves += OnSetWaves;
            PlayerManager.HealthChanged += OnHealthChanged;
            PlayerManager.CoinsChanged += OnCoinsChanged;
            EnemySpawnController.TimeChanged += SetTimerText;
            EnemySpawnController.WaveStarted += OnWaveStarted;
        }

        private void OnSetPlayerParams(Player player)
        {
            _playerConfig = player;
            SetHealthText(_playerConfig.Health, _playerConfig.Health);
            SetCoinsText(_playerConfig.Coins);
        }

        private void OnSetWaves(List<Wave> waves)
        {
            SetWavesText(0, waves.Count);
        }

        private void OnHealthChanged(int health)
        {
            SetHealthText(health, _playerConfig.Health);
        }

        private void OnCoinsChanged(int coins)
        {
            SetCoinsText(coins);
        }

        private void SetTimerText(float time)
        {
            //Get strings for seconds and minutes from time
            string minutes = Mathf.Floor(time / 60).ToString("00");
            string seconds = (time % 60).ToString("00");
            TimerText.text = minutes + ":" + seconds;
        }

        private void OnWaveStarted(int count, int maxCount)
        {
            SetWavesText(count, maxCount);
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void UnsubscribeEvents()
        {
            GameFlowManager.SetPlayerParams -= OnSetPlayerParams;
            GameFlowManager.SetWaves -= OnSetWaves;
            EnemySpawnController.TimeChanged -= SetTimerText;
            EnemySpawnController.WaveStarted -= OnWaveStarted;
        }
    }
}
