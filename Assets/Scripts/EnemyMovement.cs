using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("elements")] private Player _player;

    [Header("Spawn Sequence Related")] 
    [SerializeField] private SpriteRenderer enemyRenderer;
    [SerializeField] private SpriteRenderer spawnIndicator;
    [SerializeField] private float multiplyScale;
    private bool _hasSpawn;

    [Header("Settings")] [SerializeField] private float moveSpeed;
    [SerializeField] private float playerDetectionRadius;

    [Header("Effects")] [SerializeField] private ParticleSystem particleEffect;

    [Header("Debug")] [SerializeField] private bool showGizmos;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        particleEffect = GetComponentInChildren<ParticleSystem>();

        if (_player == null)
        {
            Debug.LogWarning("No player found, Auto-dest");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Hide the Renderer component
        enemyRenderer.enabled = false;

        // Show the spawn indicator
        spawnIndicator.enabled = true;

        // Scale up & down the spawn indicator
        Vector3 targetScale = spawnIndicator.transform.localScale * multiplyScale;
        LeanTween.scale(spawnIndicator.gameObject, targetScale, 0.3f).setLoopPingPong(4)
            .setOnComplete(SpawnSequenceCompleted);

        // Prevent following & attacking during the spawn sequence
    }

    private void Update()
    {
        if (!_hasSpawn) return;

        FollowPlayer();
        TryAttack();
    }

    private void FollowPlayer()
    {
        Vector2 direction = (_player.transform.position - transform.position).normalized;
        Vector2 targetPosition = (Vector2)transform.position + direction * (moveSpeed * Time.deltaTime);

        transform.position = targetPosition;
    }

    private void SpawnSequenceCompleted()
    {
        // Show the enemy after x seconds

        // Hide the spawn indicator

        // Hide the Renderer component
        enemyRenderer.enabled = true;

        // Show the spawn indicator
        spawnIndicator.enabled = false;

        _hasSpawn = true;
    }

    private void TryAttack()
    {
        float distanceFromPlayer = Vector2.Distance(transform.position, _player.transform.position);

        if (distanceFromPlayer <= playerDetectionRadius)
        {
            DestroyObject();
        }
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