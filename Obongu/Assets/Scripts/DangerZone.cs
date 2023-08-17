using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    // configure params
    [SerializeField] private float damageInterval = 1f;
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private float forceAmount = 2f;

    // inner params
    private float timePassedSinceLastDamage = 0f;
    private void Update()
    {
        timePassedSinceLastDamage += Time.deltaTime;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (timePassedSinceLastDamage > damageInterval)
        {
            if (collision.TryGetComponent<Player>(out Player player))
            {
                PolygonCollider2D polygonCollider2D = player.GetComponent<PolygonCollider2D>();
                if (polygonCollider2D.IsTouching(this.GetComponent<BoxCollider2D>()))
                {
                    player.GetPlayerCamera().ShakeCamera();
                    player.DamagePlayer(damageAmount);
                    timePassedSinceLastDamage = 0f;
                }
            }
        }
    }
}
