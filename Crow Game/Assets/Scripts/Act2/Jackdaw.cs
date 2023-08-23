using UnityEngine;
using System.Collections;

public class JackdawDialogue: MonoBehaviour
{
    // Regular Dialogue
    public GameObject defaultDialogue;
    public GameObject featherDialogue;

    // Intro Dialogue
    public GameObject IntroJackdaw1;
    public GameObject IntroJackdaw2;

    private bool isIntroPlaying = false;

    // "Outro" Dialogue
    public GameObject finalFeatherDialogue1;
    public GameObject finalFeatherDialogue2;

    private bool isFinalSequencePlaying = false;

    public float dialogueDuration = 5f;
    private float lastInteraction = 0f;

    private int numberOfFeathersGiven = 0;
    public static bool hasInteractedWithJackdaw = false;
    private bool hasShownIntro = false;

    public CrowMovement crowMovement;
    public CameraController cameraController;

    public GameObject pebbleSix;
    public GameObject pebbleSeven;

    private Coroutine dialogueCoroutine;

    private void Start()
    {
        defaultDialogue.SetActive(false);
        featherDialogue.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crow"))
        {
            if (!hasShownIntro)
            {
                hasInteractedWithJackdaw = true;

                isIntroPlaying = true;
                StartCoroutine(PlayIntroDialogue());
                hasShownIntro = true;
                return;
            }

            if (isIntroPlaying || isFinalSequencePlaying)
            {
                Debug.Log("Cannot Continute");
                return;
            }


            else if (Time.time - lastInteraction > dialogueDuration)
            {
                Debug.Log("Crow Can Interact");
                if (crowMovement.isHoldingFeather)
                {
                    featherDialogue.SetActive(true);
                    crowMovement.isHoldingFeather = false;
                    numberOfFeathersGiven++;
                }

                if(crowMovement.isHoldingFeather & numberOfFeathersGiven == 2)
                {
                    crowMovement.isHoldingFeather = false;
                    StartCoroutine(PlayFinalFeatherDialogue());

                }

                else
                {
                    Debug.Log("Crow Is holding Nothing");
                    defaultDialogue.SetActive(true);
                }


                lastInteraction = Time.time;
            }

            else
            {
                Debug.Log("Not enough time has passed since last interaction");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Crow"))
        {
            if (dialogueCoroutine != null)
            {
                StopCoroutine(dialogueCoroutine);
            }
            dialogueCoroutine = StartCoroutine(HideDialogueAfterDelayAct2());
        }
    }

    private IEnumerator PlayIntroDialogue()
    {
        crowMovement.canMove = false;
        crowMovement.animator.SetBool("isWalking", false);

        crowMovement.targetPosition = crowMovement.characterController.transform.position;
        crowMovement.targetDest.transform.position = crowMovement.targetDestOriginalPosition;

        IntroJackdaw1.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        IntroJackdaw1.SetActive(false);

        IntroJackdaw2.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        IntroJackdaw2.SetActive(false);

        cameraController.PlayAct2PanAnimation();
        transform.Rotate(0, 180, 0);

        crowMovement.canMove = true;
        isIntroPlaying = false;
    }

    private IEnumerator PlayFinalFeatherDialogue()
    {
        crowMovement.canMove = false;
        crowMovement.animator.SetBool("isWalking", false);

        crowMovement.targetPosition = crowMovement.characterController.transform.position;
        crowMovement.targetDest.transform.position = crowMovement.targetDestOriginalPosition;

        finalFeatherDialogue1.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        finalFeatherDialogue1.SetActive(false);

        finalFeatherDialogue2.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        finalFeatherDialogue2.SetActive(false);

        pebbleSix.SetActive(true);
        pebbleSeven.SetActive(true);

        crowMovement.canMove = true;
        isFinalSequencePlaying = false;
    }

    IEnumerator HideDialogueAfterDelayAct2()
    {
        yield return new WaitForSeconds(dialogueDuration);

        defaultDialogue.SetActive(false);
        featherDialogue.SetActive(false);
        finalFeatherDialogue1.SetActive(false);
        finalFeatherDialogue2.SetActive(false);
    }
}