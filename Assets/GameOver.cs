
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public static bool GameIsPaused;
    public GameObject QuitMenuUI;
    // Update is called once per frame
    private void Start()
    {
        QuitMenuUI.SetActive(false);
    }
    void Update()
    {

    }



    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
    public void TryAgain()
    {
        Time.timeScale = 1f;
        PlayerPrefs.DeleteKey("chooseCharacter");
        SceneManager.LoadScene("MainScene");
    }

}
