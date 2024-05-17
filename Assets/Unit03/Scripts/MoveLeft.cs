using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit03
{
    public class MoveLeft : MonoBehaviour
    {
        [SerializeField] private float m_speed = 30.0f;



        void Update()
        {
            if (GameManager.Instance.IsGameOver)
                return;

            transform.Translate(Vector3.left * Time.deltaTime * m_speed);


            if(transform.position.x < -10 && gameObject.CompareTag("Obstacle"))
            {
                Destroy(gameObject);
            }


        }
    }
}