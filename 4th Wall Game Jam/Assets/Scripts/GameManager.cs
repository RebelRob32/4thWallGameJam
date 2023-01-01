using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FWGJ.Player;

namespace FWGJ.Mechanics
{
public class GameManager : MonoBehaviour
{
        public GameObject player;
        public bool playerScared;

        public void Awake()
        {
            player = GameObject.FindWithTag("Player");
        }

        public void Update()
        {
            CheckFear();
        }

        public void CheckFear()
        {
            if(player.GetComponent<PlayerController>().fearLvl >= 99f)
            {
                Debug.Log("Too Scared");
            }
        }
    }
}

