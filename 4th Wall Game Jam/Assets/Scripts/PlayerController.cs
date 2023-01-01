using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FWGJ.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        public CharacterController controller;
        public Transform camTransform;
        public PlayerStats stats;

        private bool isGrounded;
        [SerializeField] private float groundCheckDist;
        public LayerMask groundLayer;

        private Vector3 moveDir;
        private Vector3 velocity;

        public void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        public void Update()
        {
            MovePlayer();
        }


        public void MovePlayer()
        {
            isGrounded = Physics.CheckSphere(transform.position, groundCheckDist, groundLayer);

            if(isGrounded && velocity.y<0)
            {
                velocity.y = -2f;
            }

            float z = Input.GetAxis("Vertical");
            float x = Input.GetAxis("Horizontal");
            moveDir = new Vector3(x, 0, z);
            moveDir = transform.TransformDirection(moveDir);

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
               

            }
                moveDir *= stats.moveSpeed;
                controller.Move(moveDir * Time.deltaTime);
                velocity.y += stats.gravity * Time.deltaTime;
                controller.Move(velocity * Time.deltaTime);


        }

    }

}

