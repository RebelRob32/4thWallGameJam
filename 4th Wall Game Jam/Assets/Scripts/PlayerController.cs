using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FWGJ.Mechanics;

namespace FWGJ.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]

        public CharacterController controller;
        public Transform camTransform;
        public PlayerStats stats;
        public bool isGrounded;
        public float groundCheckDist;
        
        public LayerMask groundLayer;

        private Vector3 moveDir;
        private Vector3 velocity;

        [Header("Gameplay")]

        public float fearLvl;
        public float runningFearLvl;
        public float maxFearLvl;
        public float itemUses;

        public bool isScared;
        public bool canUseItem;
        public bool isRunning;
        public bool mazePanelActive;

        public void Awake()
        {
            
            controller = GetComponent<CharacterController>();
            fearLvl = stats.fearLevel;
            itemUses = stats.itemCharges;
            maxFearLvl = 100f;
            
           
        }

        public void Update()
        {
            ItemUse();
            MovePlayer();
            
            
            
        }

        //deals with player movement
        #region Movement
        public void MovePlayer()
        {
            //checks player distance to ground

            isGrounded = Physics.CheckSphere(transform.position, groundCheckDist, groundLayer);

            if(isGrounded && velocity.y<0)
            {
                velocity.y = -2f;
            }

            float z = Input.GetAxis("Vertical");
            float x = Input.GetAxis("Horizontal");
            Vector3 movementInput = Quaternion.Euler(0, camTransform.transform.eulerAngles.y, 0) * new Vector3(x, 0, z);
            moveDir = movementInput.normalized;
            
            //tells game what player can do when grounded

            if(isGrounded)
            {
                //run with Left Shift

                if(!Input.GetKey(KeyCode.LeftShift))
                {
                    stats.moveSpeed = stats.walkSpeed;
                    isRunning = false;
                    
                }
                else if(Input.GetKey(KeyCode.LeftShift))
                {
                    stats.moveSpeed = stats.runSpeed;
                    isRunning = true;
                    
                }

                //jump with Spacebar

                if(Input.GetKeyDown(KeyCode.Space))
                {
                    velocity.y = Mathf.Sqrt(stats.jumpHeight * -2 * stats.gravity);
                }

                if (moveDir != Vector3.zero)
                {
                    Quaternion desiredRotation = Quaternion.LookRotation(moveDir, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, stats.rotationSpeed * Time.deltaTime);
                }
            }
            else
            {
                stats.moveSpeed = 0;
            }

                moveDir *= stats.moveSpeed;
                controller.Move(moveDir * Time.deltaTime);
                velocity.y += stats.gravity * Time.deltaTime;
                controller.Move(velocity * Time.deltaTime);
        }
        #endregion


        //Game events when collider is triggered

        #region TriggerFunctions
        private void OnTriggerEnter(Collider other)
        {
            if(other.name==("End Point"))
            {
               
                GameManager.FindObjectOfType<GameManager>().WinGame();
            }

            if(other.tag == ("Scare Trigger"))
            {
                
                StartCoroutine(ScareMoment());

                IEnumerator ScareMoment()
                {
                    groundCheckDist = -1f;
                    Debug.Log("Scare!");//this will activate audio or UI or something
                    fearLvl += 10f;
                    isScared = true;
                    transform.LookAt(Mechanics.CameraController.FindObjectOfType<CameraController>().foundEnemy.transform.position);
                    yield return new WaitForSeconds(1);
                    isScared = false;
                    other.gameObject.SetActive(false);
                    groundCheckDist = 1.25f;
                    yield return new WaitForSeconds(30);
                    other.gameObject.SetActive(true);
                }
                
            }

            if(other.tag ==("Pickup"))
            {
                Destroy(other.gameObject);
                itemUses++;
            }

            if(other.tag == ("Teleport"))
            {
                Buttons.FindObjectOfType<Buttons>().MoveToMazePanel.SetActive(true);
                Cursor.lockState = CursorLockMode.Confined;
                mazePanelActive = true;
            }

        }

        private void OnTriggerExit(Collider other)
        {
            if(other.tag == ("Teleport"))
            {
                Buttons.FindObjectOfType<Buttons>().MoveToMazePanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                mazePanelActive = false;
            }
        }
        #endregion

        //player buttons and what they do

        #region PlayerInput

        public void ItemUse()
        {

            if(Input.GetKeyDown(KeyCode.E) && canUseItem)
            {
                if(itemUses <= 0)
                {
                   canUseItem = false;
                }
                else
                {
                    
                     itemUses--;
                    fearLvl -= 15f;
                }
            }
            else if(Input.GetKeyDown(KeyCode.E) && !canUseItem)
            {
                Debug.Log("No Charges Left");
            }
        }

        #endregion

        public void FearLevelIncrease()
        {
            if(!isRunning)
            {
                fearLvl += 0.5f * Time.deltaTime;
            }
            else
            {
                fearLvl += 2f * Time.deltaTime;
            }
        }

        

    }

}

