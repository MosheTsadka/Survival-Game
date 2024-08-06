using System.Collections.Generic;
using UnityEngine;

namespace SurvivorGame
{
    public class Weapon : MonoBehaviour
    {
        enum State
        {
            Idle, 
            Attack
        }

        private State _state;
        
        [Header("Elements")] 
        [SerializeField] private Transform hitDetectionTransform;
        [SerializeField] private float hitDetectionRadius;
        
        [Header("Settings")] 
        [SerializeField] private float range;
        [SerializeField] private LayerMask enemyMask;
        [SerializeField] private Animator animator;
        private List<Enemy> _damagedEnemies = new List<Enemy>();

        [Header("Attack")] 
        [SerializeField] private int damage;
        [SerializeField] private float attackDelay;
        private float _attackTimer;

        [SerializeField] private float aimLerp;

        private void Start()
        {
            _state = State.Idle;
        }

        private void Update()
        {
            switch (_state)
            {
                case State.Idle:
                {
                    AutoAim();
                    break;
                }

                case State.Attack:
                {
                    Attack();
                    break;
                }
            }
        }

        private void AutoAim()
        {
            Enemy closestEnemy = GetClosestEnemy();
            
            Vector2 targetUpVector = Vector3.up;

            if (closestEnemy != null)
            {
                ManageAttack();
                targetUpVector = (closestEnemy.transform.position - transform.position).normalized;
            }
            
            transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLerp);
            
            IncrimentAttackTimer();
        }

        private void ManageAttack()
        {
            if (_attackTimer >= attackDelay)
            {
                _attackTimer = 0;
                StartAttack();
            }
        }

        private void IncrimentAttackTimer()
        {
            _attackTimer += Time.deltaTime;
        }

        [NaughtyAttributes.Button]
        private void StartAttack()
        {
            animator.Play("Attack");
            _state = State.Attack;

            animator.speed = 1f / attackDelay;
        }

        private void Attacking()
        {
            Attack();
        }

        private void StopAttack()
        {
            _state = State.Idle;
            
            _damagedEnemies.Clear();
        }

        private void Attack()
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(hitDetectionTransform.position, hitDetectionRadius, enemyMask);

            for (int i = 0; i < enemies.Length; i++)
            {
                Enemy enemy = enemies[i].GetComponent<Enemy>();
                
                if (!_damagedEnemies.Contains(enemy))
                {
                    enemy.TakeDamage(damage);
                    _damagedEnemies.Add(enemy);
                }
                
                //enemies[i].GetComponent<Enemy>().TakeDamage(damage);
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
