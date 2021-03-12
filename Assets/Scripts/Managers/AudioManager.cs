using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        // SFX
        [SerializeField]
        AudioClip unitDeathSound;
        [SerializeField]
        AudioClip waveStartSound;

        [Header("Audio Source")]
        AudioSource sfx;
        void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }
        public IEnumerator UnitDeathSound(Vector3 transform)
        {
            sfx = GetComponent<AudioSource>();
            yield return new WaitForSeconds(0);
            AudioSource.PlayClipAtPoint(unitDeathSound, transform);
        }

        public IEnumerator WaveStartSound(Vector3 transform)
        {
            sfx = GetComponent<AudioSource>();
            yield return new WaitForSeconds(0);
            AudioSource.PlayClipAtPoint(waveStartSound, transform);
        }

    }
}
