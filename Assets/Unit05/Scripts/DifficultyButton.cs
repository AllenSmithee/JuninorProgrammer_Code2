using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Unit05
{
    public class DifficultyButton : MonoBehaviour
    {
        private Button m_button;
        [SerializeField] private Difficulty m_difficulty;
        // Start is called before the first frame update
        void Start()
        {
            m_button = GetComponent<Button>();
            m_button.onClick.AddListener(SetDifficulty);

        }
        void OnDestroy()
        {
            m_button.onClick.RemoveListener(SetDifficulty);
        }

        public void SetDifficulty()
        {
            if (GameManager.Instance.CurrentState == GameState.DifficultySelect)
                GameManager.Instance.SpawnRate = m_difficulty switch
                {
                    Difficulty.Easy => 1.0f,
                    Difficulty.Medium => 0.8f,
                    Difficulty.Hard => 0.5f,
                    _ => 1.0f
                };

            GameManager.Instance.CurrentState = GameState.StartGame;
        }
    }
    enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
}