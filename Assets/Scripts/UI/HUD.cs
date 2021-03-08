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
        public GameObject gameOverQuit;
        public GameObject pauseQuit;
        public GameObject levelCompleteQuit;

        void Awake()
        {
            #if UNITY_EDITOR
            gameOverQuit.SetActive(false);
            pauseQuit.SetActive(false);
            levelCompleteQuit.SetActive(false);
            #endif
            #if UNITY_EDITOR_WEBGL
            gameOverQuit.SetActive(false);
            pauseQuit.SetActive(false);
            levelCompleteQuit.SetActive(false);
            #endif
        }
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

        void LateUpdate()
        {
            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                Toggle();
            }
        }

        void Toggle()
        {
            pausePanel.SetActive(!pausePanel.activeSelf);
            if(pausePanel.activeSelf)
            {
                GameManager.Instance.GamePaused = true;
                Time.timeScale = 0;
            }
            else
            {
                GameManager.Instance.GamePaused = false;
                Time.timeScale = 1;
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
