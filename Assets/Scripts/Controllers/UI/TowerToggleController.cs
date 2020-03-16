using System;
using Managers;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.UI
{
    public class TowerToggleController : MonoBehaviour
    {
        public event Action<Tower, bool> ToggleValueChanged; 
        
        public Toggle TowerToggle;
        public Image TowerIcon;
        public TMP_Text Price;

        private Tower _towerConfig;

        public void SetTowerToggle(Tower tower, ToggleGroup toggleGroup)
        {
            _towerConfig = tower;
            TowerIcon.sprite = _towerConfig.Image;
            Price.text = _towerConfig.BuildPrice.ToString();
            TowerToggle.group = toggleGroup;
        }
        
        private void OnEnable()
        {
            AddListeners();
            CheckAvailability();
        }

        private void AddListeners()
        {
            TowerToggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        private void OnToggleValueChanged(bool isOn)
        {
            ToggleValueChanged?.Invoke(_towerConfig, isOn);
        }

        private void CheckAvailability()
        {
            if (_towerConfig.BuildPrice > PlayerManager.Instance.Coins)
                TowerToggle.interactable = false;
            else if(!TowerToggle.interactable)
                TowerToggle.interactable = true;
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void RemoveListeners()
        {
            TowerToggle.onValueChanged.RemoveListener(OnToggleValueChanged);
        }
    }
}
