using UnityEngine;

public class CrowMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float arrivalDistance = 0.1f;
    internal CharacterController characterController;
    internal Vector3 targetPosition;
    public GameObject targetDest;
    internal Vector3 targetDestOriginalPosition;

    // Variables for item collection
    public int pebblesCollected = 0;
    public int pebblesNeeded = 10;
    public bool isHoldingPebble = false;
    public bool canHoldMultiplePebbles = false;
    public bool isHoldingJewel = false;
    public bool isHoldingTrash = false;

    public bool canMove = false;
    public Animator animator;
    public Pitcher pitcherComponent;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        targetDestOriginalPosition = targetDest.transform.position;
        targetPosition = characterController.transform.position;
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

        Vector3 moveDirection = (targetPosition - characterController.transform.position);

        // Flips Crow based on direction
        if(moveDirection.x < 0) 
            transform.rotation = Quaternion.Euler(0, -180, 0);
        else if(moveDirection.x > 0) 
            transform.rotation = Quaternion.Euler(0, 0, 0);

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

        animator.SetBool("isWalking", moveDirection.magnitude > 0);
        animator.SetBool("hasPebble", isHoldingPebble);
        animator.SetBool("hasJewel", isHoldingJewel);
        animator.SetBool("hasTrash", isHoldingTrash);
    }

    void MoveToClickPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Ground") || hit.collider.CompareTag("Pebble") || hit.collider.CompareTag("Pitcher"))
                targetPosition = hit.point;
            targetDest.transform.position = hit.point;
        }
    }

    void MoveTowardsMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Ground") || hit.collider.CompareTag("Pebble") || hit.collider.CompareTag("Pitcher") ||
                hit.collider.CompareTag("Rooster") || hit.collider.CompareTag("Dirt") || hit.collider.CompareTag("Jewel") || hit.collider.CompareTag("Trash"))
            {
                Vector3 tempPosition = hit.point;
                tempPosition.y = characterController.transform.position.y;
                targetPosition = tempPosition;
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
            }
        }
        else if(other.gameObject.CompareTag("Pitcher") && isHoldingPebble)
        {
            isHoldingPebble = false;
            pebblesCollected++;
            pitcherComponent.AddPebble();
        }

    }
}
