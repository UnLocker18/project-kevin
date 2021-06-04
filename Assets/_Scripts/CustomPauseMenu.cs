using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomPauseMenu : MonoBehaviour
{
    public bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    public GameObject UICanvas;
    public AudioSource backgroundAudio;

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetButton("Cancel"))
        {
            if (gameIsPaused) Resume();
            else Pause();
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        UICanvas.SetActive(false);
        Time.timeScale = 0f;
        gameIsPaused = true;

        backgroundAudio.Pause();
    }

    public void Resume()
    {
        if (optionsMenuUI.activeSelf) optionsMenuUI.SetActive(false);

        pauseMenuUI.SetActive(false);
        UICanvas.SetActive(true);
        Time.timeScale = 1f;
        gameIsPaused = false;

        backgroundAudio.Play();
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadMainLevel()
    {
        SceneManager.LoadScene(1);
    }

    //public void LoadTutorial()
    //{
    //    SceneManager.LoadScene(2);
    //}
}
