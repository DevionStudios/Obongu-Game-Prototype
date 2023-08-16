using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePuzzle : MonoBehaviour
{
    protected virtual void OnSuccessfulAttempt(Player player)
    {
        Debug.Log("Base SuccessfulAttempt Called");
    }

    protected virtual void OnFailedAttempt(Player player)
    {
        Debug.Log("Base Failed Attempt Called");
    }
}
