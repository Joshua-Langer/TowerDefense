using TowerDefense.Managers;
using TowerDefense.Player;
using UnityEngine;

namespace TowerDefense.UI
{
    public class GameOver : MonoBehaviour
    {
        public SceneFader sceneFader;
        public void GameOverRetry()
        {
            PlayerPrefs.SetInt("LevelReached", 1);
            PlayerPrefs.SetInt("PlayerGold", 100);
            PlayerPrefs.SetInt("PlayerHealth", 15);
            PlayerInstance.CurrentGold = 100;
            PlayerInstance.CurrentHealth = 15;
            sceneFader.FadeTo("Level1");
            GameManager.Instance.GameOver = false;
        }

        public void GameOverMainMenu()
        {
            PlayerPrefs.SetInt("LevelReached", 1);
            PlayerPrefs.SetInt("PlayerGold", 100);
            PlayerPrefs.SetInt("PlayerHealth", 15);
            PlayerInstance.CurrentGold = 100;
            PlayerInstance.CurrentHealth = 15;
            sceneFader.FadeTo("Main Menu");
            GameManager.Instance.GameOver = false;
        }
    }
}
