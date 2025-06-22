using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Manager
{

    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [Header("Audio")]
        [SerializeField] private AudioClip defaultBgm;
        [SerializeField] private AudioSource bgmSource;


        [Header("Volume Settings")]
        [Range(0, 1)] public float bgmVolume = 1f;
        [Range(0, 1)] public float sfxVolume = 1f;
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void PlayBGM(AudioClip clip, bool loop = true)
        {
            if (clip == null)
            {
                return;
            }

            bgmSource.clip = clip;
            bgmSource.loop = loop;
            bgmSource.volume = bgmVolume;
            bgmSource.Play();
        }

        public void StopBGM()
        {
            bgmSource.Stop();
        }

        public void PlayDefaultBGM()
        {
            PlayBGM(defaultBgm);
        }

        public void SetBGMVolume(float volume)
        {
            bgmVolume = volume;
            bgmSource.volume = volume;
        }

        public void ToggleMusic(bool enable)
        {
            bgmSource.mute = !enable;
        }
    }
}
