using System;
using UnityEngine;

namespace SurvivorGame
{
    public class Weapon : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private float range;
        [SerializeField] private LayerMask enemyMask;

        private void Update()
        {
            Enemy closestEnemy = null;

            //Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, enemyMask);

            if (enemies.Length <= 0)
            {
                transform.right = Vector3.up;
                return;
            }

            float minDistance = range;

            for (int i = 0; i < enemies.Length; i++)
            {
                Enemy enemyChecked = enemies[i].GetComponent<Enemy>();
                float distanceToEnemy = Vector2.Distance(transform.position, enemyChecked.transform.position);

                if (distanceToEnemy < minDistance)
                {
                    closestEnemy = enemyChecked;
                    minDistance = distanceToEnemy;
                }
            }

            if (closestEnemy == null)
            {
                transform.up = Vector3.up;
                return;
            }

            transform.right = (closestEnemy.transform.position - transform.position).normalized;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}
