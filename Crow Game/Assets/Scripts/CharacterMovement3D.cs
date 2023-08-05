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
        Vector3 moveDirection = (targetPosition - characterController.transform.position).normalized;
        characterController.SimpleMove(moveDirection * moveSpeed);

        // Checks to see if Crow is at its destination
        if (Vector3.Distance(characterController.transform.position, targetPosition) < arrivalDistance)
        {
            targetDest.transform.position = targetDestOriginalPosition; // returns target to resting spot
        }
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
                Debug.Log("Clicked Object is not tagged as Ground!");
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
                Debug.Log("Clicked Object is not tagged as Ground!");
            }

            targetDest.transform.position = hit.point;

        }
    }
}