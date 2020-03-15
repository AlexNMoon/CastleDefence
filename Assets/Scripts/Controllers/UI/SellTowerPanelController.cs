using System;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.UI
{
    public class SellTowerPanelController : MonoBehaviour
    {
        public static event Action<Tower> TowerSold;
        public event Action ClosePanel;

        public GameObject Panel;
        public TMP_Text Price;
        public Button SellButton;

        private Tower _currentTower;

        public void OpenPanel(Tower tower)
        {
            _currentTower = tower;
            Price.text = _currentTower.BuildPrice.ToString();
            EnablePanel(true);
        }

        public void EnablePanel(bool isEnabled)
        {
            Panel.SetActive(isEnabled);
        }
        
        private void OnEnable()
        {
            AddListeners();
        }

        private void AddListeners()
        {
            SellButton.onClick.AddListener(OnSellButtonClick);
        }

        private void OnSellButtonClick()
        {
            TowerSold?.Invoke(_currentTower);
            ClosePanel?.Invoke();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void RemoveListeners()
        {
            SellButton.onClick.RemoveListener(OnSellButtonClick);
        }
    }
}
