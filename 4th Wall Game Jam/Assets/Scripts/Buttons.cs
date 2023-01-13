using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using FWGJ.Mechanics;

public class Buttons : MonoBehaviour
{
    public GameObject creditsPanel;
    public GameObject howToPlayPanel;
    public GameObject MoveToMazePanel;
  

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Credits()
    {
        bool isActive = creditsPanel.activeSelf;
        creditsPanel.SetActive(!isActive);
    }
    public void HowToPlay()
    {
        bool isActive = howToPlayPanel.activeSelf;
        howToPlayPanel.SetActive(!isActive);
    }

    public void Carnival()
    {
        SceneManager.LoadScene("Carnival");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MazePanel()
    {
        bool isActive = MoveToMazePanel.activeSelf;
        MoveToMazePanel.SetActive(!isActive);
    }

    public void MazeButton()
    {
        GameManager.FindObjectOfType<GameManager>().MoveToMaze();   
    }
}
