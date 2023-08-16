using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    // events 
    public event EventHandler<OnPlayerMovementEventArgs> OnPlayerMovement;
    public event EventHandler OnPlayerAttack;
    public class OnPlayerMovementEventArgs : EventArgs
    {
        public Vector2 movement;
        public Vector2 lastMovementDir;
    }
    // control parameters
    [SerializeField] private float movementSpeed;

    // reference parameters
    [SerializeField] LayerMask objectsLayerMask;


    // inner parameters
    private Rigidbody2D playerRb;
    private InputManager inputManager;
    private Vector2 movement;
    private Vector2 lastMovementDir;
    private bool isMoving;
    private bool isAbleToMove;
    

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        inputManager.OnPlayerAttack += InputManager_OnPlayerAttack;
        inputManager.OnPlayerInteract += InputManager_OnPlayerInteract;
        inputManager.OnPlayerAltInteract += InputManager_OnPlayerAltInteract;
    }

    private void InputManager_OnPlayerAltInteract(object sender, EventArgs e)
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, lastMovementDir, movementSpeed, objectsLayerMask);
        if (hit.collider)
        {
            if (hit.transform.TryGetComponent<ClockPuzzle>(out ClockPuzzle clockPuzzle))
            {
                clockPuzzle.InteractAlternate(this);
            }
        }
    }

    private void InputManager_OnPlayerInteract(object sender, EventArgs e)
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, lastMovementDir, movementSpeed, objectsLayerMask);
        if (hit.collider)
        {
            if(hit.transform.TryGetComponent<ClockPuzzle>(out ClockPuzzle clockPuzzle))
            {
                clockPuzzle.Interact(this);
            }
        }
    }

    private void InputManager_OnPlayerAttack(object sender, EventArgs e)
    {
        if(isAbleToMove)
            OnPlayerAttack?.Invoke(this, EventArgs.Empty);
    }

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        isAbleToMove = true;
        isMoving = false;
    }
    private void Update()
    {
        if(isAbleToMove)
            PlayerMovementInputProcessing();
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, lastMovementDir, 1f, objectsLayerMask);
        if (hit.collider)
        {
            
        }
    }
    private void PlayerMovementInputProcessing()
    {
        movement = inputManager.GetMovementDirectionNormalized();
        if (movement.x < 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (movement.x > 0)
            transform.localScale = new Vector3(-1, 1, 1);
        if (IsPlayerMoving())
        {
            lastMovementDir = movement.normalized;
            isMoving = true;
        }
    }

    private void FixedUpdate()
    {
        HandlePlayerMovementOnInput();
    }

    private void HandlePlayerMovementOnInput()
    {
        if (isMoving == true)
        {
            OnPlayerMovement?.Invoke(this, new OnPlayerMovementEventArgs()
            {
                movement = this.movement,
                lastMovementDir = this.lastMovementDir
            });
            if (!IsPlayerMoving())
            {
                isMoving = false;
                return;
            }
            playerRb.MovePosition(playerRb.position + movementSpeed * Time.fixedDeltaTime * movement);
        }
    }

    public bool IsPlayerMoving()
    {
        return Math.Abs(movement.x) > 0 || Math.Abs(movement.y) > 0;
    }
    public void SetIsAbleToMove(bool isAbleToMove)
    {
        this.isAbleToMove = isAbleToMove;
    }

}
