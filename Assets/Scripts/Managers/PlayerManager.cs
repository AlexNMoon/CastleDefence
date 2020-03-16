using System;
using Controllers.UI;
using ScriptableObjects;
using UnityEngine;
using Utilities;

namespace Managers
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        public static event Action<int> HealthChanged;
        public static event Action<int> CoinsChanged;
        
        public int Health => _health;
        public int Coins => _coins;

        private int _health;
        private int _coins;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            GameFlowManager.SetPlayerParams += OnSetPlayerParams;
            BuyTowerPanelController.TowerBought += OnTowerBought;
            SellTowerPanelController.TowerSold += OnTowerSold;
        }

        private void OnSetPlayerParams(Player player)
        {
            _health = player.Health;
            _coins = player.Coins;
        }

        private void OnTowerBought(Tower tower)
        {
            _coins -= tower.BuildPrice;
            CoinsChanged?.Invoke(_coins);
        }

        private void OnTowerSold(Tower tower)
        {
            _coins += tower.BuildPrice;
            CoinsChanged?.Invoke(_coins);
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void UnsubscribeEvents()
        {
            GameFlowManager.SetPlayerParams -= OnSetPlayerParams;
            BuyTowerPanelController.TowerBought -= OnTowerBought;
            SellTowerPanelController.TowerSold -= OnTowerSold;
        }
    }
}
