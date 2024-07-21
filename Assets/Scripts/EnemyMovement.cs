using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("elements")]
    private Player _player;

    [Header("Settings")] [SerializeField] private float moveSpeed;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();

        if (_player == null)
        {
            Debug.LogWarning("No player found, Auto-dest");
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Vector2 diraction = (_player.transform.position - transform.position).normalized;
        Vector2 targetPosition = (Vector2)transform.position + diraction * (moveSpeed * Time.deltaTime);

        transform.position = targetPosition;
    }
}
