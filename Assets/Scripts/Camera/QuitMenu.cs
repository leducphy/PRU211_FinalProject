using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitMenu : MonoBehaviour
{
    public GameObject QuitMenuUI;
    // Update is called once per frame
    private void Start()
    {
        QuitMenuUI.SetActive(false);
    }
    void Update()

    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitMenuUI.SetActive(false);
        }
    }

 
    public void Quit()
    {
        QuitMenuUI.SetActive(true);
    }

    public void NoToQuit()
    {
        QuitMenuUI.SetActive(false);
    }
    public void YesToQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif

    }
}

