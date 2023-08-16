using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameStateManager : MonoBehaviour
{
    //events
    public static event EventHandler<OnKeyObtainEventArgs> OnKeyObtain;

    public class OnKeyObtainEventArgs : EventArgs
    {
        public int keyObtained;
    }
    
    // instance 
    public static GameStateManager instance;

    // inner variables
    private int keyObtained;
    private int totalKeys;
    private void Awake()
    {
        if (instance != null)
            Debug.LogError("Warning, more than one instances of " + this.name);
        instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        keyObtained = 0;
        BasePuzzle[] puzzles = FindObjectsOfType<BasePuzzle>();
        totalKeys = puzzles.Length;
    }

    public void ObtainedKey()
    {
        keyObtained++;
        OnKeyObtain?.Invoke(this, new OnKeyObtainEventArgs()
        {
            keyObtained = this.keyObtained
        });
        if(keyObtained == totalKeys)
        {
            // logic to open final gate
            Debug.Log("Open gate");
        }
    }

}
