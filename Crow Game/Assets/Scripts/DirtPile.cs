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

    private void OnTriggerEnter(Collider collision) // Corrected this line
    {
        if(collision.CompareTag("Crow"))
        {
            StartDigging();
        }
    }

    private void StartDigging()
    {
        characterMovement.canMove = false;
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

        GameObject item;
        if (pilesDug == 3)
        {
            item = Instantiate(jewelPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            item = Instantiate(trashPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
