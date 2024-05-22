using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit04
{
    public class SpawnManager : MonoBehaviour
    {
        public static SpawnManager Instance;
        [SerializeField] private GameObject m_enemyPrefab;
        [SerializeField] private GameObject m_powerUpPrefab;

        private float m_spawnRange = 9;

        private List<Enemy> m_enemiesList = new List<Enemy>();
        private int m_waveNumber = 1;


        public List<Enemy> EnemiesList { get => m_enemiesList; set => m_enemiesList = value; }

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
            WaveSpawn(m_waveNumber);

        }

        // Update is called once per frame
        void Update()
        {
            if (m_enemiesList.Count == 0)
            {
                m_waveNumber++;
                WaveSpawn(m_waveNumber);
                GetRandomVector3(out Vector3 randomPos);
                Instantiate(m_powerUpPrefab, randomPos, m_powerUpPrefab.transform.rotation);
            }

        }

        void WaveSpawn(int enemyToSpawn)
        {
            for (int i = 0; i < enemyToSpawn; i++)
            {
                RandomizeEnemySpawn();
            }
        }

        void GetRandomVector3(out Vector3 randomPos)
        {
            float randomX = Random.Range(-m_spawnRange, m_spawnRange);
            float randomZ = Random.Range(-m_spawnRange, m_spawnRange);

            randomPos = new Vector3(randomX, 0, randomZ);
        }

        void InstantiateEnemy(Vector3 spawnPos)
        {
            var enemy = Instantiate(m_enemyPrefab, spawnPos, m_enemyPrefab.transform.rotation);
            m_enemiesList.Add(enemy.GetComponent<Enemy>());

        }

        void RandomizeEnemySpawn()
        {
            GetRandomVector3(out Vector3 randomPos);

            InstantiateEnemy(randomPos);
        }
    }
}