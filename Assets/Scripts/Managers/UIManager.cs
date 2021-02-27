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
            //Load the next map for play
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //get the active scene buildIndex ID and load it again.
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(0); //0 is always in the build index as the main menu for this game.
        }
    }
}