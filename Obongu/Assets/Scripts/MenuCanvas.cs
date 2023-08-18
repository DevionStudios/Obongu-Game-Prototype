using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvas : MonoBehaviour
{
    public void LoadNextLevel()
    {
        LevelManager.instance.LoadNextSceneAsync();
    }

    public void QuitGame()
    {
        LevelManager.instance.QuitGame();
    }
}
