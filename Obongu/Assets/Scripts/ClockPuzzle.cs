using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClockPuzzle : BasePuzzle, IInteractable
{
    // events
    public event EventHandler<OnPuzzleEventArgs> OnAttemptFailure;
    public event EventHandler OnAttemptSuccess;
    public event EventHandler OnInteractEvent;

    public class OnPuzzleEventArgs: EventArgs
    {
        public int chances;
    }

    // reference variable parameters
    [SerializeField] private GameObject puzzleDec;
    [SerializeField] private GameObject puzzleFunc;
    [SerializeField] private List<ClockPuzzleZone> clockPuzzleZones;
    [SerializeField] private GameObject dial;
    
    // Configuration parameter
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private int maxChances = 3;

    // inner parameters
    private ClockPuzzleZone lastActiveClockPuzzleZone;
    private bool isTouchingZone;
    private int currentChances;
    private bool hasWon;
    
    private void Start()
    {
        currentChances = maxChances;
        hasWon = false;
        isTouchingZone = false;
    }
    private void Update()
    {
        if (dial.activeInHierarchy && !hasWon)
        {
            dial.transform.Rotate(rotateSpeed * Time.deltaTime * Vector3.forward);
            if(dial.TryGetComponent<PolygonCollider2D>(out PolygonCollider2D dialCollider))
            {
                if (dialCollider.IsTouching(lastActiveClockPuzzleZone.GetComponent<BoxCollider2D>()))
                {
                    isTouchingZone = true;
                }
                else
                {
                    isTouchingZone = false;
                }
            }
        }
    }
    public void Interact(Player player)
    {
        if (puzzleDec.activeSelf == false && hasWon == false)
        {
            // if already activated and pressed k again
            if(isTouchingZone == true)
            {
                hasWon = true;
                OnAttemptSuccess?.Invoke(this, EventArgs.Empty);
                GameStateManager.instance.ObtainedKey();
            }
            else
            {
                currentChances--;
                if(currentChances <= 0)
                {
                    // game over, send the character to fight minion or permanently half hp
                    currentChances = 0;
                }
                OnAttemptFailure?.Invoke(this, new OnPuzzleEventArgs()
                {
                        chances = currentChances
                });
            }
        }
        else
        {
            ActivatePuzzle(player);
        }
    }
    public void InteractAlternate(Player player)
    {
        if(puzzleDec.activeSelf == false)
            DeActivatePuzzle(player);
    }

    private void ActivatePuzzle(Player player)
    {
        player.SetIsAbleToMove(false);
        puzzleDec.SetActive(false);
        puzzleFunc.SetActive(true);
        if (!hasWon)
        {
            int randomIndex = (int)(UnityEngine.Random.Range(0, 20)) % clockPuzzleZones.Count;
            lastActiveClockPuzzleZone = clockPuzzleZones[randomIndex];
        }
        lastActiveClockPuzzleZone.gameObject.SetActive(true);
        OnInteractEvent?.Invoke(this, EventArgs.Empty);
    }

    private void DeActivatePuzzle(Player player)
    {
        puzzleFunc.SetActive(false);
        puzzleDec.SetActive(true);
        lastActiveClockPuzzleZone.gameObject.SetActive(false);
        player.SetIsAbleToMove(true);
    }

    // getter and setter functions
    public int GetMaxChances()
    {
        return maxChances;
    }
}
