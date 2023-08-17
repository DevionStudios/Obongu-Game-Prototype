using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Enemy : MonoBehaviour
{
    //events
    public event EventHandler<OnEnemyAttackedEventArgs> OnEnemyAttacked;
    public event EventHandler OnEnemyDead;
    public class OnEnemyAttackedEventArgs : EventArgs
    {
        public float currentHP;
    }

    // configure parameter
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackInterval = 1f;
    [SerializeField] private float skillInterval = 2f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float skillDamage = 30f;
    [SerializeField] private float hpReductionInPhase2 = 20f;

    // reference parameters
    [SerializeField] private HealthSO healthSO;

    // inner params
    private bool isFlipped = false;
    private bool isTransitioningToSecondState = false;
    private float currentHP;
    private float currentHpReduction;
    private void Start()
    {
        currentHP = healthSO.maxHp;
        currentHpReduction = 0f;
    }
    public void LookAtPlayer(Player player)
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        if (transform.position.x > player.transform.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
        else if (transform.position.x < player.transform.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (!isTransitioningToSecondState)
        {
            currentHP -= damageAmount - currentHpReduction;
            if (currentHP < 0)
            {
                currentHP = 0f;
                OnEnemyDead?.Invoke(this, EventArgs.Empty);
            }
            OnEnemyAttacked?.Invoke(this, new OnEnemyAttackedEventArgs()
            {
                currentHP = this.currentHP
            });
        }
    }

    public void EnterPhase2()
    {
        isTransitioningToSecondState = false;
        moveSpeed *= 1.5f;
        attackInterval *= 0.8f;
        currentHpReduction = hpReductionInPhase2;
    }
    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
    public float GetMoveSpeed()
    {
        return this.moveSpeed;
    }
    public float GetAttackRange()
    {
        return this.attackRange;
    }
    public float GetAttackInterval()
    {
        return this.attackInterval;
    }
    public float GetCurrentHP()
    {
        return this.currentHP;
    }
    public HealthSO GetEnemyHealthSO()
    {
        return this.healthSO;
    }
    public float GetSkillInterval()
    {
        return this.skillInterval;
    }
    public float GetSkillDamage()
    {
        return this.skillDamage;
    }
    public float GetAttackDamage()
    {
        return this.attackDamage;
    }
    public bool GetIsTransitioningToSecondState()
    {
        return this.isTransitioningToSecondState;
    }
    public void SetIsTransitioningToSecondState(bool transitionState)
    {
        this.isTransitioningToSecondState = transitionState;
    }
}
