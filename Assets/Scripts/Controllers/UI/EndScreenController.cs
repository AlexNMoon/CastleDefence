using System;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.UI
{
    public class EndScreenController : MonoBehaviour
    {
        public static event Action Restart;
        
        public GameObject LoseText;
        public GameObject WinText;
        public Button RestartButton;

        public void ShowWinText()
        {
            SetTextActive(LoseText, false);
            SetTextActive(WinText, true);
            EnablePanel(true);
        }

        private void EnablePanel(bool isEnabled)
        {
            gameObject.SetActive(isEnabled);
        }

        private void SetTextActive(GameObject text, bool isActive)
        {
            text.SetActive(isActive);
        }

        public void ShowLoseText()
        {
            SetTextActive(WinText, false);
            SetTextActive(LoseText, true);
            EnablePanel(true);
        }

        private void OnEnable()
        {
            AddListeners();
        }

        private void AddListeners()
        {
            RestartButton.onClick.AddListener(OnRestartButtonClick);
        }

        private void OnRestartButtonClick()
        {
            Restart?.Invoke();
            EnablePanel(false);
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void RemoveListeners()
        {
            RestartButton.onClick.RemoveListener(OnRestartButtonClick);
        }
    }
}
