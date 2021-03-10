using System.Collections;
using System.Collections.Generic;
using TowerDefense.Managers;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense.UI{
    public class MainMenu : MonoBehaviour
    {
        public GameObject quitButton;

        public void StartGame()
        {
            SceneManager.LoadScene("LevelSelect");
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        void Awake()
        {
            #if UNITY_EDITOR_WEB_GL
            quitButton.SetActive(false);
            #endif

            #if UNITY_EDITOR
            quitButton.SetActive(false);
            #endif
            
        }

        
    }
}
