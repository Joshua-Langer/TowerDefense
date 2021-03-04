using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense.Managers{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance = null;

        public string[] LevelList = new string[]
            {
                "Main Menu",
                "Level1",
                "Level2",
                "Level3",
                "Level4",
                "Level5"
            };

        [HideInInspector] public bool GamePaused {get; set;}
        [HideInInspector] public bool GameOver {get; set;}
        [HideInInspector] public bool LevelComplete{get; set;}

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
    }
}
