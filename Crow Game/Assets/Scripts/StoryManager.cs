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
    private bool allowAdvance = false;
    private float advanceDelay = 0.5f;

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
        StartCoroutine(InitiateStoryAdvance());
    }

    private IEnumerator InitiateStoryAdvance()
    {
        yield return new WaitForSeconds(advanceDelay);
        allowAdvance = true;
        ShowNextLine();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !pebbleCompletion && allowAdvance)
        {
            ShowNextLine();
        }

        if (characterMovement.pebblesCollected == 3 && pebbleCompletion == false)
        {
            OnTaskCompleted();
        }
    }

    public void ShowNextLine()
    {
        // This keeps the final line from being shown UNTIL all the pebbles are picked up
        if (currentLine < storyLines.Length - 1 || (currentLine == storyLines.Length - 1 && pebbleCompletion))
        {
            storyText.text = storyLines[currentLine];
            currentLine++;

            if (currentLine == 3)
            {
                characterMovement.canMove = true;
            }
        }
        else if (!pebbleCompletion && currentLine == storyLines.Length - 1)
        {
            // Don't advance.
        }
    }

    public void OnTaskCompleted()
    {
        pebbleCompletion = true;
        ShowNextLine();
    }
}
