using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unit05
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] List<GameObject> m_goodsList;
        [SerializeField] List<GameObject> m_badsList;

        [SerializeField] GameObject m_goodsContainer;
        [SerializeField] GameObject m_badsContainer;



        [SerializeField] GameObject m_gameOverText;
        [SerializeField] GameObject m_difficultySelectPanel;

        [SerializeField] float m_spawnRate = 1.0f;

        GameState m_gameState = GameState.None;

        [Space(10), Header("Debug")]
        [SerializeField] private bool m_keepSpawningOnGameOver = false;

        private bool m_spawnTarget = true;

        internal float SpawnRate { get => m_spawnRate; set => m_spawnRate = value; }

        internal GameState CurrentState
        {
            get => m_gameState;
            set
            {
                if (m_gameState == value)
                {
                    return;
                }

                m_gameState = value;
                switch (m_gameState)
                {
                    case GameState.DifficultySelect:
                        DifficultySelectUISetting();
                        break;
                    case GameState.StartGame:
                        StartGameUISetting();
                        StartCoroutine(SpawnTargets());
                        break;
                    case GameState.GameOver:
                        GameOverUISetting();
                        break;
                    case GameState.None:
                        break;
                }
            }
        }

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
            CurrentState = GameState.DifficultySelect;

        }

        // Update is called once per frame
        void Update()
        {
            if (ScoreManager.Instance.IsGameOver)
                CurrentState = GameState.GameOver;

        }


        [ContextMenu("Instantiate Goods")]
        void InstantiateGoods()
        {
            foreach (var good in m_goodsList)
            {
                Instantiate(good, m_goodsContainer.transform);
            }
        }

        [ContextMenu("Instantiate Bads")]
        void InstantiateBads()
        {
            foreach (var bad in m_badsList)
            {
                Instantiate(bad, m_badsContainer.transform);
            }
        }


        IEnumerator SpawnTargets()
        {
            while (m_spawnTarget)
            {
                yield return new WaitForSeconds(m_spawnRate);
                var combinedList = new List<GameObject>().Concat(m_goodsList).Concat(m_badsList).ToList();
                int index = UnityEngine.Random.Range(0, combinedList.Count);
                GameObject prefab = combinedList[index];
                GameObject newObject = Instantiate(prefab);

                if (m_goodsList.Contains(prefab))
                {
                    newObject.transform.SetParent(m_goodsContainer.transform);
                }
                else if (m_badsList.Contains(prefab))
                {
                    newObject.transform.SetParent(m_badsContainer.transform);
                }

            }
        }

        public void RestartGame()
        {
            ScoreManager.Instance.ResetScore();
            UnityEngine.SceneManagement.Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }

        internal void UpdateGameState(GameState gameState)
        {
            CurrentState = gameState;
        }

        public void GameOverUISetting()
        {
            m_gameOverText.SetActive(true);
            if (m_keepSpawningOnGameOver)
            {
                m_spawnTarget = true;
            }
            else
            {
                m_spawnTarget = false;
            }
        }

        void DifficultySelectUISetting()
        {
            m_gameOverText.SetActive(false);
            m_difficultySelectPanel.SetActive(true);
        }
        void StartGameUISetting()
        {
            m_difficultySelectPanel.SetActive(false);
            m_gameOverText.SetActive(false);
        }

    }
    enum GameState
    {
        None,
        DifficultySelect,
        StartGame,
        GameOver
    }
}