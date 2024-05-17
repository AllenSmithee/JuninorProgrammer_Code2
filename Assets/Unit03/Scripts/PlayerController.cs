using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit03
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody m_rb;

        [SerializeField] private float m_jumpForce = 10.0f;
        [SerializeField] private float m_gravityModifier = 2.0f;

        [SerializeField] private bool m_isOnGround = true;
        private Vector3 m_orginGravity;

        internal event System.Action OnGameOver;

        // Start is called before the first frame update
        void Start()
        {
            m_rb = GetComponent<Rigidbody>();
            m_orginGravity = Physics.gravity;
            Physics.gravity *= m_gravityModifier;

        }

        void OnDisable()
        {
            Physics.gravity = m_orginGravity;
        }

        // Update is called once per frame
        void Update()
        {

            if (GameManager.Instance.IsGameOver)
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (m_isOnGround)
                {
                    Jump();
                    m_isOnGround = false;
                }
                else
                {
                    Debug.Log("Can't jump while in the air");

                }
            }
        }

        void Jump()
        {
            m_rb.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
        }

        void OnCollisionEnter(Collision collision)
        {





            if (collision.gameObject.CompareTag("Ground"))
            {
                m_isOnGround = true;
            }
            else if (collision.gameObject.CompareTag("Obstacle"))
            {
                Debug.Log("Game Over!");
                GameManager.Instance.IsGameOver = true;
            }
        }
    }
}