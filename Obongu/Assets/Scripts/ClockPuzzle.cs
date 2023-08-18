using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClockPuzzle : BasePuzzle, IInteractable
{
    // events
    public event EventHandler<OnPuzzleEventArgs> OnAttemptFailure;
    public event EventHandler<OnAttemptSuccessEventArgs> OnAttemptSuccess;
    public event EventHandler OnInteractEvent;

    public class OnPuzzleEventArgs: EventArgs
    {
        public int chances;
        public bool isACurse;
    }
    public class OnAttemptSuccessEventArgs: EventArgs
    {
        public Player player;
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
    private Player activePlayer;
    private enum PuzzleState
    {
        won, 
        lost,
        incomplete
    };
    private PuzzleState puzzleState;
    
    private void Start()
    {
        currentChances = maxChances;
        puzzleState = PuzzleState.incomplete;
        isTouchingZone = false;
    }
    private void Update()
    {
        if (dial.activeInHierarchy && puzzleState == PuzzleState.incomplete)
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
        if (puzzleDec.activeSelf == false && puzzleState == PuzzleState.incomplete)
        {
            AudioManager.instance.PlaySound("interact");
            // if already activated and pressed k again
            if (isTouchingZone == true)
            {
                OnSuccessfulAttempt(player);
            }
            else
            {
                currentChances--;
                if(currentChances <= 0)
                {
                    // game over, send the character to fight minion or permanently half hp
                    OnFailedAttempt(player);
                }
                OnAttemptFailure?.Invoke(this, new OnPuzzleEventArgs()
                {
                        chances = currentChances,
                        isACurse = false
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
            player.GetPlayerCanvas().gameObject.SetActive(true);
            DeActivatePuzzle(player);
    }

    private void ActivatePuzzle(Player player)
    {
        AudioManager.instance.PlaySound("interact");
        player.SetIsAbleToMove(false);
        player.GetPlayerCanvas().gameObject.SetActive(false);
        puzzleDec.SetActive(false);
        puzzleFunc.SetActive(true);
        if (puzzleState == PuzzleState.incomplete)
        {
            int randomIndex = (int)(UnityEngine.Random.Range(0, 20)) % clockPuzzleZones.Count;
            lastActiveClockPuzzleZone = clockPuzzleZones[randomIndex];
        }
        player.SetIsAbleToMove(false);
        lastActiveClockPuzzleZone.gameObject.SetActive(true);
        OnInteractEvent?.Invoke(this, EventArgs.Empty);
    }

    private void DeActivatePuzzle(Player player)
    {
        puzzleFunc.SetActive(false);
        puzzleDec.SetActive(true);
        AudioManager.instance.PlaySound("interact");
        lastActiveClockPuzzleZone.gameObject.SetActive(false);
        player.SetIsAbleToMove(true);
    }
    protected override void OnFailedAttempt(Player player)
    {
        puzzleState = PuzzleState.lost;
        AudioManager.instance.PlaySound("puzzlefail");
        currentChances = 0;
        activePlayer = player;
    }

    protected override void OnSuccessfulAttempt(Player player)
    {
        if (puzzleState == PuzzleState.incomplete)
        {
            puzzleState = PuzzleState.won;
            OnAttemptSuccess?.Invoke(this, new OnAttemptSuccessEventArgs()
            {
                player = player
            });
            GameStateManager.instance.ObtainedKey();
        }
    }

    public void FightMinion()
    {
        // transfer to the different stage
        DeActivatePuzzle(activePlayer);
        AudioManager.instance.PlaySound("interact");
        activePlayer.GetPlayerCanvas().gameObject.SetActive(true);
        GameStateManager.instance.TeleportPlayer((BasePuzzle)this, activePlayer);
        activePlayer.SetIsAbleToMove(true);
        SelfDestruct();
    }
    public void ReduceHalfHp()
    {
        // reduce half hp of player
        activePlayer.DamagePlayer(activePlayer.GetCurrentHp() / 2f);
        AudioManager.instance.PlaySound("interact");
        GameStateManager.instance.ObtainedKey();
        activePlayer.SetIsAbleToMove(true);
        activePlayer.GetPlayerCanvas().gameObject.SetActive(true);
        OnAttemptFailure?.Invoke(this, new OnPuzzleEventArgs()
        {
            chances = currentChances,
            isACurse = true
        });
        SelfDestruct();
    }
    public void SelfDestruct()
    {
        Destroy(gameObject);
    }
    // getter and setter functions
    public int GetMaxChances()
    {
        return maxChances;
    }
}
