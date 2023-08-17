using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameStateManager : MonoBehaviour
{
    //events
    public static event EventHandler<OnKeyObtainEventArgs> OnKeyObtain;
    public static event EventHandler OnTeleportPlayer;


    public class OnKeyObtainEventArgs : EventArgs
    {
        public int keyObtained;
    }

    // custom structure
    [Serializable]
    public struct TeleportPoint
    {
        public Transform sourceTransform;
        public Transform destinationTransform;
        public TeleportPointSO teleportPointSO;
    }

    // reference variables
    [SerializeField] TeleportPoint[] teleportPoints;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform enemySpawnPoint;
    [SerializeField] Transform exitGate;
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
        exitGate.gameObject.SetActive(false);
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

    public void TeleportPlayer(BasePuzzle basePuzzle, Player player)
    {
        foreach(TeleportPoint teleportPoint in teleportPoints)
        {
            if(teleportPoint.teleportPointSO.id == basePuzzle.teleportPointSO.id)
            {
                exitGate.gameObject.SetActive(false);
                Instantiate(enemyPrefab, enemySpawnPoint.transform.position, enemyPrefab.transform.rotation);
                player.transform.position = teleportPoint.destinationTransform.position;
                player.SetIsFightingEnemy(true);
                player.SetTeleportPoint(teleportPoint);
                OnTeleportPlayer?.Invoke(this, EventArgs.Empty);
                return;
            }
        }
    }
    public void ActivateExitGate()
    {
        exitGate.gameObject.SetActive(true);
    }
}

