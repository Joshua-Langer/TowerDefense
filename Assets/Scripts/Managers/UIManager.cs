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

        public void PauseGame()
        {
            if(GameManager.Instance.GamePaused) //See if the game was paused and pause all time in the game world.
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }

        public void NextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Will be bugged if there isn't a scene remaining.
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //get the active scene buildIndex ID and load it again.
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(GameManager.Instance.LevelList[0]);
        }

        public void StartGame()
        {
            SceneManager.LoadScene(GameManager.Instance.LevelList[1], LoadSceneMode.Single);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}