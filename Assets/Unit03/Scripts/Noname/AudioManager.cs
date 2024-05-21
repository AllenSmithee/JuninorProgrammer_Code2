using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit03
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }
        [SerializeField] private AudioClip m_jumpSound;
        [SerializeField] private AudioClip m_crashSound;
        [SerializeField] private AudioClip[] m_bgm;

        public int BGMCount => m_bgm.Length;
        public AudioClip JumpSound => m_jumpSound;
        public AudioClip CrashSound => m_crashSound;


        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            Instance = this;
        }

        internal AudioClip GetBGM(int targetIndex)
        {
            if (!m_bgm[targetIndex].LoadAudioData())
            {
                return null;
            }

            if (targetIndex > m_bgm.Length)
            {
                return m_bgm[0];
            }
            return m_bgm[targetIndex];
        }


    }
}