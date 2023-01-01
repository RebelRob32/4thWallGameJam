using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FWGJ.Mechanics
{
    public class CameraController : MonoBehaviour
    {
        public Transform targetTransform;
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
        public Vector3 ZoomOffset = Vector3.zero;
        public float senstivityX = 5;
        public float senstivityY = 1;
        public float minY = 30;
        public float maxY = 50;
        public bool isZooming;
        private float currentX = 0;
        private float currentY = 1;

        public void Start()
        {
           Cursor.lockState =  CursorLockMode.Locked;
        }

        void Update()
        {
            currentX += Input.GetAxis("Mouse X");
            currentY -= Input.GetAxis("Mouse Y");
            currentX = Mathf.Repeat(currentX, 360);
            currentY = Mathf.Clamp(currentY, minY, maxY);
            isMoving = (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) ? true : false;
            isZooming = Input.GetMouseButton(1);

        }
        void LateUpdate()
        {
            Vector3 dist = isZooming ? ZoomOffset : CamOffset;
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            transform.position = targetTransform.position + rotation * dist;
            transform.LookAt(targetTransform.position);
            CheckWall();
        }
        public LayerMask wallLayer;
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
    }

}

