using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private bool isSkill;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
            player.GetPlayerCamera().ShakeCamera();
            if (isSkill)
                player.DamagePlayer(enemy.GetSkillDamage());
            else
                player.DamagePlayer(enemy.GetAttackDamage());
        }
    }
}
