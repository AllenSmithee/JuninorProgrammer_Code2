using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Unit04
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float m_moveSpeed = 3.0f;
        [SerializeField] private Rigidbody m_enemyRb;
        [SerializeField] private GameObject m_player;



        // Start is called before the first frame update
        void Start()
        {
            m_player = GameObject.Find("Player");

        }

        // Update is called once per frame
        void Update()
        {


            MoveEnemy();
            DestoryWhenOutOfBounds();
        }

        Vector3 DirectionToPlayer()
        {
            return (m_player.transform.position - transform.position).normalized;
        }

        void MoveEnemy()
        {
            m_enemyRb.AddForce(DirectionToPlayer() * m_moveSpeed);
        }

        void DestoryWhenOutOfBounds()
        {
            if (transform.position.y < -10)
            {
                SpawnManager.Instance.EnemiesList.Remove(this);
                Destroy(gameObject);
            }
        }

    }
}