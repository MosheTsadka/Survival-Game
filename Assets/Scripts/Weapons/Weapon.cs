using System;
using UnityEngine;

namespace SurvivorGame
{
    public class Weapon : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private float range;
        [SerializeField] private LayerMask enemyMask;

        [SerializeField] private float aimLerp;

        private void Update()
        {
            AutoAim();
        }

        private void AutoAim()
        {
            Enemy closestEnemy = GetClosestEnemy();
            
            Vector2 targetUpVector = Vector3.up;

            if (closestEnemy != null)
            {
                targetUpVector = (closestEnemy.transform.position - transform.position).normalized;
            }
            
            transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLerp);
        }
        
        private Enemy GetClosestEnemy()
        {
            Enemy closestEnemy = null;
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, enemyMask);

            if (enemies.Length <= 0)
            {
                return null;
            }

            float minDistance = range;

            foreach (var t in enemies)
            {
                Enemy enemyChecked = t.GetComponent<Enemy>();
                float distanceToEnemy = Vector2.Distance(transform.position, enemyChecked.transform.position);

                if (distanceToEnemy < minDistance)
                {
                    closestEnemy = enemyChecked;
                    minDistance = distanceToEnemy;
                }
            }

            return closestEnemy;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}
