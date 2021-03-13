using System.Collections;
using System.Collections.Generic;
using TowerDefense.Managers;
using TowerDefense.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense.UI
{
    public class GameOver : MonoBehaviour
    {
        public void GameOverRetry()
        {
            PlayerPrefs.SetInt("LevelReached", 1);
            PlayerPrefs.SetInt("PlayerGold", 100);
            PlayerPrefs.SetInt("PlayerHealth", 15);
            PlayerInstance.CurrentGold = 100;
            PlayerInstance.CurrentHealth = 15;
            SceneManager.LoadScene("Level1");
            GameManager.Instance.GameOver = false;
        }

        public void GameOverMainMenu()
        {
            PlayerPrefs.SetInt("LevelReached", 1);
            PlayerPrefs.SetInt("PlayerGold", 100);
            PlayerPrefs.SetInt("PlayerHealth", 15);
            PlayerInstance.CurrentGold = 100;
            PlayerInstance.CurrentHealth = 15;
            SceneManager.LoadScene("Main Menu");
            GameManager.Instance.GameOver = false;
        }
    }
}
