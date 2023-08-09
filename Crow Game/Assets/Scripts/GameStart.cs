using UnityEngine;
using UnityEngine.UI;

public class PlayGameButton : MonoBehaviour
{
    public Button playButton;
    public GameObject UICanvas;  // Drag the entire UI Canvas here
    public Animator cameraAnimator;  // Drag the Camera (with the Animator component) here

    private bool animationPlayed = false;  // Flag to check if animation has played

    private void Start()
    {
        playButton.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        if (!animationPlayed) // Check if animation hasn't been played
        {
            UICanvas.SetActive(false);  // Hides the entire UI
            cameraAnimator.Play("CameraIntroAnimation");
            animationPlayed = true;  // Set the flag to true
        }
    }
}
