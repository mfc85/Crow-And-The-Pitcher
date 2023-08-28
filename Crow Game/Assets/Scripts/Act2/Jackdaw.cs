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
    public GameObject IntroJackdaw3;
    public GameObject IntroJackdaw4;
    public GameObject IntroJackdaw5;

    private bool isIntroPlaying = false;

    // "Outro" Dialogue
    public GameObject finalFeatherDialogue1;
    public GameObject finalFeatherDialogue2;
    public GameObject finalFeatherDialogue3;
    public GameObject finalFeatherDialogue4;

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

    public Animator jackdawAnimator;

    private bool featherDialogueTriggered = false;

    private void Start()
    {
        defaultDialogue.SetActive(false);
        featherDialogue.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("OnTriggerEnter called. isHoldingFeather: " + crowMovement.isHoldingFeather);


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
                return;
            }


            else if (Time.time - lastInteraction > dialogueDuration)
            {

                if (crowMovement.isHoldingFeather && numberOfFeathersGiven == 2)
                {
                    crowMovement.isHoldingFeather = false;
                    numberOfFeathersGiven++;
                    UpdateJackdawSprite();

                    StartCoroutine(PlayFinalFeatherDialogue());

                }

                else if (crowMovement.isHoldingFeather)
                {
                    Debug.Log("Playing Feather Dialogue");
                    featherDialogue.SetActive(true);
                    crowMovement.isHoldingFeather = false;
                    numberOfFeathersGiven++;

                    UpdateJackdawSprite();

                    featherDialogueTriggered = true;
                }

                else if(!featherDialogueTriggered)
                {
                    Debug.Log("Playing Default Dialogue");
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

            featherDialogueTriggered = false;
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

        IntroJackdaw3.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        IntroJackdaw3.SetActive(false);

        IntroJackdaw4.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        IntroJackdaw4.SetActive(false);

        IntroJackdaw5.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        IntroJackdaw5.SetActive(false);

        cameraController.PlayAct2PanAnimation();
        transform.Rotate(0, 180, 0);

        crowMovement.canMove = true;
        isIntroPlaying = false;
    }

    private IEnumerator PlayFinalFeatherDialogue()
    {

        finalFeatherDialogue1.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        finalFeatherDialogue1.SetActive(false);

        finalFeatherDialogue2.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        finalFeatherDialogue2.SetActive(false);

        finalFeatherDialogue3.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        finalFeatherDialogue3.SetActive(false);

        finalFeatherDialogue4.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        finalFeatherDialogue4.SetActive(false);


        pebbleSix.SetActive(true);
        pebbleSeven.SetActive(true);

        GetComponent<BoxCollider>().enabled = false;

        isFinalSequencePlaying = false;
    }

    IEnumerator HideDialogueAfterDelayAct2()
    {
        yield return new WaitForSeconds(dialogueDuration);

        defaultDialogue.SetActive(false);
        featherDialogue.SetActive(false);
    }

    private void UpdateJackdawSprite()
    {
        Debug.Log("Updated Sprite. Number of feathers: " + numberOfFeathersGiven);

        switch (numberOfFeathersGiven)
        {
            case 1:
                jackdawAnimator.Play("JackdawOneFeather");
                break;
            case 2:
                jackdawAnimator.Play("JackdawTwoFeather");
                break;
            case 3:
                jackdawAnimator.Play("JackdawThreeFeather");
                break;
            default:
                jackdawAnimator.Play("JackdawIdle");
                break;
        }
    }

}