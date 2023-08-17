using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySecondStateBehaviour : StateMachineBehaviour
{
    private Player player;
    private Rigidbody2D enemyRb;
    private Enemy enemy;
    private float lastAttackTime;
    private float lastSkillTime;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindObjectOfType<Player>();
        enemyRb = animator.GetComponent<Rigidbody2D>();
        enemy = animator.GetComponent<Enemy>();
        lastSkillTime = Mathf.Infinity;
        lastAttackTime = Mathf.Infinity;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null)
            return;
        lastAttackTime += Time.fixedDeltaTime;
        lastSkillTime += Time.fixedDeltaTime;
        if (enemy.GetCurrentHP() <= 0f)
        {
            player.SetIsFightingEnemy(false);
            GameStateManager.instance.ActivateExitGate();
            GameStateManager.instance.ObtainedKey();
            animator.SetTrigger("Death");
        }
        if (Vector2.Distance(player.transform.position, enemyRb.position) <= enemy.GetAttackRange())
        {
            if(lastSkillTime > enemy.GetSkillInterval())
            {
                animator.SetTrigger("Skill");
                lastSkillTime = 0f;
                lastAttackTime = 0f;
            }
            else if (lastAttackTime > enemy.GetAttackInterval())
            {
                animator.SetTrigger("Attack");
                lastAttackTime = 0f;
            }
        }
        else
        {
            enemy.LookAtPlayer(player);
            Vector2 target = new Vector2(player.transform.position.x, player.transform.position.y);
            Vector2 newPos = Vector2.MoveTowards(enemyRb.position, target, enemy.GetMoveSpeed() * Time.fixedDeltaTime);
            enemyRb.MovePosition(newPos);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Skill");
        animator.ResetTrigger("Attack");
    }
}
