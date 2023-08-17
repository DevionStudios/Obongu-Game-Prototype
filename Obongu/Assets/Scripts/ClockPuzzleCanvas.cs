using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockPuzzleCanvas : MonoBehaviour
{
    // reference variable parameters
    [SerializeField] private ClockPuzzle clockPuzzle;
    [SerializeField] private TextMeshProUGUI clockPuzzleChanceText;
    [SerializeField] private TextMeshProUGUI clockPuzzleFinalScreenText;
    [SerializeField] private GameObject successTextGameObject;
    [SerializeField] private GameObject failureGameObject;

    private void Awake()
    {
        clockPuzzle.OnAttemptFailure += ClockPuzzle_OnAttemptFailure;
        clockPuzzle.OnAttemptSuccess += ClockPuzzle_OnAttemptSuccess;
    }

    private void Start()
    {
        successTextGameObject.SetActive(false);
        failureGameObject.SetActive(false);
        clockPuzzleChanceText.text = clockPuzzle.GetMaxChances().ToString();
    }
    private void ClockPuzzle_OnAttemptSuccess(object sender, ClockPuzzle.OnAttemptSuccessEventArgs e)
    {
        successTextGameObject.SetActive(true);
    }

    private void ClockPuzzle_OnAttemptFailure(object sender, ClockPuzzle.OnPuzzleEventArgs e)
    {
        if (!e.isACurse)
        {
            clockPuzzleChanceText.text = e.chances.ToString();
            if (e.chances == 0)
            {
                failureGameObject.SetActive(true);
            }
        }
        else
        {
            failureGameObject.SetActive(false);
            successTextGameObject.SetActive(true);
            clockPuzzleFinalScreenText.text = "You Made A Deal With The Devil! Congrats! Press 'L' To Exit";
        };
    }
}
