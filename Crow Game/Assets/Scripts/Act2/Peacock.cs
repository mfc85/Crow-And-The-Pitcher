using UnityEngine;
using UnityEngine.UI;

public class PeacockMinigame : MonoBehaviour
{
    public RectTransform indicator; // Slider indicator (shows where you'd hit if you click)
    public RectTransform successZone; // Where you WANT to hit/click
    public RectTransform sliderBox; // The box the indicator moves within
    public float speed = 5f; // Speed that the indicator moves back and forth

    public GameObject minigameUI; // Parent object containing Indicator, Success Zone, and Slider Box
    public CrowMovement crowMovement;

    private bool movingRight = true;
    private int successCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crow"))
        {
            if (!crowMovement.isHoldingFeather && successCount < 3)
            {
                StartMinigame();
            }
        }
    }

    private void StartMinigame()
    {
        crowMovement.canMove = false;
        crowMovement.animator.SetBool("isWalking", false);
        crowMovement.targetPosition = crowMovement.characterController.transform.position;
        crowMovement.targetDest.transform.position = crowMovement.targetDestOriginalPosition;

        minigameUI.SetActive(true); // Activate the UI elements
        this.enabled = true;
    }

    private void Update()
    {
        MoveIndicator();

        if (Input.GetMouseButtonDown(0))
        {
            CheckSuccess();
        }
    }

    void MoveIndicator()
    {
        if (movingRight)
        {
            indicator.localPosition += Vector3.right * speed * Time.deltaTime;

            if (indicator.localPosition.x >= (sliderBox.rect.width / 2) - (indicator.rect.width / 2))
            {
                movingRight = false;
            }
        }
        else
        {
            indicator.localPosition -= Vector3.right * speed * Time.deltaTime;

            if (indicator.localPosition.x <= -(sliderBox.rect.width / 2) + (indicator.rect.width / 2))
            {
                movingRight = true;
            }
        }
    }

    void CheckSuccess()
    {
        if (indicator.localPosition.x >= -successZone.rect.width / 2 && indicator.localPosition.x <= successZone.rect.width / 2)
        {
            // Success !!!
            successCount++;
            EndMinigame();
        }
    }

    private void EndMinigame()
    {
        this.enabled = false;
        crowMovement.canMove = true;
        crowMovement.isHoldingFeather = true;

        minigameUI.SetActive(false); // Deactivate the UI elements
    }
}
