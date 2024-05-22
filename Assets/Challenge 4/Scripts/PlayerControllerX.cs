using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Challenge4
{
    public class PlayerControllerX : MonoBehaviour
    {
        private Rigidbody playerRb;
        private float speed = 500;
        private GameObject focalPoint;

        public bool hasPowerup;
        public GameObject powerupIndicator;
        public int powerUpDuration = 5;
        [SerializeField] float m_dashMultiplier = 2.5f;

        [SerializeField] ParticleSystem m_dustParticle;
        private float normalStrength = 10; // how hard to hit enemy without powerup
        private float powerupStrength = 25; // how hard to hit enemy with powerup
        private bool m_dashFlag = false;
        private float verticalInput;
        private bool isCooldown = false;
        [SerializeField] private float m_cooldownTime = 1.0f;
        void Start()
        {
            playerRb = GetComponent<Rigidbody>();
            focalPoint = GameObject.Find("Focal Point");
        }

        void Update()
        {
            // Set powerup indicator position to beneath player
            powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);

            // Add force to player in direction of the focal point (and camera)
            verticalInput = Input.GetAxis("Vertical");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isCooldown)
                {
                    return;
                }
                StartCoroutine(Cooldown());
                m_dashFlag = true;

                m_dustParticle.Play();
            }

        }

        void FixedUpdate()
        {

            playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime);
            if (m_dashFlag)
            {
                m_dashFlag = false;
                playerRb.AddForce(focalPoint.transform.forward * verticalInput * m_dashMultiplier * speed * Time.deltaTime);
            }

        }



        // If Player collides with powerup, activate powerup
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Powerup"))
            {
                Destroy(other.gameObject);
                hasPowerup = true;
                powerupIndicator.SetActive(true);
                StartCoroutine(PowerupCooldown());
            }
        }

        // Coroutine to count down powerup duration
        IEnumerator PowerupCooldown()
        {
            yield return new WaitForSeconds(powerUpDuration);
            hasPowerup = false;
            powerupIndicator.SetActive(false);
        }

        // If Player collides with enemy
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
                //Vector3 awayFromPlayer = transform.position - other.gameObject.transform.position;
                Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position;

                if (hasPowerup) // if have powerup hit enemy with powerup force
                {
                    enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
                }
                else // if no powerup, hit enemy with normal strength 
                {
                    enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
                }


            }
        }

        IEnumerator Cooldown()
        {
            isCooldown = true;
            yield return new WaitForSeconds(m_cooldownTime);
            isCooldown = false;
        }
    }
}
