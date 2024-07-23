using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private int health;
    
    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(damage, health);
        health -= realDamage;

        if (health <= 0)
        {
            PlayerDead();
        }
    }

    private void PlayerDead()
    {
        Debug.Log("The player is dead");
    }
}
