using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("EndlessRunning");
    }

    public void ExitGame()
    {
        Debug.Log("Game Close");
        Application.Quit();
    }
}
