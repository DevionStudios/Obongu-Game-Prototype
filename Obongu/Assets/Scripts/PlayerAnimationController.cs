using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    // constants
    private const string MOVEMENT_X = "MovementX";
    private const string MOVEMENT_Y = "MovementY";
    private const string LAST_MOVEMENT_DIR_X = "LastMovementDirX";
    private const string LAST_MOVEMENT_DIR_Y = "LastMovementDirY";
    private const string MOVEMENT_SPEED = "MovementSpeed";
    private const string ATTACK_TRIGGER = "Attack";
    // inner private variables
    private Animator playerAnimator;
    private Player player;
    private void Awake()
    {
        player = GetComponent<Player>();
        playerAnimator = GetComponent<Animator>();
        player.OnPlayerMovement += Player_OnPlayerMovement;
        player.OnPlayerAttack += Player_OnPlayerAttack;
    }

    private void Player_OnPlayerAttack(object sender, System.EventArgs e)
    {
        if(playerAnimator != null)
            playerAnimator.SetTrigger(ATTACK_TRIGGER);
    }

    private void Player_OnPlayerMovement(object sender, Player.OnPlayerMovementEventArgs e)
    {
        if(e.movement != Vector2.zero)
        {
            playerAnimator.SetFloat(MOVEMENT_X, e.movement.x);
            playerAnimator.SetFloat(MOVEMENT_Y, e.movement.y);
            playerAnimator.SetFloat(MOVEMENT_SPEED, 1);
        }
        else
        {
            playerAnimator.SetFloat(LAST_MOVEMENT_DIR_X, e.lastMovementDir.x);
            playerAnimator.SetFloat(LAST_MOVEMENT_DIR_Y, e.lastMovementDir.y);
            playerAnimator.SetFloat(MOVEMENT_SPEED, 0);
        }
    }
}
