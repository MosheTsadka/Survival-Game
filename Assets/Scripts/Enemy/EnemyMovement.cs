using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Elements")] private Player _player;

    [Header("Settings")] [SerializeField] private float moveSpeed;

    private void Update()
    { 
        if (_player != null)
        {
            FollowPlayer();
        }
    }

    public void StorePlayer(Player player)
    {
        this._player = player;
    }

    private void FollowPlayer()
    {
        Vector2 direction = (_player.transform.position - transform.position).normalized;
        Vector2 targetPosition = (Vector2)transform.position + direction * (moveSpeed * Time.deltaTime);

        transform.position = targetPosition;
    }
}