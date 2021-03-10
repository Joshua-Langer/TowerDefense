using System.Collections;
using System.Collections.Generic;
using TowerDefense.Managers;
using TowerDefense.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefense.UI
{
    public class LevelSelect : MonoBehaviour
    {
        public Button[] levelButtons;

        void Awake()
        {
            PlayerInstance.CurrentHealth = PlayerPrefs.GetInt("PlayerHealth", 15);
            PlayerInstance.CurrentGold = PlayerPrefs.GetInt("PlayerGold", 100);
        }

        void Start()
        {
            var levelReached = PlayerPrefs.GetInt("LevelReached", 1);
            LoadPlayerFromInstance();
            for(var i = 0; i < levelButtons.Length; i++)
            {
                if(i + 1 > levelReached)
                {
                    levelButtons[i].interactable = false;
                }
            }
        }

        public void Select(string levelName)
        {
            SceneManager.LoadScene(levelName); //May implement SceneFader Later?
        }

        void LoadPlayerFromInstance()
        {
            PlayerManager.Instance.Gold = PlayerInstance.CurrentGold;
            PlayerManager.Instance.Health = PlayerInstance.CurrentHealth;
        }
    }
}