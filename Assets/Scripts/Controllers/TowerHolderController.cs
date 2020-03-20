using System;
using System.Collections.Generic;
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
            EndScreenController.Restart += OnRestart;
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
            if (IsPointerOverUIObject())
                return;
            _isInSetting = true;
            if(!_isPlaced)
                SelectTowerToBuy?.Invoke();
            else
                SelectTowerToSell?.Invoke(CurrentTower.Config);
        }
        private bool IsPointerOverUIObject() 
        {
            //Check if pointer (mouse or touch) is over UI element
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }

        private void OnRestart()
        {
            EnablePlaceholder(true);
            CurrentTower.HideTower();
            _isInSetting = false;
            _isPlaced = false;
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
            EndScreenController.Restart += OnRestart;
        }
    }
}
