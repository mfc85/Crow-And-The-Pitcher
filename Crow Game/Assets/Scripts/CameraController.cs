using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Button playButton;
    public GameObject UICanvas;
    public Animator cameraAnimator;

    public GameObject arrowSign; 

    private bool animationPlayed = false;

    private void Start()
    {
        playButton.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        if (!animationPlayed)
        {
            UICanvas.SetActive(false);
            cameraAnimator.Play("CameraIntroAnimation");
            animationPlayed = true;
        }
    }

    // Public function to start the Act 1 Transition animation
    public void PlayAct1TransitionAnimation()
    {
        cameraAnimator.Play("CameraAct1Transition");
        arrowSign.SetActive(true);
    }
}
