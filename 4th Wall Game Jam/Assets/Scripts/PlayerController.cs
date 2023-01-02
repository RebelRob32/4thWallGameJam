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
        public bool isScared;

        public void Awake()
        {
            controller = GetComponent<CharacterController>();
            fearLvl = stats.fearLevel;
        }

        public void Update()
        {
            MovePlayer();
        }

        //deals with player movement
        #region Movement
        public void MovePlayer()
        {
            isGrounded = Physics.CheckSphere(transform.position, groundCheckDist, groundLayer);

            if(isGrounded && velocity.y<0)
            {
                velocity.y = -2f;
            }

            float z = Input.GetAxis("Vertical");
            float x = Input.GetAxis("Horizontal");
            Vector3 movementInput = Quaternion.Euler(0, camTransform.transform.eulerAngles.y, 0) * new Vector3(x, 0, z);
            moveDir = movementInput.normalized;

            if(isGrounded)
            {
                if(!Input.GetKey(KeyCode.LeftShift))
                {
                    stats.moveSpeed = stats.walkSpeed;
                }
                else if(Input.GetKey(KeyCode.LeftShift))
                {
                    stats.moveSpeed = stats.runSpeed;
                }

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
                    yield return new WaitForSeconds(9);
                    other.gameObject.SetActive(true);
                }
                
            }

          

        }

       



    }

}

