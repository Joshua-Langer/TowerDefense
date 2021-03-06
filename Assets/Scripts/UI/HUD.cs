using System.Collections;
using System.Collections.Generic;
using TowerDefense.Managers;
using TowerDefense.Waves;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.UI{
    public class HUD : MonoBehaviour
    {
        public Text playerHealth;
        public Text playerGold;
        public Text playerWave;
        public GameObject pausePanel;
        public GameObject gameOverPanel;
        public GameObject levelCompletePanel;

        void Start()
        {
            pausePanel.SetActive(false);
            gameOverPanel.SetActive(false);
            levelCompletePanel.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            playerGold.text = PlayerManager.Instance.Gold.ToString();
            playerHealth.text = PlayerManager.Instance.Health.ToString();
            playerWave.text = WaveSpawner.currentWave.ToString();
            Pause();
            if(GameManager.Instance.GameOver)
            {
                GameIsOver();
            }
            if(GameManager.Instance.LevelComplete)
            {
                LevelIsComplete();
            }
            else if(!GameManager.Instance.LevelComplete)
            {
                levelCompletePanel.SetActive(false);
            }
        }

        void Pause()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.GamePaused = true;
                UIManager.Instance.PauseGame();
                pausePanel.SetActive(true);  
            }
            if(Input.GetKey(KeyCode.Space) && GameManager.Instance.GamePaused)
            {
                GameManager.Instance.GamePaused = false;
                UIManager.Instance.PauseGame();
                pausePanel.SetActive(false);
            }
        }

        void GameIsOver()
        {
            gameOverPanel.SetActive(true);
            //Time.timeScale = 0;
        }

        void LevelIsComplete()
        {
            levelCompletePanel.SetActive(true);
        }

        public void PauseMainMenu()
        {
            UIManager.Instance.MainMenu();
        }

        public void PauseRestartLevel()
        {
            UIManager.Instance.RestartLevel();
        }

        public void PauseQuitGame()
        {
            UIManager.Instance.ExitGame();
        }
    }
}
