using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform playerTarget;

    [Header("Settings")]
    [SerializeField] private Vector2 minMaxXY;

    private void LateUpdate()
    {
        if (playerTarget == null)
        {
            Debug.LogWarning("No target has been specified");
            return;
        }
        
        Vector3 targetPosition = playerTarget.position;
        targetPosition.z = -10;

        targetPosition.x = Mathf.Clamp(targetPosition.x, -minMaxXY.x, minMaxXY.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -minMaxXY.y, minMaxXY.y);

        transform.position = targetPosition;
    }
}
