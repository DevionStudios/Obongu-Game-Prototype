using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCanvas : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private Image healthBar;
    private void Awake()
    {
        enemy.OnEnemyAttacked += Enemy_OnEnemyAttacked;
    }

    private void Enemy_OnEnemyAttacked(object sender, Enemy.OnEnemyAttackedEventArgs e)
    {
        healthBar.transform.localScale = new Vector3(e.currentHP / enemy.GetEnemyHealthSO().maxHp, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
}
