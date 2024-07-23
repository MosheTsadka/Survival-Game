using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private int maxHealth;
    private int _health;

    [Header("Elements")] 
    [SerializeField] private Slider heathSlider;

    private void Start()
    {
        _health = maxHealth;

        heathSlider.value = 1;
    }

    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(damage, _health);
        _health -= realDamage;

        float healthBarValue = (float)_health / maxHealth;
        heathSlider.value = healthBarValue;

        if (_health <= 0)
        {
            PlayerDead();
        }
    }

    private void PlayerDead()
    {
        Debug.Log("The player is dead");
    }
}
