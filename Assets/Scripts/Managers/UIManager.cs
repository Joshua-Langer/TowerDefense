using TowerDefense.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense.Managers{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance = null;

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

        public void NextLevel(string levelName)
        {
            SceneManager.LoadScene(levelName);
            SavePlayerToInstance();
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //get the active scene buildIndex ID and load it again.
            LoadPlayerFromInstance();
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(GameManager.Instance.LevelList[0]);
        }

        public void StartGame()
        {
            SceneManager.LoadScene(GameManager.Instance.LevelList[1], LoadSceneMode.Single);
            SavePlayerToInstance(); //TEMP FOR NOW until the level screen is setup.
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        void SavePlayerToInstance()
        {
            PlayerInstance.CurrentGold = PlayerManager.Instance.Gold;
            PlayerInstance.CurrentHealth = PlayerManager.Instance.Health;
            PlayerPrefs.SetInt("PlayerGold", PlayerInstance.CurrentGold);
            PlayerPrefs.SetInt("PlayerHealth", PlayerInstance.CurrentHealth);
            //save last level completed.
        }

        void LoadPlayerFromInstance()
        {
            PlayerManager.Instance.Gold = PlayerInstance.CurrentGold;
            PlayerManager.Instance.Health = PlayerInstance.CurrentHealth;
        }
    }
}