using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Challenge5
{
    public class GameManagerX : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI gameOverText;
        public GameObject titleScreen;
        public Button restartButton;

        public List<GameObject> targetPrefabs;

        private int score;
        private float spawnRate = 1.5f;
        public bool isGameActive;

        private float spaceBetweenSquares = 2.5f;
        private float minValueX = -3.75f; //  x value of the center of the left-most square
        private float minValueY = -3.75f; //  y value of the center of the bottom-most square

        //Noname added
        [Header("Noname added")]
        [SerializeField] private TextMeshProUGUI m_timeText;
        private string m_scoreTextFormat = "Score : {0}";
        private string m_timeTextFormat = "Time : {0}";
        private float m_timeSetting = 60.0f;
        [SerializeField] private TextMeshProUGUI m_healthText;
        public int Health { get; set; }

        [SerializeField] private GameObject m_playerTopUI;
        void Update()
        {
            if (!isGameActive)
            {
                return;
            }

            UpdateScoreUI();
            UpdateTimeUI();
            UpdateHealthUI();
            if(m_timeSetting <= 0 || Health <= 0)
            {
                GameOver();
            }
        }

        void UpdateScoreUI()
        {
            scoreText.text = string.Format(m_scoreTextFormat, score);
        }
        void UpdateTimeUI()
        {
            m_timeSetting -= Time.deltaTime;
            m_timeText.text = string.Format(m_timeTextFormat, m_timeSetting.ToString("F1"));
        }
        void UpdateHealthUI()
        {
            m_healthText.text = string.Format("Health : {0}", Health);
        }

        // Start the game, remove title screen, reset score, and adjust spawnRate based on difficulty button clicked
        public void StartGame(int difficultyValue)
        {
            spawnRate /= difficultyValue;
            isGameActive = true;
            StartCoroutine(SpawnTarget());
            m_timeSetting = 60.0f;
            m_timeText.text = string.Format(m_timeTextFormat, m_timeSetting.ToString("F1"));
            Health = 5;
            score = 0;
            UpdateScore(score);
            titleScreen.gameObject.SetActive(false);
            m_playerTopUI.SetActive(true);
        }

        // While game is active spawn a random target
        IEnumerator SpawnTarget()
        {
            while (isGameActive)
            {
                yield return new WaitForSeconds(spawnRate);
                int index = Random.Range(0, targetPrefabs.Count);

                if (isGameActive)
                {
                    Instantiate(targetPrefabs[index], RandomSpawnPosition(), targetPrefabs[index].transform.rotation);
                }

            }
        }

        // Generate a random spawn position based on a random index from 0 to 3
        Vector3 RandomSpawnPosition()
        {
            float spawnPosX = minValueX + (RandomSquareIndex() * spaceBetweenSquares);
            float spawnPosY = minValueY + (RandomSquareIndex() * spaceBetweenSquares);

            Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, 0);
            return spawnPosition;

        }

        // Generates random square index from 0 to 3, which determines which square the target will appear in
        int RandomSquareIndex()
        {
            return Random.Range(0, 4);
        }

        // Update score with value from target clicked
        public void UpdateScore(int scoreToAdd)
        {
            score += scoreToAdd;
        }

        // Stop game, bring up game over text and restart button
        public void GameOver()
        {
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            isGameActive = false;
        }

        // Restart game by reloading the scene
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}