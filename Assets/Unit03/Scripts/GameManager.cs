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

        [ContextMenu("Restart Game")]
        void RestartGame()
        {
            IsGameOver = false;

            m_targetScene.TargetScene.AsyncUnloadTarget().Forget();
            m_targetScene.TargetScene.AsyncLoadTarget().Forget();

        }
        [ContextMenu("UnLoad Scene")]
        void UnloadScene()
        {
            IsGameOver = false;
            m_targetScene.TargetScene.AsyncUnloadTarget().Forget();
        }
        [ContextMenu("Load Scene")]
        void LoadScene()
        {
            m_targetScene.TargetScene.AsyncLoadTarget().Forget();
        }




    }
}