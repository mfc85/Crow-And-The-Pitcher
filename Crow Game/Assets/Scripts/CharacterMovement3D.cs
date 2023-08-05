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
    }

    void MoveToClickPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the raycast hit the ground or any other movable surface
            if (hit.collider.CompareTag("Ground"))
            {
                targetPosition = hit.point;
                // Ignore the Y-axis to keep the character on the same level as the ground
                targetPosition.y = characterController.transform.position.y;

                // Start the Coroutine to move towards the target position
                StartCoroutine(MoveTowardsTarget());
            }
            else
            {
                Debug.Log("Clicked Object is not tagged as Ground!");
            }
        }
    }

    System.Collections.IEnumerator MoveTowardsTarget()
    {
        while (Vector3.Distance(characterController.transform.position, targetPosition) > 0.1f)
        {
            // Move the character towards the target position
            Vector3 moveDirection = (targetPosition - characterController.transform.position).normalized;
            characterController.SimpleMove(moveDirection * moveSpeed);
            yield return null;
        }
    }
}
