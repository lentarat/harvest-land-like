using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Audio
{
    public class HarvestAudioSystem : MonoBehaviour
    {
        [Header("Audio")]
        [SerializeField] private AudioSource _musicAudioSource;
        [SerializeField] private AudioSource _sfxAudioSource;

        [Header("Clips")]
        [SerializeField] private AudioClip _backgroundMusicAudioClip;
        [SerializeField] private AudioClip _harvestAudioClip;

        [Header("Pool")]
        [SerializeField] private int _poolSize = 3;

        [Header("Settings")]
        [SerializeField] private float _cooldown = 0.1f;
        
        private float _lastPlayTime;
        private readonly Queue<AudioSource> _pool = new();

        private void Awake()
        {
            InitPool();
            PlayMusic();
        }

        private void InitPool()
        {
            for (int i = 0; i < _poolSize; i++)
            {
                AudioSource audioSource = Instantiate(_sfxAudioSource, transform);
                audioSource.gameObject.SetActive(false);
                _pool.Enqueue(audioSource);
            }
        }

        private void PlayMusic()
        {
            _musicAudioSource.clip = _backgroundMusicAudioClip;
            _musicAudioSource.loop = true;
            _musicAudioSource.Play();
        }

        private AudioSource GetAudioSource()
        {
            foreach (var src in _pool)
            {
                if (!src.isPlaying)
                    return src;
            }

            AudioSource audioSource = _pool.Dequeue();
            _pool.Enqueue(audioSource);
            return audioSource;
        }

        public void PlayHarvestSFX()
        {
            if (Time.time - _lastPlayTime < _cooldown)
            {
                return;
            }

            _lastPlayTime = Time.time;

            AudioSource audioSource = GetAudioSource();

            audioSource.gameObject.SetActive(true);
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.clip = _harvestAudioClip;
            audioSource.Play();
        }
    }
}