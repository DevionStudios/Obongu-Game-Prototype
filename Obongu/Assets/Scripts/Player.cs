using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    // events 
    public event EventHandler<OnPlayerMovementEventArgs> OnPlayerMovement;
    public event EventHandler<OnPlayerDamagedEventArgs> OnPlayerDamaged;
    public event EventHandler OnPlayerAttack;
    public class OnPlayerMovementEventArgs : EventArgs
    {
        public Vector2 movement;
        public Vector2 lastMovementDir;
    }
    public class OnPlayerDamagedEventArgs : EventArgs
    {
        public float currentHp;
    }
    // configure parameters
    [SerializeField] private float movementSpeed;
    [SerializeField] private float attackDamage = 50f;

    // reference parameters
    [SerializeField] private LayerMask objectsLayerMask;
    [SerializeField] private HealthSO playerHealthSO;
    [SerializeField] private PlayerCanvas playerCanvas;
    [SerializeField] private CameraShake playerCamera;


    // inner parameters
    private Rigidbody2D playerRb;
    private InputManager inputManager;
    private Vector2 movement;
    private Vector2 lastMovementDir;
    private bool isMoving;
    private bool isAbleToMove;
    private float currentHp;
    private bool isFightingEnemy;
    private GameStateManager.TeleportPoint prevTeleportPoint;

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
            if (hit.transform.TryGetComponent<ClockPuzzle>(out ClockPuzzle clockPuzzle))
            {
                clockPuzzle.Interact(this);
            }
        }
    }

    private void InputManager_OnPlayerAttack(object sender, EventArgs e)
    {
        if (isAbleToMove)
            OnPlayerAttack?.Invoke(this, EventArgs.Empty);
    }

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        isAbleToMove = true;
        isMoving = false;
        isFightingEnemy = false;
        currentHp = playerHealthSO.maxHp;
    }
    private void Update()
    {
        if (isAbleToMove)
            PlayerMovementInputProcessing();
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, lastMovementDir, 1f, objectsLayerMask);
        if (hit.collider)
        {
            // render something to make player know it is interactable
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

    public void DamagePlayer(float damageAmount)
    {
        currentHp -= damageAmount;
        if (currentHp < 0)
        {
            currentHp = 0;
            // render death screen
            Destroy(gameObject);
        }
        OnPlayerDamaged?.Invoke(this, new OnPlayerDamagedEventArgs()
        {
            currentHp = this.currentHp
        });
    }
    public bool IsPlayerMoving()
    {
        return Math.Abs(movement.x) > 0 || Math.Abs(movement.y) > 0;
    }
    public void SetIsAbleToMove(bool isAbleToMove)
    {
        this.isAbleToMove = isAbleToMove;
    }
    public float GetCurrentHp()
    {
        return this.currentHp;
    }
    public HealthSO GetHealthSO()
    {
        return this.playerHealthSO;
    }
    public PlayerCanvas GetPlayerCanvas()
    {
        return this.playerCanvas;
    }
    public float GetPlayerAttackDamage()
    {
        return this.attackDamage;
    }
    public CameraShake GetPlayerCamera()
    {
        return playerCamera;
    }
    public void SetIsFightingEnemy(bool isFighting)
    {
        this.isFightingEnemy = isFighting;
    }
    public bool GetIsFightingEnemy()
    {
        return this.isFightingEnemy;
    }
    public GameStateManager.TeleportPoint GetTeleportPoint()
    {
        return prevTeleportPoint;
    }    
    public void SetTeleportPoint(GameStateManager.TeleportPoint tp)
    {
        this.prevTeleportPoint = tp;
    }
}
