using System.Collections;
using System.Collections.Generic;
using TowerDefense.Managers;
using TowerDefense.Waves;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense.UI{
    public class LevelCompletePanel : MonoBehaviour
    {
        public string menuSceneName = "MainMenu";
        public string nextLevel = "";
        public int levelToUnlock;

        //SceneFader?
        
        public void NextLevel()
        {
            PlayerPrefs.SetInt("LevelReached", levelToUnlock);
            UIManager.Instance.NextLevel(nextLevel);
            WaveSpawner.currentWave = 1;
            gameObject.SetActive(false);
        }

        void Awake()
        {
            GameManager.Instance.LevelComplete = false;
        }
    }
}
