using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace SurvivorGame
{
    public class PlayerHealth : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private int maxHealth;
        private int _health;

        [Header("Elements")] [SerializeField] private Slider heathSlider;

        [SerializeField] private TMP_Text healthText;

        private void Start()
        {
            _health = maxHealth;
            UpdateUI();

            heathSlider.value = 1;
        }

        public void TakeDamage(int damage)
        {
            int realDamage = Mathf.Min(damage, _health);
            _health -= realDamage;

            UpdateUI();

            if (_health <= 0)
            {
                PlayerDead();
            }
        }

        private void UpdateUI()
        {
            float healthBarValue = (float)_health / maxHealth;
            heathSlider.value = healthBarValue;

            healthText.text = _health + " / " + maxHealth;
        }

        private void PlayerDead()
        {
            Debug.Log("The player is dead");
            SceneManager.LoadScene(0);
        }
    }
}