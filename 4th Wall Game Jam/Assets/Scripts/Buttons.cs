using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using FWGJ.Mechanics;
using FWGJ.Player;

public class Buttons : MonoBehaviour
{
    public GameObject creditsPanel;
    public GameObject howToPlayPanel;
    public GameObject moveToMazePanel;
    public GameObject escPanel;
    public bool isPaused;


    public void Update()
    {
        PauseButton();
    }


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
        bool isActive = moveToMazePanel.activeSelf;
        moveToMazePanel.SetActive(!isActive);
    }

    public void MazeButton()
    {
        GameManager.FindObjectOfType<GameManager>().MoveToMaze();   
    }

    public void PauseButton()
    {
 if(Input.GetKeyDown(KeyCode.Escape))
        {
           if(escPanel != null && PlayerController.FindObjectOfType<PlayerController>().mazePanelActive == false)
            {
                bool isActive = escPanel.activeSelf;
                escPanel.SetActive(!isActive);
            }

           if(escPanel.activeSelf)
            {   
               
                isPaused = true;  
                
            }
           else
            {
                isPaused = false;
            }

        }
    }

    
}
