using System;
using Controllers.UI;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Controllers
{
    public class TowerHolderController : MonoBehaviour
    {
        public static event Action SelectTowerToBuy;
        public static event Action<Tower> SelectTowerToSell;

        public TowerController CurrentTower;
        public GameObject PlaceholderImage;
        
        private bool _isPlaced;
        private bool _isInSetting;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            TowerControlPanelController.TowerControlPanelClose += OnTowerControlPanelClose;
            BuyTowerPanelController.TowerBought += OnTowerBought;
            SellTowerPanelController.TowerSold += OnTowerSold;
        }

        private void OnTowerControlPanelClose()
        {
            _isInSetting = false;
        }

        private void OnTowerBought(Tower tower)
        {
            if(!_isInSetting)
                return;
            EnablePlaceholder(false);
            CurrentTower.SetUpTower(tower);
            _isPlaced = true;
            _isInSetting = false;
        }

        private void EnablePlaceholder(bool isEnabled)
        {
            PlaceholderImage.SetActive(isEnabled);
        }

        private void OnTowerSold(Tower tower)
        {
            if(!_isInSetting)
                return;
            EnablePlaceholder(true);
            CurrentTower.HideTower();
            _isInSetting = false;
            _isPlaced = false;
        }

        private void OnMouseUpAsButton()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            _isInSetting = true;
            if(!_isPlaced)
                SelectTowerToBuy?.Invoke();
            else
                SelectTowerToSell?.Invoke(CurrentTower.Config);
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void UnsubscribeEvents()
        {
            TowerControlPanelController.TowerControlPanelClose -= OnTowerControlPanelClose;
            BuyTowerPanelController.TowerBought -= OnTowerBought;
            SellTowerPanelController.TowerSold -= OnTowerSold;
        }
    }
}
