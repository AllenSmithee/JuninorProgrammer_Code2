using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Unit04
{
    public class RotateCamera : MonoBehaviour
    {
        [SerializeField] private float m_rotateSpeed;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            ControlCamera();
        }

        void ControlCamera()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up, horizontalInput * m_rotateSpeed * Time.deltaTime);
        }


    }
}