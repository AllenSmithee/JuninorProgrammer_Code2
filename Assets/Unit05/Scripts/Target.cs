using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit05
{
    public class Target : MonoBehaviour
    {
        private Rigidbody m_targetRb;

        [SerializeField] private float m_minSpeed = 12.0f;
        [SerializeField] private float m_maxSpeed = 16.0f;
        [SerializeField] private float m_maxTorque = 10.0f;
        [SerializeField] private float m_xRange = 4.0f;
        [SerializeField] private float m_ySpawnPos = -3.0f;
        [SerializeField] private int m_pointValue = 1;
        [SerializeField] private ParticleSystem m_explosionParticle;

        // Start is called before the first frame update
        void Start()
        {
            m_targetRb = GetComponent<Rigidbody>();

            m_targetRb.AddForce(RandomForce(), ForceMode.Impulse);
            m_targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

            transform.position = RandomSpawnPos();
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnMouseDown()
        {
            if (ScoreManager.Instance.IsGameOver)
            {
                return;
            }
            ScoreManager.Instance.UpdateScore(m_pointValue);

            if(gameObject.CompareTag("Bad"))
            {
                ScoreManager.Instance.UpdateHealth(-1);
            }

            Instantiate(m_explosionParticle, transform.position, m_explosionParticle.transform.rotation);
            Destroy(gameObject);

        }

        private void OnTriggerEnter(Collider other)
        {
            Destroy(gameObject);
            if (ScoreManager.Instance.IsGameOver)
            {
                return;
            }
            if (gameObject.CompareTag("Good"))
            {
                ScoreManager.Instance.UpdateScore(-5);
                ScoreManager.Instance.UpdateHealth(-1);
            }

        }
        Vector3 RandomForce()
        {
            return Vector3.up * Random.Range(m_minSpeed, m_maxSpeed);
        }
        float RandomTorque()
        {
            return Random.Range(-m_maxTorque, m_maxTorque);
        }
        Vector3 RandomSpawnPos()
        {
            return new Vector3(Random.Range(-m_xRange, m_xRange), m_ySpawnPos);
        }
    }
}