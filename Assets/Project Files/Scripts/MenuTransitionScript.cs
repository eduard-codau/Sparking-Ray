using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuTransitionScript : MonoBehaviour
{

    public void StartPressed()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void HelpPressed()
    {
        SceneManager.LoadScene("Help");
    }

    public void QuitPressed()
    {
        Application.Quit();
    }

    public void BackPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
