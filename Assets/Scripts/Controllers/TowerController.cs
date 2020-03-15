using ScriptableObjects;
using UnityEngine;

namespace Controllers
{
    public class TowerController : MonoBehaviour
    {
        public Tower Config { get; set; }
        public GameObject ThisGameObject;
        public SpriteRenderer TowerRenderer;
        public CircleCollider2D CircleCollider;

        public void SetUpTower(Tower tower)
        {
            Config = tower;
            TowerRenderer.sprite = Config.Image;
            CircleCollider.radius = Config.Range;
            EnableTower(true);
        }

        private void EnableTower(bool isEnabled)
        {
            ThisGameObject.SetActive(isEnabled);
        }

        public void HideTower()
        {
            EnableTower(false);
        }
    }
}
