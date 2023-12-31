using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpilogueScript : MonoBehaviour
{
    public float dialogueDuration = 5f;

    public CrowMovement characterMovement;

    public GameObject epilogue1;
    public GameObject epilogue2;
    public GameObject epilogue3;
    public GameObject epilogue4;
    public GameObject epilogue5;
    public GameObject epilogue6;
    public GameObject epilogue7;
    public GameObject epilogue8;

    public GameObject crowDrinking;
    public GameObject player;
    public GameObject pitcher;

    public GameObject endCard;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crow"))
        {
            StartCoroutine(PlayEpilogue());
        }
    }

    private IEnumerator PlayEpilogue()
    {
        

        epilogue1.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        epilogue1.SetActive(false);

        epilogue2.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        epilogue2.SetActive(false);

        epilogue3.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        epilogue3.SetActive(false);

        characterMovement.canMove = false;

        player.SetActive(false);
        pitcher.SetActive(false);
        crowDrinking.SetActive(true);

        epilogue4.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        epilogue4.SetActive(false);

        epilogue5.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        epilogue5.SetActive(false);

        epilogue6.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        epilogue6.SetActive(false);

        epilogue7.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        epilogue7.SetActive(false);

        epilogue8.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        epilogue8.SetActive(false);

        crowDrinking.SetActive(false);
        endCard.SetActive(true);
    }
}
