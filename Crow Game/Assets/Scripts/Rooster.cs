using UnityEngine;
using System.Collections;

public class RoosterDialogue : MonoBehaviour
{
    public GameObject defaultDialogue;
    public GameObject trashDialogue;
    public GameObject jewelDialogue;
    public GameObject thanksDialogue;

    public float dialogueDuration = 5f;
    private float lastInteraction = 0f;

    private bool hasReceivedJewel = false;
    public static bool hasInteractedWithRooster = false;

    public CrowMovement crowMovement;
    public Animator pitcherAnimator;

    public GameObject pebbleFour;
    public GameObject pebbleFive;

    private Coroutine dialogueCoroutine;

    private void Start()
    {
	defaultDialogue.SetActive(false);
	trashDialogue.SetActive(false);
	jewelDialogue.SetActive(false);
	thanksDialogue.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {

	if(other.CompareTag("Crow") && Time.time - lastInteraction > dialogueDuration && hasReceivedJewel == true)
	{
            thanksDialogue.SetActive(true);
	}
	
	else if(other.CompareTag("Crow") && Time.time - lastInteraction > dialogueDuration)
	{
	    hasInteractedWithRooster = true;

            if(crowMovement.isHoldingTrash)
            {

		trashDialogue.SetActive(true);

		crowMovement.isHoldingTrash = false;

            }
            else if(crowMovement.isHoldingJewel)
            {

		jewelDialogue.SetActive(true);

		pebbleFour.SetActive(true);
		pebbleFive.SetActive(true);

		hasReceivedJewel = true;
		
		pitcherAnimator.Play("PitcherMovement");

		crowMovement.isHoldingJewel = false;

            }
            else
            {
		defaultDialogue.SetActive(true);

            }
            
            lastInteraction = Time.time;
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
    
    IEnumerator HideDialogueAfterDelay()
    {
	yield return new WaitForSeconds(dialogueDuration);
	
	defaultDialogue.SetActive(false);
	trashDialogue.SetActive(false);
	jewelDialogue.SetActive(false);
	thanksDialogue.SetActive(false);
    }
}



	

	