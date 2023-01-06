using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FWGJ.Player;

namespace FWGJ.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        public EnemyStats stats;
        public Transform scarePos;
        public GameObject player;
        public GameObject[] scareZone;
        private bool scareZoneFound;
        

        public Vector3 playerOffset;
        public Vector3 startingPos;


        public LayerMask playerLayer;
      
        

        
        public bool isScaring;
        public bool inRange;

        public void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            scarePos = null;
            
            startingPos = transform.position;
        }

        public void Update()
        {
            playerOffset = new Vector3(player.transform.position.x,0,0);
            DetectPlayer();
            ScarePlayer();
            scarePos = DetectScareZone();
            
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, stats.range);
            Gizmos.color = Color.red;
        }


        public void ScarePlayer()
        {
            
            if(FWGJ.Player.PlayerController.FindObjectOfType<PlayerController>().isScared && inRange)
            {
                StartCoroutine(ScareTime());   
            }

            

        }
        
        private void DetectPlayer()
        {
           inRange = Physics.CheckSphere(transform.position, stats.range, playerLayer);
            
            
        }

        private Transform DetectScareZone()
        {
            scareZone = GameObject.FindGameObjectsWithTag("Scare Point");

            float closestDist = Mathf.Infinity;
            Transform trans = null;

            foreach (GameObject zone in scareZone)
            {
                float currentDist;
                currentDist = Vector3.Distance(transform.position, zone.transform.position);
                if (currentDist < closestDist)
                {
                    closestDist = currentDist;
                    trans = zone.transform;
                }


            }
            return trans;
        }
       

        IEnumerator ScareTime()
        {
            //move forward a bit then back to starting position
            transform.LookAt(player.transform);
            transform.position = scarePos.transform.position;
            isScaring = true;
            yield return new WaitForSeconds(1f);
            transform.position = startingPos;
            isScaring = false;
            
                        
        }

        



    }


}

