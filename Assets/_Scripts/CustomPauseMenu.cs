using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomPauseMenu : MonoBehaviour
{
    public bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    private GameObject restartLevel;
    private GameObject endLevel;

    private void Start()
    {
        restartLevel = transform.Find("RestartLevel").gameObject;
        endLevel = transform.Find("EndLevel").gameObject;
    }

    public void ToggleMenu()
    {
        if (restartLevel.activeSelf) restartLevel.SetActive(false);

        if (gameIsPaused) Resume();
        else Pause();
    }

    public void ShowEndLevel()
    {
        endLevel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Pause()
    {
        if (endLevel.activeSelf) return;

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Resume()
    {
        if (optionsMenuUI.activeSelf) optionsMenuUI.SetActive(false);        

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void RestartFromBeginning()
    {
        CheckpointManager checkpointManager = CheckpointManager.instance;
        if (checkpointManager != null) Destroy(checkpointManager.gameObject);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void QuitToMenu()
    {
        CheckpointManager checkpointManager = CheckpointManager.instance;
        if (checkpointManager != null) Destroy(checkpointManager.gameObject);

        SceneManager.LoadScene(0);
        Time.timeScale = 1f;        
    }

    public void LoadMainLevel()
    {
        CheckpointManager checkpointManager = CheckpointManager.instance;
        if (checkpointManager != null) Destroy(checkpointManager.gameObject);

        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void LoadNextLevel()
    {
        CheckpointManager checkpointManager = CheckpointManager.instance;
        if (checkpointManager != null) Destroy(checkpointManager.gameObject);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }

    public void ShowRestart()
    {
        if (pauseMenuUI.activeSelf) return;

        restartLevel.SetActive(true);
        Time.timeScale = 0f;
    }
}
