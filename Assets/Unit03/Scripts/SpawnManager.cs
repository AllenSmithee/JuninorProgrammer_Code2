using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Unit03
{
    public class SpawnManager : MonoBehaviour
    {

        [Header("Debug")]
        [SerializeField] private bool m_debugMode = false;

        [Space(10)]

        [SerializeField] private GameObject[] m_obstaclePrefabs;
        [SerializeField] Vector3 m_spawnPos = new Vector3(25, 0, 0);

        [SerializeField] Transform m_prefabContainer;
        private CancellationTokenSource m_cts;


        // Start is called before the first frame update
        void OnEnable()
        {
            m_cts = new CancellationTokenSource();
            SpawnRandomObstacleAsync().Forget();
        }

        async UniTask SpawnRandomObstacleAsync()
        {
            while (!GameManager.Instance.IsGameOver && !m_debugMode)
            {
                float randomRepeatRate = Random.Range(0.0f, 2.0f);
                await UniTask.Delay((int)(randomRepeatRate * 1000), m_cts.Token.IsCancellationRequested); // Convert seconds to milliseconds

                if (m_cts.Token.IsCancellationRequested)
                {
                    break;
                }

                int randomIndex = Random.Range(0, m_obstaclePrefabs.Length);
                var target = Instantiate(m_obstaclePrefabs[randomIndex], m_spawnPos, m_obstaclePrefabs[randomIndex].transform.rotation);
                target.transform.SetParent(m_prefabContainer);

            }
        }

        void OnDestroy()
        {
            m_cts.Cancel();
        }


    }
}