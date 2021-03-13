using UnityEngine;

namespace TowerDefense.UI{
    public class MainMenu : MonoBehaviour
    {
        public GameObject quitButton;
        public GameObject infoPanel;
        public GameObject creditsPanel;

        public SceneFader sceneFader;

        public void StartGame()
        {
            sceneFader.FadeTo("LevelSelect");
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void OpenInfo()
        {
            infoPanel.SetActive(true);
        }

        public void CloseInfo()
        {
            infoPanel.SetActive(false);
        }

        public void OpenCredits()
        {
            creditsPanel.SetActive(true);
        }

        public void CloseCredits()
        {
            creditsPanel.SetActive(false);
        }

        void Awake()
        {
            #if UNITY_WEB_GL
            quitButton.SetActive(false);
            #endif

            #if UNITY_EDITOR
            quitButton.SetActive(false);
            #endif
        }

        
    }
}
