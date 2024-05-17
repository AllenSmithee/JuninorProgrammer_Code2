using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit03
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody m_rb;

        [SerializeField] private float m_foce = 10.0f;

        // Start is called before the first frame update
        void Start()
        {
            m_rb = GetComponent<Rigidbody>();

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_rb.AddForce(Vector3.up * m_foce, ForceMode.Impulse);
            }
        }
    }
}