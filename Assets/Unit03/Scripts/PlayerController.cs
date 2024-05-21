using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace Unit03
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody m_rb;
        private Animator m_playerAnimator;

        [SerializeField] private ParticleSystem m_explosionParticle;
        [SerializeField] private ParticleSystem m_dirtParticle;
        [SerializeField] private AudioSource m_playerAudioSource;

        [SerializeField] private float m_jumpForce = 10.0f;
        [SerializeField] private float m_gravityModifier = 2.0f;

        [SerializeField] private bool m_isOnGround = true;
        private Vector3 m_orginGravity;


        public bool IsOnGround
        {
            get { return m_isOnGround; }
            set
            {
                if (m_isOnGround != value)
                {
                    m_isOnGround = value;
                    if (value)
                    {
                        m_dirtParticle.Play();
                    }
                    else
                    {
                        m_dirtParticle.Stop();
                    }
                }
            }
        }

        internal event System.Action OnGameOver;

        // Start is called before the first frame update
        void Start()
        {
            m_rb = GetComponent<Rigidbody>();
            m_playerAnimator = GetComponent<Animator>();

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
                m_dirtParticle.Stop();
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (m_isOnGround)
                {
                    IsOnGround = false;
                    m_playerAudioSource.PlayOneShot(AudioManager.Instance.JumpSound);
                    Jump();
                }
                else
                {
                    Debug.Log("Can't jump while in the air");

                }
            }
        }

        void Jump()
        {
            m_playerAnimator.SetTrigger("Jump_trig");
            m_rb.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
        }

        void OnCollisionEnter(Collision collision)
        {

            if (collision.gameObject.CompareTag("Ground"))
            {
                IsOnGround = true;
            }
            else if (collision.gameObject.CompareTag("Obstacle"))
            {
                Debug.Log("Game Over!");
                GameManager.Instance.IsGameOver = true;

                m_playerAnimator.SetBool("Death_b", true);
                m_playerAnimator.SetInteger("DeathType_int", 1);

                m_playerAudioSource.PlayOneShot(AudioManager.Instance.CrashSound, 1.0f);
                m_explosionParticle.Play();


            }
        }
    }
}