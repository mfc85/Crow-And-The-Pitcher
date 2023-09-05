using System.Collections;
using TMPro;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public TextMeshProUGUI storyText;
    public CrowMovement characterMovement;
    public GameObject titleScreenUI;
    

   // private int currentLine = 0;
    private bool pebbleCompletion = false;
    public float dialogueDuration = 5f;
    private float lastInteraction = 0f;

    public AudioClip buttonPress;
    public AudioSource soundSource; 

    public GameObject dialogue1;
    public GameObject dialogue2;
    public GameObject dialogue3;
    public GameObject dialogue4;
    public GameObject dialogue5;
    public GameObject dialogue6;
    public GameObject dialogue7;

    // private string[] storyLines =
    //{
    //    "This is the story of the Crow",
    //    "and the Pitcher.",
    //    "Oh. The Crow is thirsty. We must help the crow fill up the pitcher so he may drink! Put the pebbles in the Pitcher!",
    ////    "Nice! Now onward my friend toward your new adventure!"
    // };

    private void Start()
    {
        //storyText.text = "";
        titleScreenUI.SetActive(true);
        dialogue1.SetActive(false);
        dialogue2.SetActive(false);
        dialogue3.SetActive(false);
        dialogue4.SetActive(false);
        dialogue5.SetActive(false);
        dialogue6.SetActive(false);
        dialogue7.SetActive(false);


    }

    public void StartStory()
    {
        soundSource.clip = buttonPress;
        soundSource.Play();
        titleScreenUI.SetActive(false);
        StartCoroutine(PlayProDialogue());
       // ShowNextLine();
    }

    private void Update()
    {
        if (characterMovement.pebblesCollected == 3 && !pebbleCompletion)
        {
            StartCoroutine(OnTaskCompleted());
        }
    }

    private IEnumerator PlayProDialogue()
    {
        dialogue1.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        dialogue1.SetActive(false);

        dialogue2.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        dialogue2.SetActive(false);

        dialogue3.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        dialogue3.SetActive(false);

        dialogue4.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        dialogue4.SetActive(false);

        characterMovement.canMove = true;

        dialogue5.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        dialogue5.SetActive(false);


        

    }

    //public void ShowNextLine()
    //{
    //    if (currentLine < storyLines.Length)
    //    {
    //        storyText.text = storyLines[currentLine];
    //        currentLine++;
    //        if (currentLine == 3)
    //        {
    //            characterMovement.canMove = true;
    //        }
    //        else if (currentLine < storyLines.Length)
    //        {
    //            StopCoroutine("DisplayTextWithDelay");
    //            StartCoroutine(DisplayTextWithDelay());
    //        }
    //    }
    //}

    //private IEnumerator DisplayTextWithDelay()
    //{
    //    yield return new WaitForSeconds(displayDuration);
    //    if (currentLine != 3)
    //    {
    //        ShowNextLine();
    //    }
    //}

    public IEnumerator OnTaskCompleted()
    {

        //storyText.text = storyLines[storyLines.Length - 1];
        dialogue6.SetActive(true);
        dialogue7.SetActive(true);
        yield return new WaitForSeconds(dialogueDuration);
        dialogue6.SetActive(false);
        dialogue7.SetActive(false);

        pebbleCompletion = true;

        StartCoroutine(HideLastLineAfterDuration());
    }

    private IEnumerator HideLastLineAfterDuration()
    {
        yield return new WaitForSeconds(dialogueDuration);

        dialogue1.SetActive(false);
        dialogue2.SetActive(false);
        dialogue3.SetActive(false);
        dialogue4.SetActive(false);
        dialogue5.SetActive(false);
        dialogue6.SetActive(false);
        dialogue7.SetActive(false);
    }
}
