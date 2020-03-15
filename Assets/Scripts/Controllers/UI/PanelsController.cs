using System.Collections.Generic;
using Managers;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.UI
{
    public class PanelsController : MonoBehaviour
    {
        public TowerControlPanelController TowerControlPanel;
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
            
        }

        private void SubscribeEvents()
        {
            GameFlowManager.SetTowers += OnSetTowers;
            TowerHolderController.SelectTowerToBuy += OnSelectTowerToBuy;
            TowerHolderController.SelectTowerToSell += OnSelectTowerToSell;
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
        }
    }
}
