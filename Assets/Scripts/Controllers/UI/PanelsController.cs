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
        }

        private void AddListeners()
        {
            StartButton.onClick.AddListener(OnStartButtonClick);
        }

        private void OnStartButtonClick()
        {
            
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void RemoveListeners()
        {
            StartButton.onClick.RemoveListener(OnStartButtonClick);
        }
    }
}
