using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tutoriales
{
    public class CharController : MonoBehaviour
    {

        public bool Player = true;

        Transform tr;
        Rigidbody rg;
        Animator anim;

        public Transform CameraShoulder;
        public Transform CameraHolder;
        private Transform cam;

        private float rotY = 0f;

        public float speed = 200;
        public float rotationSpeed = 25;
        public float jumpForce = 25;
        public float minAngle = -70;
        public float maxAngle = 90;
        public float cameraSpeed = 24;

        public bool OnGraund = false;
        public bool Jumping = false;

        private Vector2 moveDelta;
        private float deltaT;

        public InputController _Input;

        //Use this for initialization
        void Start()
        {

            tr = this.transform;
            rg = GetComponent<Rigidbody>();
            anim = GetComponentInChildren<Animator>();
            cam = Camera.main.transform;
        }

        //Update is called once per frame
        void FixedUpdate()
        {
            if (Player)
            {
                PlayerControl();
                MoveControl();
                CameraControl();
                AnimControl();
            }
            else
            {
                gameObject.name = "Player";
            }
        }

        private void PlayerControl()
        {
            _Input.Update();

            float deltaX = _Input.CheckF("Horizontal");
            float deltaZ = _Input.CheckF("Vertical");
            Jumping = _Input.Check("Jump");

            moveDelta = new Vector2(deltaX, deltaZ);

            deltaT = Time.deltaTime;
        }


        private void MoveControl()
        {
            RaycastHit hit;
            OnGraund = Physics.Raycast(this.tr.position, -tr.up, out hit, .2f);
            if (OnGraund)
            {
                if (Jumping)
                {
                    rg.AddForce(tr.up * jumpForce);
                }
            }

            Vector3 sp = rg.velocity;

            Vector3 side = speed * moveDelta.x * deltaT * tr.right;
            Vector3 forward = speed * moveDelta.y * deltaT * tr.forward;


            Vector3 endSpeed = side + forward;

            endSpeed.y = sp.y;

            rg.velocity = endSpeed;
        }

        private void CameraControl()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            float deltaT = Time.deltaTime;

            float xrot = mouseX * deltaT * rotationSpeed;

            tr.Rotate(0, xrot, 0);

            rotY += mouseY * deltaT * rotationSpeed;
            rotY = Mathf.Clamp(rotY, minAngle, maxAngle);


            Quaternion localRotation = Quaternion.Euler(-rotY, 0, 0);
            CameraShoulder.localRotation = localRotation;

            cam.position = Vector3.Lerp(cam.position, CameraHolder.position, cameraSpeed * deltaT);
            cam.rotation = Quaternion.Lerp(cam.rotation, CameraHolder.rotation, cameraSpeed * deltaT);

        }

        private void AnimControl()
        {
            anim.SetFloat("X", moveDelta.x);
            anim.SetFloat("Y", moveDelta.y);
        }
    }
}
