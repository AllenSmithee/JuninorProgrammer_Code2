using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Unit03
{
    public class RepeatBackground : MonoBehaviour
    {
        private Vector3 m_startPos;

        private float m_repeatWidth;

        // Start is called before the first frame update
        void Start()
        {
            m_startPos = transform.position;
            m_repeatWidth = GetComponent<BoxCollider>().size.x * 0.5f;
        }

        // Update is called once per frame
        void Update()
        {
            if (transform.position.x < m_startPos.x - m_repeatWidth)
            {
                transform.position = m_startPos;
            }
        }
    }
}