using UnityEngine;

public class PointAndClickCharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private CharacterController characterController;
    private Vector3 targetPosition;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
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

        if (characterController.isGrounded)
        {
            // Keep the character on the ground
            targetPosition.y = characterController.transform.position.y;
        }

        // Calculate the movement direction and move the character
        Vector3 moveDirection = (targetPosition - characterController.transform.position).normalized;
        characterController.SimpleMove(moveDirection * moveSpeed);
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
        }
    }
}
