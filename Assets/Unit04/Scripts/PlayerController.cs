using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit04
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody m_playerRb;




        [SerializeField] private float m_moveSpeed = 5.0f;



        [SerializeField] private Transform m_focalPoint;




        // Start is called before the first frame update
        void Start()
        {
            m_playerRb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            GetPlayerInput(out float verticalValue);

            MovePlayer(verticalValue);
        }

        // atm just vertical
        void GetPlayerInput(out float verticalValue)
        {
            verticalValue = Input.GetAxis("Vertical");
        }

        void MovePlayer(float inputValue)
        {
            //using the m_focalPoint's local position to move the player where the camera is facing
            m_playerRb.AddForce(m_focalPoint.forward * inputValue * m_moveSpeed);
        }

    }
}