using System;
using UnityEngine;
using UnityEngine.Pool;

namespace SurvivorGame
{
    public class DamageTextManager : MonoBehaviour
    {
        [Header("Elements")]
        [SerializeField] private DamageText damageTextPrefab;

        [Header("Pooling")] 
        private ObjectPool<DamageText> _damageTextPool;

        private void Awake()
        {
            Enemy.OnDamageTaken += EnemyHitCallBack;
        }

        private void Start()
        {
            _damageTextPool = new ObjectPool<DamageText>(CreateFunction, ActionOnGet, ActionOnRelease, ActionOnDestroy);
        }

        private DamageText CreateFunction()
        {
            return Instantiate(damageTextPrefab, transform);
        }

        private void ActionOnGet(DamageText obj)
        {
            damageTextPrefab.gameObject.SetActive(true);
        }

        private void ActionOnRelease(DamageText obj)
        {
            damageTextPrefab.gameObject.SetActive(false);
        }

        private void ActionOnDestroy(DamageText obj)
        {
            Destroy(damageTextPrefab.gameObject);
        }

        private void OnDestroy()
        {
            Enemy.OnDamageTaken -= EnemyHitCallBack;
        }

        [NaughtyAttributes.Button]
        private void EnemyHitCallBack(int damage, Vector2 enemyPos)
        {
            DamageText damageTextInstance = _damageTextPool.Get();
            
            Vector3 spawnPos = enemyPos + Vector2.up * 1.5f;
            damageTextInstance.transform.position = spawnPos;
            
            damageTextInstance.PlayAnimation(damage);

            LeanTween.delayedCall(1, () => _damageTextPool.Release(damageTextInstance));
        }
    }
}
