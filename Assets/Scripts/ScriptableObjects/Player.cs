using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu (fileName = "Player", menuName = "Game/Player")]
    public class Player : ScriptableObject
    {
        public int Health;
        public int Coins;
    }
}
