using UnityEngine;

public class CrowMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float arrivalDistance = 0.1f; // How far Crow is to be considered "at destination"
    private CharacterController characterController;
    private Vector3 targetPosition;
    public GameObject targetDest;
    private Vector3 targetDestOriginalPosition; // Stores resting spot of the target indicator

    //variables for pebble collection
    public int pebblesCollected = 0;
    public int pebblesNeeded = 10;
    public bool isHoldingPebble = false;
    public bool canHoldMultiplePebbles = false; // Limit for the Exposition: Crow only places the pebbles in one-by-one

    //for Exposition + Talking to NPCs
    public bool canMove = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        targetDestOriginalPosition = targetDest.transform.position; // Saves the resting spot upon start
        targetPosition = characterController.transform.position; // Initialize targetPosition to the character's initial position
    }

    void Update()
    {
        if (!canMove) return;

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
            if (hit.collider.CompareTag("Ground") || hit.collider.CompareTag("Pebble") || hit.collider.CompareTag("Pitcher"))
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
            if (hit.collider.CompareTag("Ground") || hit.collider.CompareTag("Pebble") || hit.collider.CompareTag("Pitcher"))
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Pebble"))
        {
            if(!canHoldMultiplePebbles && !isHoldingPebble)
            {
                isHoldingPebble = true;
                Destroy(other.gameObject);

            }
            else if (canHoldMultiplePebbles)
            {
                pebblesCollected++;
                Destroy(other.gameObject);

                Debug.Log("Pebbles Collected: " + pebblesCollected);
            }
            
        }

        else if(other.gameObject.CompareTag("Pitcher") && isHoldingPebble)
        {
            isHoldingPebble = false;
            pebblesCollected++;

            Debug.Log("Pebbles in Pitcher: " + pebblesCollected);
        }

    }

}
