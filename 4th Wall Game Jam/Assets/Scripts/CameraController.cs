using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FWGJ.Mechanics
{
    public class CameraController : MonoBehaviour
    {
        public Transform targetTransform;
        public GameObject[] closestEnemy;
        public Transform foundEnemy;

        private Camera _Cam;
        public Camera Cam
        {
            get
            {
                if (_Cam == null)
                {
                    _Cam = GetComponent<Camera>();
                }
                return _Cam;
            }
        }
        public bool isMoving;
        public Vector3 CamOffset = Vector3.zero;

        public float senstivityX = 5;
        public float senstivityY = 1;
        public float minY = 30;
        public float maxY = 50;

        private float currentX = 0;
        private float currentY = 1;
        public LayerMask wallLayer;
        

        public void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            foundEnemy = null;
        }

        void Update()
        {
            foundEnemy = FindClosestEnemy();

            currentX += Input.GetAxis("Mouse X");
            currentY -= Input.GetAxis("Mouse Y");
            currentX = Mathf.Repeat(currentX, 360);
            currentY = Mathf.Clamp(currentY, minY, maxY);
            isMoving = (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) ? true : false;


        }

        void LateUpdate()
        {
            Vector3 dist = CamOffset;
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            transform.position = targetTransform.position + rotation * dist;
            transform.LookAt(targetTransform.position);
            CheckWall();
        }

        void CheckWall()
        {
            RaycastHit hit;
            Vector3 start = targetTransform.position;
            Vector3 dir = transform.position - targetTransform.position;
            float dist = CamOffset.z * -1;
            Debug.DrawRay(targetTransform.position, dir, Color.green);
            if (Physics.Raycast(targetTransform.position, dir, out hit, dist, wallLayer))
            {
                float hitDist = hit.distance;
                Vector3 sphereCastCenter = targetTransform.position + (dir.normalized * hitDist);
                transform.position = sphereCastCenter;
            }
        }

        void LookAtEnemy()
        {
            Vector3 offset = new Vector3(0, 0, 0);
            Vector3 camOffset = new Vector3(0, 0, 0);
            Vector3 CamPosition = transform.position + offset;

            transform.position = CamPosition;
            

        }

        private Transform FindClosestEnemy()
        {
            closestEnemy = GameObject.FindGameObjectsWithTag("Enemy");

            float closestDist = Mathf.Infinity;
            Transform trans = null;

            foreach(GameObject enemy in closestEnemy)
            {
                float currentDist;
                currentDist = Vector3.Distance(transform.position, enemy.transform.position);
                if(currentDist<closestDist)
                {
                    closestDist = currentDist;
                    trans = enemy.transform;
                }

                
            }
            return trans;
        }

    }
}

