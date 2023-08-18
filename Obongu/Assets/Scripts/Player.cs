using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    // constants
    private const string ATTACK_SFX_NAME = "slash";
    private const string PLAYER_HURT_SFX_NAME = "player-hurt";
    private const string PLAYER_DEATH_SFX_NAME = "death";

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
    [SerializeField] private LayerMask brickLayerMask;
    [SerializeField] private HealthSO playerHealthSO;
    [SerializeField] private PlayerCanvas playerCanvas;
    [SerializeField] private CameraShake playerCamera;

    // sfx params
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private float walkPitch = 1.6f;
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
    private float timeSinceDamageTaken = 0f;
    private AudioSource audioSource;
    private PolygonCollider2D footCollider;

    private void Awake()
    {
        currentHp = playerHealthSO.maxHp;
        inputManager = GetComponent<InputManager>();
        inputManager.OnPlayerAttack += InputManager_OnPlayerAttack;
        inputManager.OnPlayerInteract += InputManager_OnPlayerInteract;
        inputManager.OnPlayerAltInteract += InputManager_OnPlayerAltInteract;
        LevelManager.OnSceneChange += Instance_OnSceneChange;
    }

    private void Instance_OnSceneChange(object sender, EventArgs e)
    {
        inputManager.OnPlayerAttack -= InputManager_OnPlayerAttack;
        inputManager.OnPlayerInteract -= InputManager_OnPlayerInteract;
        inputManager.OnPlayerAltInteract -= InputManager_OnPlayerAltInteract;
        LevelManager.OnSceneChange -= Instance_OnSceneChange;
    }

    private void InputManager_OnPlayerAltInteract(object sender, EventArgs e)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lastMovementDir, movementSpeed, objectsLayerMask);
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lastMovementDir, movementSpeed, objectsLayerMask);
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
        {
            OnPlayerAttack?.Invoke(this, EventArgs.Empty);
            AudioManager.instance.PlayOneShot(ATTACK_SFX_NAME);
        }
    }

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        isAbleToMove = true;
        isMoving = false;
        isFightingEnemy = false;
        audioSource.clip = clips[0];
        footCollider = GetComponent<PolygonCollider2D>();
    }
    private void Update()
    {
        if (isAbleToMove)
            PlayerMovementInputProcessing();
        RenderInteractHint();
        if(footCollider.IsTouchingLayers(brickLayerMask))
        {
            audioSource.clip = clips[1];
        }
        else
        {
            audioSource.clip = clips[0];
        }
    }

    private void RenderInteractHint()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, lastMovementDir, 1f, objectsLayerMask);
        if (hit.collider)
        {
            if (hit.transform.TryGetComponent<ClockPuzzle>(out ClockPuzzle clockPuzzle))
            {
                playerCanvas.SetHintText(true);
            }
            else
            {
                playerCanvas.SetHintText(false);
            }
        }
        else
        {
            playerCanvas.SetHintText(false);
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
            if (!audioSource.isPlaying)
            {
                audioSource.pitch = walkPitch;
                audioSource.Play();
            }
            OnPlayerMovement?.Invoke(this, new OnPlayerMovementEventArgs()
            {
                movement = this.movement,
                lastMovementDir = this.lastMovementDir
            });
            if (!IsPlayerMoving())
            {
                audioSource.Stop();
                isMoving = false;
                return;
            }
            playerRb.MovePosition(playerRb.position + movementSpeed * Time.fixedDeltaTime * movement);
        }
    }

    public void DamagePlayer(float damageAmount)
    {
        currentHp -= damageAmount;
        if (currentHp <= 0)
        {
            currentHp = 0;
            AudioManager.instance.PlaySound(PLAYER_DEATH_SFX_NAME);
            GameStateManager.instance.PlayerDied();
            inputManager.OnPlayerAttack -= InputManager_OnPlayerAttack;
            inputManager.OnPlayerInteract -= InputManager_OnPlayerInteract;
            inputManager.OnPlayerAltInteract -= InputManager_OnPlayerAltInteract;
            this.gameObject.SetActive(false);
        }
        AudioManager.instance.PlaySound(PLAYER_HURT_SFX_NAME);
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
    public Vector2 GetLastMovementDir()
    {
        return this.lastMovementDir;
    }
}
