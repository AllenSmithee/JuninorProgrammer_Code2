using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Unit03
{
    public class AudioSelector : MonoBehaviour
    {
        [SerializeField] private AudioSource m_audioSource;



        void OnEnable()
        {
            SetRandomBGM();

        }

        void SetRandomBGM()
        {
            var targetbgm = AudioManager.Instance.GetBGM(Random.Range(0, AudioManager.Instance.BGMCount));
            if (targetbgm is null)
            {
                return;
            }

            m_audioSource.clip = targetbgm;
            m_audioSource.Play();
        }

        [ContextMenu("Get 1 BGM")]
        public void GetFirstBGM()
        {
            var targetbgm = AudioManager.Instance.GetBGM(0);
            if (targetbgm is null)
            {
                return;
            }

            m_audioSource.clip = targetbgm;
            m_audioSource.Play();

        }
        [ContextMenu("Get 2 BGM")]
        public void GetSecondBGM()
        {
            var targetbgm = AudioManager.Instance.GetBGM(1);
            if (targetbgm is null)
            {
                return;
            }

            m_audioSource.clip = targetbgm;
            m_audioSource.Play();
        }
        [ContextMenu("Get 3 BGM")]
        public void GetThirdBGM()
        {
            var targetbgm = AudioManager.Instance.GetBGM(2);
            if (targetbgm is null)
            {
                return;
            }

            m_audioSource.clip = targetbgm;
            m_audioSource.Play();
        }
    }
}