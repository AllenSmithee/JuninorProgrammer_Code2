using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit05
{
    using TMPro;
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;
        [SerializeField] private TextMeshProUGUI m_scoreText;
        [SerializeField] private string m_scoreTextFormat = "Score: {0}";
        [SerializeField] private int m_score = 0;

        [SerializeField] private TextMeshProUGUI m_healthText;
        [SerializeField] private string m_healthTextFormat = "Health: {0}";
        [SerializeField] private int m_health = 5;

        public bool IsGameOver { get => m_health <= 0; }
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            m_healthText.text = string.Format(m_healthTextFormat, m_health);
            m_scoreText.text = string.Format(m_scoreTextFormat, m_score);
        }


        internal void UpdateScore(int scoreToAdd)
        {
            m_score += scoreToAdd;
        }
        internal void ResetScore()
        {
            m_score = 0;
        }
        internal void UpdateHealth(int healthToAdd)
        {
            m_health += healthToAdd;
        }
    }
}