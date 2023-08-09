using UnityEngine;

public class CameraEdgeFollow : MonoBehaviour
{
    public Transform player;
    public float edgeThreshold = 0.1f;
    public float smoothTime = 0.2f;

    private Vector3 lastPlayerPosition;
    private Vector3 currentVelocity;

    private void Update()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(player.position);
        float playerHorizontalMovement = player.position.x - lastPlayerPosition.x;

        Vector3 targetPosition = transform.position;

        if (viewPos.x < edgeThreshold && playerHorizontalMovement < 0) // Player is moving left and near the left edge
        {
            targetPosition += Vector3.left * Mathf.Abs(playerHorizontalMovement);
        }
        else if (viewPos.x > 1.0f - edgeThreshold && playerHorizontalMovement > 0) // Player is moving right and near the right edge
        {
            targetPosition += Vector3.right * Mathf.Abs(playerHorizontalMovement);
        }

        // Smooth the camera movement
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);

        lastPlayerPosition = player.position;
    }
}
