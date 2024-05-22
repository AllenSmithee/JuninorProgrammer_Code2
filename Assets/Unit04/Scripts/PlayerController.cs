using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Unit04
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody m_playerRb;




        [SerializeField] private float m_moveSpeed = 5.0f;
        [SerializeField] private bool m_hasPowerUp = false;
        [SerializeField] private GameObject m_powerUpIndicator;
        [SerializeField] private float m_powerUpStrength = 15.0f;
        [SerializeField] private float m_powerUpDuration = 5.0f;
        [SerializeField] private Transform m_focalPoint;



        // Start is called before the first frame update
        void Start()
        {
            m_playerRb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            if (m_hasPowerUp)
            {
                m_powerUpIndicator.SetActive(true);
                m_powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
            }
            else
            {
                m_powerUpIndicator.SetActive(false);
            }




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


        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PowerUp"))
            {
                m_hasPowerUp = true;
                Destroy(other.gameObject);
                StartCoroutine(PowerUpCountdownRoutine());
            }
        }

        IEnumerator PowerUpCountdownRoutine()
        {
            yield return new WaitForSeconds(m_powerUpDuration);
            m_hasPowerUp = false;
        }



        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy") && m_hasPowerUp)
            {
                Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

                enemyRigidbody.AddForce(awayFromPlayer * m_powerUpStrength, ForceMode.Impulse);
            }
        }
    }
}