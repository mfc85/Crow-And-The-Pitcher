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

    public CrowMovement characterMovement;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Crow") && !characterMovement.isHoldingTrash && RoosterDialogue.hasInteractedWithRooster)
        {
            StartDigging();
        }
    }

    private void StartDigging()
    {
        characterMovement.canMove = false;
	characterMovement.targetPosition = characterMovement.characterController.transform.position;
	characterMovement.targetDest.transform.position = characterMovement.targetDestOriginalPosition;
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
        characterMovement.canMove = true;

        pilesDug++;

        if (pilesDug == 3)
        {
            characterMovement.isHoldingJewel = true;
        }
        else
        {
            characterMovement.isHoldingTrash = true;
        }

        Destroy(gameObject);
    }
}
