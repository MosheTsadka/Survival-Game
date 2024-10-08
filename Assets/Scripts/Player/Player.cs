using UnityEngine;

namespace SurvivorGame
{
    public class Player : MonoBehaviour
    {
        private PlayerHealth _playerHealth;

        private void Awake()
        {
            _playerHealth = GetComponent<PlayerHealth>();
        }

        public void TakeDamage(int damage)
        {
            _playerHealth.TakeDamage(damage);
        }
    }
}
