using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomPauseMenu : MonoBehaviour
{
    public bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    private GameObject restartLevel;
    //public GameObject UICanvas;
    //public AudioSource backgroundAudio;

    private void Start()
    {
        restartLevel = transform.Find("RestartLevel").gameObject;
    }

    private void Update()
    {
        //if (Input.GetButtonDown("Cancel"))
        //{
        //    if (gameIsPaused) Resume();
        //    else Pause();
        //}
    }

    public void ToggleMenu()
    {
        if (gameIsPaused) Resume();
        else Pause();
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        //UICanvas.SetActive(false);
        Time.timeScale = 0f;
        gameIsPaused = true;

        //backgroundAudio.Pause();
    }

    public void Resume()
    {
        if (optionsMenuUI.activeSelf) optionsMenuUI.SetActive(false);
        if (restartLevel.activeSelf) restartLevel.SetActive(false);

        pauseMenuUI.SetActive(false);
        //UICanvas.SetActive(true);
        Time.timeScale = 1f;
        gameIsPaused = false;

        //backgroundAudio.Play();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void LoadMainLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //public void LoadTutorial()
    //{
    //    SceneManager.LoadScene(2);
    //}
}
