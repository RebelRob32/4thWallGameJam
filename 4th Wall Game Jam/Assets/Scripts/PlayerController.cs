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
        private bool isGrounded;
        [SerializeField] private float groundCheckDist;
        
        public LayerMask groundLayer;

        private Vector3 moveDir;
        private Vector3 velocity;

        [Header("Gameplay")]

        public float fearLvl;
        public float maxFearLvl;
        public bool isScared;
        public float itemUses;
        public bool canUseItem;

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
            fearLvl += 0.5f * Time.deltaTime;
            
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
                }
                else if(Input.GetKey(KeyCode.LeftShift))
                {
                    stats.moveSpeed = stats.runSpeed;
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

            if(other.tag == ("Scare Point"))
            {
                
                StartCoroutine(ScareMoment());

                IEnumerator ScareMoment()
                {
                    groundCheckDist = -1f;
                    Debug.Log("Scare!");//this will activate audio or UI or something
                    fearLvl += 10f;
                    other.gameObject.SetActive(false);
                    isScared = true;
                    yield return new WaitForSeconds(2);
                    isScared = false;
                    groundCheckDist = 1.25f;
                    yield return new WaitForSeconds(30);
                    other.gameObject.SetActive(true);
                }
                
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


    }

}

