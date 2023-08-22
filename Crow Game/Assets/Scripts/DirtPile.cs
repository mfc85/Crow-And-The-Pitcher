using UnityEngine;
using UnityEngine.UI;

public class Digging : MonoBehaviour
{
    public GameObject progressBarUI;
    public Slider progressBar;
    public GameObject clickUI;

    public GameObject trashPrefab;
    public GameObject jewelPrefab;

    private static int pilesDug = 0;
    public int neededClicks = 10;
    private int currentClicks = 0;
    private bool isInteracting = false;

    public CrowMovement crowMovement;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Crow") && !crowMovement.isHoldingTrash && RoosterDialogue.hasInteractedWithRooster)
        {
            StartDigging();
        }
    }

    private void StartDigging()
    {
        crowMovement.canMove = false;
        crowMovement.animator.SetBool("isWalking", false);

        crowMovement.targetPosition = crowMovement.characterController.transform.position;
	    crowMovement.targetDest.transform.position = crowMovement.targetDestOriginalPosition;
        isInteracting = true;
        progressBarUI.SetActive(true);
        clickUI.SetActive(true);
    }

    private void Update()
    {
        if(isInteracting && Input.GetMouseButtonDown(0))
        {
            currentClicks++;
            progressBar.value = (float)currentClicks / neededClicks;

            if(currentClicks >= neededClicks)
            {
                FinishDigging();
            }
        }
    }

    private void FinishDigging()
    {
        isInteracting = false;
        progressBarUI.SetActive(false);
        clickUI.SetActive(false);
        crowMovement.canMove = true;

        pilesDug++;

        if (pilesDug == 3)
        {
            crowMovement.isHoldingJewel = true;
        }
        else
        {
            crowMovement.isHoldingTrash = true;
        }

        Destroy(gameObject);
    }
}
