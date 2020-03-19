using System;
using System.Collections.Generic;
using Managers;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.UI
{
    public class PanelsController : MonoBehaviour
    {
        public static event Action StartButtonClick; 
        
        public TowerControlPanelController TowerControlPanel;
        public EndScreenController EndScreenPanel;
        public Button StartButton;
        
        private void OnEnable()
        {
            AddListeners();
            SubscribeEvents();
        }

        private void AddListeners()
        {
            StartButton.onClick.AddListener(OnStartButtonClick);
        }

        private void OnStartButtonClick()
        {
            StartButtonClick?.Invoke();
            EnableStartButton(false);
        }

        private void EnableStartButton(bool isEnabled)
        {
            StartButton.gameObject.SetActive(isEnabled);
        }

        private void SubscribeEvents()
        {
            GameFlowManager.SetTowers += OnSetTowers;
            TowerHolderController.SelectTowerToBuy += OnSelectTowerToBuy;
            TowerHolderController.SelectTowerToSell += OnSelectTowerToSell;
            PlayerManager.GameEnd += OnGameEnd;
            EnemySpawnController.GameWin += OnGameWin;
            EndScreenController.Restart += OnRestart;
        }

        private void OnSetTowers(List<Tower> towers)
        {
            TowerControlPanel.SetBuyTowerPanel(towers);
        }

        private void OnSelectTowerToBuy()
        {
            TowerControlPanel.OpenBuyTowerPanel();
        }

        private void OnSelectTowerToSell(Tower tower)
        {
            TowerControlPanel.OpenSellTowerPanel(tower);
        }

        private void OnGameEnd()
        {
            EndScreenPanel.ShowLoseText();
        }

        private void OnGameWin()
        {
            EndScreenPanel.ShowWinText();
        }

        private void OnRestart()
        {
            EnableStartButton(true);
        }

        private void OnDisable()
        {
            RemoveListeners();
            UnsubscribeEvents();
        }

        private void RemoveListeners()
        {
            StartButton.onClick.RemoveListener(OnStartButtonClick);
        }

        private void UnsubscribeEvents()
        {
            GameFlowManager.SetTowers -= OnSetTowers;
            TowerHolderController.SelectTowerToBuy -= OnSelectTowerToBuy;
            TowerHolderController.SelectTowerToSell -= OnSelectTowerToSell;
            PlayerManager.GameEnd -= OnGameEnd;
            EnemySpawnController.GameWin -= OnGameWin;
        }
    }
}
