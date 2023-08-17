using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyText;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loserScreen;
    [SerializeField] private GameObject controlScreen;
    [SerializeField] private GameObject mobileScreenControls;
    private Player player;
    private void Awake()
    {
        controlScreen.SetActive(true);
        GameStateManager.OnKeyObtain += GameStateManager_OnKeyObtain;
        player = FindObjectOfType<Player>();
        GameStateManager.OnWin += GameStateManager_OnWin;
        player.GetComponent<InputManager>().OnPlayerAltInteract += GameCanvas_OnPlayerAltInteract;
        GameStateManager.OnLose += GameStateManager_OnLose;
    }

    private void GameStateManager_OnLose(object sender, System.EventArgs e)
    {
        mobileScreenControls.SetActive(false);
        loserScreen.SetActive(true);
    }

    private void GameCanvas_OnPlayerAltInteract(object sender, System.EventArgs e)
    {
        controlScreen.SetActive(false);
        player.GetComponent<InputManager>().OnPlayerAltInteract -= GameCanvas_OnPlayerAltInteract;
    }

    private void Start()
    {
        winScreen.SetActive(false);
        loserScreen.SetActive(false);
        mobileScreenControls.SetActive(true);
    }
    private void GameStateManager_OnWin(object sender, System.EventArgs e)
    {
        winScreen.SetActive(true);
    }

    private void GameStateManager_OnKeyObtain(object sender, GameStateManager.OnKeyObtainEventArgs e)
    {
        keyText.text = e.keyObtained.ToString();
    }
}
