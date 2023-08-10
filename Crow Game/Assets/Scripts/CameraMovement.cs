using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform crowTransform;
    public Vector3 offsetToCrow;
    public Vector3 targetPosition;
    public float transitionSpeed = 1.0f;
    private bool shouldMove = false;

    private void Update()
    {
        if (shouldMove)
        {
            // Interpolate the camera's position.
            transform.position = Vector3.Lerp(transform.position, targetPosition, transitionSpeed * Time.deltaTime);

            // Ensure the camera doesn't move past the Crow.
            if (transform.position.x > crowTransform.position.x + offsetToCrow.x)
            {
                transform.position = new Vector3(crowTransform.position.x + offsetToCrow.x, transform.position.y, transform.position.z);
            }
        }
    }

    // Public function to start the camera movement.
    public void StartCameraMove()
    {
        shouldMove = true;
    }
}
