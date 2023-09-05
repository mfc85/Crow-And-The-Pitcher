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

    public AudioClip digging;
    public AudioClip jewel;
    public AudioClip trash;
    public AudioSource soundSource;

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

        soundSource.clip = digging;
        soundSource.Play();

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
                soundSource.clip = trash;
                soundSource.Play();
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
            soundSource.clip = jewel;
            soundSource.Play();
        }
        else
        {
            soundSource.clip = trash;
            soundSource.Play();
            crowMovement.isHoldingTrash = true;
        }

        Destroy(gameObject);
    }
}
