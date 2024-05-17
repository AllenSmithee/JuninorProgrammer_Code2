using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Unit03
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] m_obstaclePrefabs;
        [SerializeField] Vector3 m_spawnPos = new Vector3(25, 0, 0);

        [SerializeField] Transform m_prefabContainer;
        private float m_startDelay = 1.0f;
        private float m_repeatRate = 2.0f;

        // Start is called before the first frame update
        void OnEnable()
        {
            SpawnRandomObstacleAsync().Forget();
        }

        async UniTaskVoid SpawnRandomObstacleAsync()
        {
            while (!GameManager.Instance.IsGameOver)
            {
                float randomRepeatRate = Random.Range(0.0f, 2.0f);
                await UniTask.Delay((int)(randomRepeatRate * 1000)); // Convert seconds to milliseconds

                int randomIndex = Random.Range(0, m_obstaclePrefabs.Length);
                var target = Instantiate(m_obstaclePrefabs[randomIndex], m_spawnPos, m_obstaclePrefabs[randomIndex].transform.rotation);
                target.transform.SetParent(m_prefabContainer);
            }
        }



    }
}