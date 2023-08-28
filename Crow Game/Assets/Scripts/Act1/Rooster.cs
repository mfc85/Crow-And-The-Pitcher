using UnityEngine;
using System.Collections;

public class RoosterDialogue : MonoBehaviour
{

    // Regular dialogue
    public GameObject defaultDialogue;
    public GameObject trashDialogue;
    public GameObject thanksDialogue;

    // Intro dialogue
    public GameObject introDialogue1;
    public GameObject introDialogue2;
    public GameObject introDialogue3;
    public GameObject introDialogue4;
    public GameObject introDialogue5;
    public GameObject introDialogue6;
    public GameObject introDialogue7;

    private bool isIntroPlaying = false;

    // "Outro" dialogue
    public GameObject jewelDialogue1;
    public GameObject jewelDialogue2;
    public GameObject jewelDialogue3;
    public GameObject jewelDialogue4;

    private bool isJewelSequencePlaying = false;


    public float dialogueDuration = 5f;
    private float lastInteraction = 0f;

    private bool hasReceivedJewel = false;
    public static bool hasInteractedWithRooster = false;
    private bool hasShownIntro = false;

    public CrowMovement crowMovement;
    public Animator pitcherAnimator;
    public CameraController cameraController;

    public GameObject pebbleFour;
    public GameObject pebbleFive;

    private Coroutine dialogueCoroutine;

    private void Start()
    {
	defaultDialogue.SetActive(false);
	trashDialogue.SetActive(false);
	thanksDialogue.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crow"))
        {
            if (!hasShownIntro)
            {
                hasInteractedWithRooster = true;

                isIntroPlaying = true;
                StartCoroutine(PlayIntroDialogue());
                hasShownIntro = true;
                return;
            }

            if (isIntroPlaying || isJewelSequencePlaying) return;

            else if (Time.time - lastInteraction > dialogueDuration)
            {

                if (crowMovement.isHoldingTrash)
                {
                    trashDialogue.SetActive(true);
                    crowMovement.isHoldingTrash = false;
                }
                else if (crowMovement.isHoldingJewel)
                {
                    isJewelSequencePlaying = true;

                    crowMovement.isHoldingJewel = false;
                    hasReceivedJewel = true;

                    StartCoroutine(PlayJewelDialogueSequence());
                    
                }
                else if (hasReceivedJewel)
                {
                    thanksDialogue.SetActive(true);
                }
                else
                {
                    defaultDialogue.SetActive(true);
                }

                lastInteraction = Time.time;
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
	if(other.CompareTag("Crow"))
	{
            if(dialogueCoroutine != null)
	    {
		StopCoroutine(dialogueCoroutine);
	    }
            dialogueCoroutine = StartCoroutine(HideDialogueAfterDelay());
        }
    }

    private IEnumerator PlayIntroDialogue()
    {
        crowMovement.canMove = false;
        crowMovement.animator.SetBool("isWalking", false);

        crowMovement.targetPosition = crowMovement.characterController.transform.position;
        crowMovement.targetDest.transform.position = crowMovement.targetDestOriginalPosition;

        //Rooster Intro Dialogue
        introDialogue1.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        introDialogue1.SetActive(false);

        introDialogue2.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        introDialogue2.SetActive(false);

        introDialogue3.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        introDialogue3.SetActive(false);

        introDialogue4.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        introDialogue4.SetActive(false);

        //Narrator Intro Dialogue
        introDialogue5.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        introDialogue5.SetActive(false);

        introDialogue6.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        introDialogue6.SetActive(false);

        introDialogue7.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        introDialogue7.SetActive(false);

        cameraController.PlayAct1PanAnimation();
        transform.Rotate(0, 180, 0);

        crowMovement.canMove = true;
        isIntroPlaying = false;

    }

    private IEnumerator PlayJewelDialogueSequence()
    {
        crowMovement.animator.SetBool("isWalking", false);

        crowMovement.targetPosition = crowMovement.characterController.transform.position;
        crowMovement.targetDest.transform.position = crowMovement.targetDestOriginalPosition;

        jewelDialogue1.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        jewelDialogue1.SetActive(false);

        jewelDialogue2.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        jewelDialogue2.SetActive(false);

        jewelDialogue3.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        jewelDialogue3.SetActive(false);

        jewelDialogue4.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        jewelDialogue4.SetActive(false);

        pebbleFour.SetActive(true);
        pebbleFive.SetActive(true);

        pitcherAnimator.Play("PitcherMovement");

        crowMovement.canMove = true;
        isJewelSequencePlaying = false;
    }
    
    IEnumerator HideDialogueAfterDelay()
    {
	yield return new WaitForSeconds(dialogueDuration);
	
	defaultDialogue.SetActive(false);
	trashDialogue.SetActive(false);
	thanksDialogue.SetActive(false);
    }
}



	

	