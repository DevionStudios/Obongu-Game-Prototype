using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyText;
    private void Awake()
    {
        GameStateManager.OnKeyObtain += GameStateManager_OnKeyObtain;
    }

    private void GameStateManager_OnKeyObtain(object sender, GameStateManager.OnKeyObtainEventArgs e)
    {
        keyText.text = e.keyObtained.ToString();
    }
}
