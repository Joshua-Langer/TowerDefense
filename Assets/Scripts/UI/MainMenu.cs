using System.Collections;
using System.Collections.Generic;
using TowerDefense.Managers;
using UnityEngine;

namespace TowerDefense.UI{
    public class MainMenu : MonoBehaviour
    {
        public void StartGame()
        {
            UIManager.Instance.StartGame();
        }

        public void QuitGame()
        {
            UIManager.Instance.ExitGame();
        }

        
    }
}
