using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockPuzzleCanvas : MonoBehaviour
{
    // reference variable parameters
    [SerializeField] private ClockPuzzle clockPuzzle;
    [SerializeField] private TextMeshProUGUI clockPuzzleChanceText;
    [SerializeField] private GameObject successTextGameObject;

    private void Awake()
    {
        clockPuzzle.OnAttemptFailure += ClockPuzzle_OnAttemptFailure;
        clockPuzzle.OnAttemptSuccess += ClockPuzzle_OnAttemptSuccess;
    }

    private void Start()
    {
        successTextGameObject.SetActive(false);
        clockPuzzleChanceText.text = clockPuzzle.GetMaxChances().ToString();
    }
    private void ClockPuzzle_OnAttemptSuccess(object sender, System.EventArgs e)
    {
        successTextGameObject.SetActive(true);
    }

    private void ClockPuzzle_OnAttemptFailure(object sender, ClockPuzzle.OnPuzzleEventArgs e)
    {
        clockPuzzleChanceText.text = e.chances.ToString();
    }
}
