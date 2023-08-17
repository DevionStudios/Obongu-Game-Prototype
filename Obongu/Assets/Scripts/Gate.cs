using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] bool isExitGate = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isExitGate)
        {
            if (collision.TryGetComponent<Player>(out Player player))
            {
                player.transform.position = player.GetTeleportPoint().sourceTransform.position;
            }
        }
        else
        {
            if (collision.TryGetComponent<Player>(out Player player))
            {
                GameStateManager.instance.TeleportPlayerToBoss(player);
            }
        }
    }
    public void GateOpen()
    {
        if(TryGetComponent<Animator>(out Animator anim))
        {
            anim.SetTrigger("Open");
        }
    }
}
