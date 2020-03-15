using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.UI
{
    public class BuyTowerPanelController : MonoBehaviour
    {
        public static event Action<Tower> TowerBought;
        public event Action ClosePanel;

        public GameObject Panel;
        public ToggleGroup TowerToggleGroup;
        public TowerToggleController TowerTogglePrefab;
        public RectTransform FirstColumn;
        public RectTransform SecondColumn;
        public Button BuyButton;

        private Tower _currentSelectedTower;

        public void SetPanel(List<Tower> towers)
        {
            List<Tower> firstColumnTowers = towers.GetRange(0, towers.Count / 2);
            List<Tower> secondColumnTowers = towers.GetRange(towers.Count / 2, towers.Count / 2);
            for (int i = 0; i < firstColumnTowers.Count; i++)
            {
                InstantiateToggle(firstColumnTowers[i], FirstColumn);
            }
            for (int i = 0; i < secondColumnTowers.Count; i++)
            {
                InstantiateToggle(secondColumnTowers[i], SecondColumn);
            }
        }

        private void InstantiateToggle(Tower tower, RectTransform parent)
        {
            TowerToggleController currentToggle = Instantiate(TowerTogglePrefab, parent);
            currentToggle.SetTowerToggle(tower, TowerToggleGroup);
            currentToggle.ToggleValueChanged += OnTowerToggleValueChanged;
        }

        private void OnTowerToggleValueChanged(Tower tower, bool isOn)
        {
            if (isOn)
                _currentSelectedTower = tower;
            else if (_currentSelectedTower != null && _currentSelectedTower.Equals(tower))
                _currentSelectedTower = null;
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
            BuyButton.onClick.AddListener(OnBuyButtonClick);
        }

        private void OnBuyButtonClick()
        {
            if (_currentSelectedTower == null)
                return;
            TowerBought?.Invoke(_currentSelectedTower);
            ClosePanel?.Invoke();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void RemoveListeners()
        {
            BuyButton.onClick.RemoveListener(OnBuyButtonClick);
        }
        
    }
}
