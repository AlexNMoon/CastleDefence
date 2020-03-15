using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.UI
{
    public class TowerControlPanelController : MonoBehaviour
    {
        public static event Action TowerControlPanelClose;
        
        public BuyTowerPanelController BuyTowerController;
        public SellTowerPanelController SellTowerController;
        public Button CancelButton;
        public GameObject Panel;

        public void SetBuyTowerPanel(List<Tower> towers)
        {
            BuyTowerController.SetPanel(towers);
        }
        
        public void OpenBuyTowerPanel()
        {
            EnablePanel(true);
            BuyTowerController.EnablePanel(true);
        }

        private void EnablePanel(bool isActive)
        {
            Panel.SetActive(isActive);
        }

        public void OpenSellTowerPanel(Tower tower)
        {
            EnablePanel(true);
            SellTowerController.OpenPanel(tower);
        }
        
        private void OnEnable()
        {
            AddListeners();
            SubscribeEvents();
        }

        private void AddListeners()
        {
            CancelButton.onClick.AddListener(OnCancelButtonClick);
        }

        private void OnCancelButtonClick()
        {
            ClosePanels();
            TowerControlPanelClose?.Invoke();
        }

        private void ClosePanels()
        {
            EnablePanel(false);
            BuyTowerController.EnablePanel(false);
            SellTowerController.EnablePanel(false);
        }

        private void SubscribeEvents()
        {
            BuyTowerController.ClosePanel += ClosePanels;
            SellTowerController.ClosePanel += ClosePanels;
        }

        private void OnDisable()
        {
            RemoveListeners();
            UnsubscribeEvents();
        }

        private void RemoveListeners()
        {
            CancelButton.onClick.RemoveListener(OnCancelButtonClick);
        }

        private void UnsubscribeEvents()
        {
            BuyTowerController.ClosePanel -= ClosePanels;
            SellTowerController.ClosePanel -= ClosePanels;
        }
    }
}
