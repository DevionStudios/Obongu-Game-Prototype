using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class BasePuzzle : MonoBehaviour
{
    [SerializeField] public TeleportPointSO teleportPointSO;
    protected virtual void OnSuccessfulAttempt(Player player)
    {
        Debug.Log("Base SuccessfulAttempt Called");
    }

    protected virtual void OnFailedAttempt(Player player)
    {
        Debug.Log("Base Failed Attempt Called");
    }
}
