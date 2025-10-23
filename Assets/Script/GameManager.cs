using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    public Datacontainer datacontainer;
    public GameObject failpanel;
    public GameObject winPanel;
    public GameObject pausePanel;

    bool isGamePause = false;
    private void Awake()
    {
        _instance = this;
        Time.timeScale = 1.0f;
    }

    public void Gameover()
    {
        Time.timeScale = 0f;
        failpanel.SetActive(true);
    }
    public void WinStage()
    {
        Time.timeScale = 0f;
        winPanel.SetActive(true);
    }

    public void ClearLevel(int t)
    {
        datacontainer.levelclear[t] = true;
    }

    public void PauseGame()
    {
        isGamePause=!isGamePause;
        pausePanel.SetActive(isGamePause);

        if (isGamePause) { Time.timeScale = 0f; }
        else { Time.timeScale = 1f; }
    }

    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
    public void ReloadScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);

    }

    public void ExitGame() { Application.Quit(); }
}
