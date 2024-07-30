using UnityEngine;

namespace SurvivorGame
{
    public class Enemy : MonoBehaviour
    {
        [Header("elements")] private Player _player;
        private EnemyMovement _enemyMovement;

        [Header("Spawn Sequence Related")] [SerializeField]
        private SpriteRenderer enemyRenderer;

        [SerializeField] private SpriteRenderer spawnIndicator;
        [SerializeField] private float multiplyScale;
        private bool _hasSpawn;

        [Header("Effects")] [SerializeField] private ParticleSystem particleEffect;

        [Header("Attack")] [SerializeField] private int damage;
        [SerializeField] private float attackFrequency;
        [SerializeField] private float playerDetectionRadius;
        private float _attackDelay;
        private float _attackTimer;

        [Header("Debug")] [SerializeField] private bool showGizmos;

        private void Awake()
        {
            _player = FindObjectOfType<Player>();
            particleEffect = GetComponentInChildren<ParticleSystem>();
            _enemyMovement = GetComponent<EnemyMovement>();

            if (_player == null)
            {
                Debug.LogWarning("No player found, Auto-dest");
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            StartSpawnSequence();
            _attackDelay = 1f / attackFrequency;
        }

        private void Update()
        {
            if (_attackTimer >= _attackDelay)
            {
                TryAttack();
            }
            else
            {
                Wait();
            }
        }

        private void StartSpawnSequence()
        {
            SetRenderersVisibility(false);

            Vector3 targetScale = spawnIndicator.transform.localScale * multiplyScale;
            LeanTween.scale(spawnIndicator.gameObject, targetScale, 0.3f).setLoopPingPong(4)
                .setOnComplete(SpawnSequenceCompleted);
        }

        private void SpawnSequenceCompleted()
        {
            SetRenderersVisibility(true);
            _hasSpawn = true;
            _enemyMovement.StorePlayer(_player);
        }

        private void SetRenderersVisibility(bool visibility)
        {
            enemyRenderer.enabled = visibility;
            spawnIndicator.enabled = !visibility;
        }

        private void TryAttack()
        {
            float distanceFromPlayer = Vector2.Distance(transform.position, _player.transform.position);

            if (distanceFromPlayer <= playerDetectionRadius)
            {
                Attack();
            }
        }

        private void Wait()
        {
            _attackTimer += Time.deltaTime;
        }

        private void Attack()
        {
            _attackTimer = 0;
            _player.TakeDamage(damage);
        }

        private void DestroyObject()
        {
            particleEffect.transform.parent = null;
            particleEffect.Play();
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            if (!showGizmos) return;

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
        }
    }
}