using System.Collections;
using System.Collections.Generic;
using TowerDefense.Managers;
using TowerDefense.Waves;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense.UI{
    public class LevelCompletePanel : MonoBehaviour
    {
        public GameObject nextLevel;
        
        public void NextLevel()
        {
            UIManager.Instance.NextLevel();
            WaveSpawner.currentWave = 1;
            gameObject.SetActive(false);
        }

        void Awake()
        {
            gameObject.SetActive(false);
        }
    }
}
