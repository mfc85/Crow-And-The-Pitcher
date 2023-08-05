using UnityEngine;

public class CrowMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float arrivalDistance = 0.1f; // How far Crow is to be considered "at destination"
    private CharacterController characterController;
    private Vector3 targetPosition;
    public GameObject targetDest;
    private Vector3 targetDestOriginalPosition; // Stores resting spot of the target indicator

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        targetDestOriginalPosition = targetDest.transform.position; // Saves the resting spot upon start
        targetPosition = characterController.transform.position; // Initialize targetPosition to the character's initial position
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToClickPosition();
        }

        if (Input.GetMouseButton(0))
        {
            MoveTowardsMousePosition();
        }

        // Calculate the movement direction and move Crow
        Vector3 moveDirection = (targetPosition - characterController.transform.position);

        // If Crow is close enough to the target, stop moving
        if (moveDirection.magnitude < arrivalDistance)
        {
            moveDirection = Vector3.zero;
            targetDest.transform.position = targetDestOriginalPosition;
        }
        else
        {
            moveDirection = moveDirection.normalized * moveSpeed;
        }

        characterController.SimpleMove(moveDirection);
    }

    void MoveToClickPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the raycast hit any object tagged as "Ground"
            if (hit.collider.CompareTag("Ground"))
            {
                targetPosition = hit.point;
            }
            else
            {

            }

            targetDest.transform.position = hit.point;

        }

    }

    void MoveTowardsMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the raycast hit any object tagged as "Ground"
            if (hit.collider.CompareTag("Ground"))
            {
                Vector3 targetPosition = hit.point;
                targetPosition.y = characterController.transform.position.y;

                this.targetPosition = targetPosition;
            }
            else
            {

            }

            targetDest.transform.position = hit.point;

        }
    }
}
