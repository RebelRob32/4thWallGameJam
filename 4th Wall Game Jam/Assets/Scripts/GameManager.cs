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
        public GameObject player;
        public GameObject winPanel;
        public TMP_Text chargeText;
        public Slider fearSlider;
        
        public bool playerScared;

        public void Awake()
        {
            player = GameObject.FindWithTag("Player");
        }

        public void Update()
        {
            CheckFear();
            chargeText.text = player.gameObject.GetComponent<PlayerController>().itemUses.ToString();
            
        }

        public void CheckFear()
        {
            if(player.GetComponent<PlayerController>().fearLvl >= 99f)
            {
                Debug.Log("Too Scared");
                LoseGame();
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
            //dynamic camera, load menu panel to restart or return to main menu. 
        }

        IEnumerator WinConditionsMet()
        {
            winPanel.SetActive(true);
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene("Main Menu");
        }
    }
}

