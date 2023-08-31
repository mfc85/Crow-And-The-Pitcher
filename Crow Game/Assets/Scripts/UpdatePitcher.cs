using UnityEngine;

public class Pitcher : MonoBehaviour
{
    private string[] pitcherWaterLevelNames =
    {
        "PitcherEmpty",
        "PitcherOne",
        "PitcherTwo",
        "PitcherThree",
	    "PitcherFour",
        "PitcherFive",
        "PitcherEight"
    };

    private Animator pitcherAnimator;
    private int pebblesInPitcher = 0;

    public CameraController cameraController;

    private void Awake()
    {
        pitcherAnimator = GetComponent<Animator>();
    }

    public void AddPebble()
    {
        pebblesInPitcher++;
        UpdatePitcherSprite();

        if(pebblesInPitcher == 3)
        {
            cameraController.PlayAct1TransitionAnimation();
        }
        if(pebblesInPitcher == 5)
        {
            cameraController.PlayAct2TransitionAnimation();
        }
        if(pebblesInPitcher == 7)
        {
            cameraController.Act3();
        }
    }

    private void UpdatePitcherSprite()
    {
        if (pebblesInPitcher < pitcherWaterLevelNames.Length)
        {
            pitcherAnimator.Play(pitcherWaterLevelNames[pebblesInPitcher]);
        }
    }
}
