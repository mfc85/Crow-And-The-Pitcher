using UnityEngine;
using UnityEngine.UI;

public class PeacockMinigame : MonoBehaviour
{
    public GameObject minigameUI;
    public RectTransform indicator;
    public RectTransform successZone;
    public RectTransform sliderBox;
    public float baseSpeed = 5f; // Base speed of the indicator
    public float speedIncrement = 1.5f; // Increase in speed for every play
    private float currentSpeed; // Current speed of the indicator

    public CrowMovement crowMovement;
    public Animator peacockAnimator;

    public AudioClip yoink;
    public AudioSource soundSource;

    private bool movingRight = true;
    private int playCount = 0; // Counter for how many times the game has been played
    private const int maxPlayCount = 3; // Maximum times the game can be played

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crow") && playCount < maxPlayCount)
        {
            if (!crowMovement.isHoldingFeather)
            {
                StartMinigame();
            }
        }
    }

    private void StartMinigame()
    {
        currentSpeed = baseSpeed + (speedIncrement * (playCount - 1)); // Adjust speed based on play count

        crowMovement.canMove = false;
        crowMovement.animator.SetBool("isWalking", false);

        crowMovement.targetPosition = crowMovement.characterController.transform.position;
        crowMovement.targetDest.transform.position = crowMovement.targetDestOriginalPosition;

        minigameUI.SetActive(true);
    }

    private void Update()
    {
        if (!minigameUI.activeSelf)
            return;

        MoveIndicator();

        if (Input.GetMouseButtonDown(0))
        {
            CheckSuccess();
        }

        if (playCount == 3)
        {
            peacockAnimator.Play("PeacockWalk");
        }
    }

    void MoveIndicator()
    {
        float sliderBoundary = sliderBox.rect.width / 2 - indicator.rect.width / 2;

        if (movingRight)
        {
            indicator.localPosition += Vector3.right * currentSpeed * Time.deltaTime;

            if (indicator.localPosition.x >= sliderBoundary)
            {
                movingRight = false;
            }
        }
        else
        {
            indicator.localPosition -= Vector3.right * currentSpeed * Time.deltaTime;

            if (indicator.localPosition.x <= -sliderBoundary)
            {
                movingRight = true;
            }
        }
    }

    void CheckSuccess()
    {
        if (indicator.localPosition.x >= successZone.localPosition.x - successZone.rect.width / 2 &&
            indicator.localPosition.x <= successZone.localPosition.x + successZone.rect.width / 2)
        {
            EndMinigame();
        }
    }

    private void EndMinigame()
    {
        soundSource.clip = yoink;
        soundSource.Play();

        minigameUI.SetActive(false);
        crowMovement.canMove = true;
        crowMovement.isHoldingFeather = true;
        playCount++;
    }
}
