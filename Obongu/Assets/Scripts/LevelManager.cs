using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    // configure params
    [SerializeField] private float loadSpeed = 3f;

    // reference params
    [SerializeField] private Image progressBar;
    [SerializeField] private GameObject loaderCanvas;
    public static event EventHandler OnSceneChange;

    private bool isLoading = false;
    private float target;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }
    
    public void LoadNextScene()
    {
        OnSceneChange?.Invoke(this, EventArgs.Empty);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadSpecificScene(int buildIndex)
    {
        OnSceneChange?.Invoke(this, EventArgs.Empty);
        SceneManager.LoadScene(buildIndex);
    }
    public void LoadMenuScene()
    {
        OnSceneChange?.Invoke(this, EventArgs.Empty);
        SceneManager.LoadScene("Menu");
    }
    public void LoadNextSceneAsync()
    {
        OnSceneChange?.Invoke(this, EventArgs.Empty);
        var scene = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        AsyncSceneLoadFunc(scene);
    }
    public void LoadMenuSceneAsync()
    {
        OnSceneChange?.Invoke(this, EventArgs.Empty);
        var scene = SceneManager.LoadSceneAsync("Menu");
        AsyncSceneLoadFunc(scene);
    }
    public void LoadSpecificSceneAsync(int buildIndex)
    {
        OnSceneChange?.Invoke(this, EventArgs.Empty);
        var scene = SceneManager.LoadSceneAsync(buildIndex);
        AsyncSceneLoadFunc(scene);
    }
    public void QuitGame()
    {
        OnSceneChange?.Invoke(this, EventArgs.Empty);
        Application.Quit();
    }

    private void AsyncSceneLoadFunc(AsyncOperation scene)
    {
        isLoading = true;
        scene.allowSceneActivation = false;
        loaderCanvas.SetActive(true);
        do
        {
            target = scene.progress;
        } while (scene.progress < 0.9f);
        scene.allowSceneActivation = true;
        loaderCanvas.SetActive(false);
        isLoading = false;
    }

    private void Update()
    {
        float fillAmount = Mathf.MoveTowards(progressBar.transform.localScale.x, target, loadSpeed * Time.deltaTime);
        progressBar.transform.localScale = new Vector3(fillAmount, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
    }
}
