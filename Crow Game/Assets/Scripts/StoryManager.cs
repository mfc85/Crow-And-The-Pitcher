using System.Collections;
using TMPro;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public TextMeshProUGUI storyText;
    public CrowMovement characterMovement;
    public GameObject titleScreenUI;
    public GameObject expositionUI;

    private int currentLine = 0;
    private bool pebbleCompletion = false;
    private float displayDuration = 5.0f;

    private string[] storyLines =
    {
        "This is the story of the Crow",
        "and the Pitcher.",
        "Oh. The Crow is thirsty. We must help the crow fill up the pitcher so he may drink! Put the pebbles in the Pitcher!",
        "Nice! Now onward my friend toward your new adventure!"
    };

    private void Start()
    {
        storyText.text = "";
        titleScreenUI.SetActive(true);
        expositionUI.SetActive(false);
    }

    public void StartStory()
    {
        titleScreenUI.SetActive(false);
        expositionUI.SetActive(true);
        ShowNextLine();
    }

    private void Update()
    {
        if (characterMovement.pebblesCollected == 3 && !pebbleCompletion)
        {
            OnTaskCompleted();
        }
    }

    public void ShowNextLine()
    {
        if (currentLine < storyLines.Length)
        {
            storyText.text = storyLines[currentLine];
            currentLine++;
            if (currentLine == 3)
            {
                characterMovement.canMove = true;
            }
            else if (currentLine < storyLines.Length)
            {
                StopCoroutine("DisplayTextWithDelay");
                StartCoroutine(DisplayTextWithDelay());
            }
        }
    }

    private IEnumerator DisplayTextWithDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        if (currentLine != 3)
        {
            ShowNextLine();
        }
    }

    public void OnTaskCompleted()
    {
        pebbleCompletion = true;
        storyText.text = storyLines[storyLines.Length - 1];
        StartCoroutine(HideLastLineAfterDuration());
    }

    private IEnumerator HideLastLineAfterDuration()
    {
        yield return new WaitForSeconds(displayDuration);
        storyText.text = "";
    }
}
