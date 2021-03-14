using TowerDefense.Managers;
using TowerDefense.Waves;
using UnityEngine;

namespace TowerDefense.UI{
    public class LevelCompletePanel : MonoBehaviour
    {
        public string menuSceneName = "MainMenu";
        public string nextLevel = "";
        public int levelToUnlock;
        public SceneFader sceneFader;

        //SceneFader?
        
        public void NextLevel()
        {
            PlayerPrefs.SetInt("LevelReached", levelToUnlock);
            sceneFader.FadeTo(nextLevel);
            WaveSpawner.currentWave = 1;
            gameObject.SetActive(false);
        }

        void Awake()
        {
            GameManager.Instance.LevelComplete = false;
        }
    }
}
