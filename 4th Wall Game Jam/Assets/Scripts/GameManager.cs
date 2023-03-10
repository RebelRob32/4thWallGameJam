using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using FWGJ.Player;

namespace FWGJ.Mechanics
{
public class GameManager : MonoBehaviour
{
        [Header("Game Objects")]
        public GameObject player;

        [Header("UI Elements")]
        public GameObject winPanel;
        public GameObject losePanel;
        public TMP_Text chargeText;
        public Slider fearSlider;
        
        [Header("Bools")]
        public bool playerScared;
        public bool inMenu;

        public void Awake()
        {
            player = GameObject.FindWithTag("Player");
            if(player == null)
            {
                return;
            }
        }

        public void Update()
        {
            if(Buttons.FindObjectOfType<Buttons>().isPaused)
            {
                
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
            }
            else
            {
                
                //Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
            }

            GetCurrentSceneName();
            CheckFear();
            if(chargeText==null)
            {
                return;
            }
            else
            {
                chargeText.text = player.gameObject.GetComponent<PlayerController>().itemUses.ToString();
            }

           
            
            
        }

        public void CheckFear()
        {
            if(player == null)
            {
                return;
            }

            if(player.GetComponent<PlayerController>().fearLvl >= 99)
            {
                LoseGame();

            }

           if(player.GetComponent<PlayerController>().fearLvl>=25f && player.GetComponent<PlayerController>().fearLvl <= 25.002f)
            {
                Debug.Log("Whoa this is spooky!");
            } 
            
            if(player.GetComponent<PlayerController>().fearLvl>=35f && player.GetComponent<PlayerController>().fearLvl <= 35.002f)
            {
                Debug.Log("This is getting super spooky!");
            }
            
            if(player.GetComponent<PlayerController>().fearLvl>=50f && player.GetComponent<PlayerController>().fearLvl <= 50.002f)
            {
                Debug.Log("Hold on! What are you making me do? This is TOO spooky!");
            }
            
            if(player.GetComponent<PlayerController>().fearLvl>=75f && player.GetComponent<PlayerController>().fearLvl <= 75.002f)
            {
                Debug.Log("I don't want to do this anymore, just let me go to the ferris wheel this is TOO SPOOKY!");
            }
          

            fearSlider.maxValue = player.gameObject.GetComponent<PlayerController>().maxFearLvl;
            fearSlider.value = player.gameObject.GetComponent<PlayerController>().fearLvl;
        }

        public void WinGame()
        {
            Debug.Log("Win!");
            StartCoroutine(WinConditionsMet());
        }

        public void LoseGame()
        {
            losePanel.SetActive(true);
        }

        IEnumerator WinConditionsMet()
        {
            winPanel.SetActive(true);
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene("Main Menu");
        }

        public void MoveToMaze()
        {
            SceneManager.LoadScene("WorkShopScene"); 
        }

        public void GetCurrentSceneName()
        {
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "Main Menu" || Buttons.FindObjectOfType<Buttons>().isPaused)
            {
                Cursor.lockState = CursorLockMode.None;
                inMenu = true;
            }
            else
            if (PlayerController.FindObjectOfType<PlayerController>().mazePanelActive == false && Buttons.FindObjectOfType<Buttons>().isPaused == false)
            {
                Cursor.lockState = CursorLockMode.Locked;
                inMenu = false;
            }
            if(scene.name == "WorkshopScene")
            {
                player.GetComponent<PlayerController>().FearLevelIncrease();
                inMenu = false;
            }
        }









    }
}

