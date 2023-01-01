using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FWGJ.Player;

namespace FWGJ.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        public EnemyStats stats;
        public Transform playerTransform;
        public GameObject player;

        public LayerMask playerLayer;

        public bool playerInRange;
        public bool isScaring;

        public void Start()
        {
            playerTransform = GameObject.Find("Player").transform;
            player = GameObject.Find("Player");
        }

        public void Update()
        {
            DetectPlayer();
            ScarePlayer();
        }

        public void DetectPlayer()
        {
            playerInRange = Physics.CheckSphere(transform.position, stats.range, playerLayer);
        }

        public void ScarePlayer()
        {
            
            if(playerInRange)
            {
                StartCoroutine(ScareTimer());
                
                IEnumerator ScareTimer()
                {
                    Debug.Log("Boo!");
                    isScaring = true;
                    //scare animation
                    yield return new WaitForSeconds(2);
                    isScaring = false;
                    
                }
                
                
            }
        }
    }
}

