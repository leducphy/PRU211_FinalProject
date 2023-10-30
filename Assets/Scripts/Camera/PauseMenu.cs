
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused;
    public GameObject pauseMenuUI;
    public GameObject QuitMenuUI;
    // Update is called once per frame
    private void Start()
    {
        pauseMenuUI.SetActive(false);
        QuitMenuUI.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        Debug.Log("Resume");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Quit()
    {
        QuitMenuUI.SetActive(true );
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        PlayerPrefs.DeleteKey("chooseCharacter");
        SceneManager.LoadScene("MainScene");
    }
    
}
