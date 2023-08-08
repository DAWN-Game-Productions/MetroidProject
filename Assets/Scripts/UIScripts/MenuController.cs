using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // first level to be loaded when new game is pressed
    public string _newGameLevel;
    private string sampleLevel;

    public void StartGame()
    {
        SceneManager.LoadScene(_newGameLevel);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
