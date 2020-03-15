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

        public void StartTimer(float seconds)
        {
            SetTimerText(seconds);
            StartCoroutine(Timer(seconds));
        }

        private void SetTimerText(float time)
        {
            string minutes = Mathf.Floor(time / 60).ToString("00");
            string seconds = (time % 60).ToString("00");
            TimerText.text = minutes + ":" + seconds;
        }

        private IEnumerator Timer(float seconds)
        {
            float secondsLeft = seconds;
            while (secondsLeft > 0)
            {
                yield return new WaitForSeconds(1f);
                secondsLeft--;
                SetTimerText(secondsLeft);
            }
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            GameFlowManager.SetPlayerParams += OnSetPlayerParams;
            GameFlowManager.SetWaves += OnSetWaves;
        }

        private void OnSetPlayerParams(Player player)
        {
            SetHealthText(player.Health, player.Health);
            SetCoinsText(player.Coins);
        }

        private void OnSetWaves(List<Wave> waves)
        {
            SetWavesText(0, waves.Count);
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void UnsubscribeEvents()
        {
            GameFlowManager.SetPlayerParams -= OnSetPlayerParams;
            GameFlowManager.SetWaves -= OnSetWaves;
        }
    }
}
