using Cysharp.Threading.Tasks;
using Noname.Extentions;
using Noname.SceneManage;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unit03
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private GameObject m_gameOverPanel;
        [SerializeField] SceneHandler m_targetScene;

        public bool IsGameOver { get; set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            Instance = this;

            IsGameOver = false;

            m_targetScene.UpdateTargetScene(m_targetScene.TargetScene.SceneAsset);


        }

        void Start()
        {
            if (!m_targetScene.TargetScene.IsLoaded())
                m_targetScene.TargetScene.AsyncLoadTarget().Forget();
        }

        private void Update()
        {
            if (IsGameOver)
            {
                m_gameOverPanel.SetActive(true);
            }
            else
            {
                m_gameOverPanel.SetActive(false);

            }

        }

        [ContextMenu("Restart Game")]
        public void RestartGame()
        {
            IsGameOver = false;

            m_targetScene.TargetScene.AsyncUnloadTarget().Forget();
            m_targetScene.TargetScene.AsyncLoadTarget().Forget();
        }

        [ContextMenu("UnLoad Scene")]
        void UnloadScene()
        {
            m_targetScene.TargetScene.AsyncUnloadTarget().Forget();
        }

        [ContextMenu("Load Scene")]
        void LoadScene()
        {
            IsGameOver = false;
            m_targetScene.TargetScene.AsyncLoadTarget().Forget();
        }




    }
}