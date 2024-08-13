using System;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

namespace SurvivorGame
{
    public class DamageTextManager : MonoBehaviour
    {
        [Header("Elements")]
        [SerializeField] private DamageText damageText;

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
            return Instantiate(damageText, transform);
        }

        private void ActionOnGet(DamageText obj)
        {
            damageText.gameObject.SetActive(true);
        }

        private void ActionOnRelease(DamageText obj)
        {
            damageText.gameObject.SetActive(false);
        }

        private void ActionOnDestroy(DamageText obj)
        {
            Destroy(damageText.gameObject);
        }

        private void OnDestroy()
        {
            Enemy.OnDamageTaken -= EnemyHitCallBack;
        }

        private void EnemyHitCallBack(int damage, Vector2 enemyPos)
        {
            DamageText damageTextInstance = _damageTextPool.Get();
            
            Vector3 spawnPos = enemyPos + Vector2.up * 1.5f;
            damageTextInstance.transform.position = spawnPos;
            
            damageTextInstance.PlayAnimation(damage);

            LeanTween.delayedCall(1.0f, () => _damageTextPool.Release(damageTextInstance));
        }
    }
}
