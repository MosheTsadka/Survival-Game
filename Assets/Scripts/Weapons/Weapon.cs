using System;
using UnityEngine;

namespace SurvivorGame
{
    public class Weapon : MonoBehaviour
    {
        [Header("Elements")] 
        [SerializeField] private Transform hitDetectionTransform;
        [SerializeField] private float hitDetectionRadius;
        
        [Header("Settings")] 
        [SerializeField] private float range;
        [SerializeField] private LayerMask enemyMask;

        [Header("Attack")] 
        [SerializeField] private int damage;

        [SerializeField] private float aimLerp;

        private void Update()
        {
            AutoAim();

            Attack();
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

        private void Attack()
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(hitDetectionTransform.position, hitDetectionRadius, enemyMask);

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<Enemy>().TakeDamage(damage);
            }
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
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(hitDetectionTransform.position, hitDetectionRadius);
        }
    }
}
