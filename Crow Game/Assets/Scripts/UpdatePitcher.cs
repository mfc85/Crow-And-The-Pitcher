using UnityEngine;

public class Pitcher : MonoBehaviour
{


    private string[] pitcherWaterLevelNames =
    {
        "PitcherEmpty",
        "PitcherOne",
        "PitcherTwo",
        "PitcherThree",
        "PitcherFive",
        "PitcherEight"
    };

    private Animator pitcherAnimator;
    private int pebblesInPitcher = 0;

    private void Awake()
    {
        pitcherAnimator = GetComponent<Animator>();
    }

    public void AddPebble()
    {
        pebblesInPitcher++;
        UpdatePitcherSprite();

    }

    private void UpdatePitcherSprite()
    {
        if (pebblesInPitcher < pitcherWaterLevelNames.Length)
        {
            pitcherAnimator.Play(pitcherWaterLevelNames[pebblesInPitcher]);
        }
    }
}
