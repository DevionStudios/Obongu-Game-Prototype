using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject hintText;

    private void Awake()
    {
        player.OnPlayerDamaged += Player_OnPlayerDamaged;
    }
    private void Start()
    {
        hintText.SetActive(false);
    }
    private void Player_OnPlayerDamaged(object sender, Player.OnPlayerDamagedEventArgs e)
    {
        float fillAmount = e.currentHp/player.GetHealthSO().maxHp;
        healthBar.transform.localScale = new Vector3(fillAmount, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }

    public void SetHintText(bool visibility)
    {
        hintText.SetActive(visibility);
    }
}
