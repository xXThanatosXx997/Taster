using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void PlayGame(int no)
    {
        SceneManager.LoadScene(no);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
