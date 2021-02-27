using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    namespace TowerDefense.Managers{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance = null;

        private int level;
        public int Wave {get; set;}
        [HideInInspector] public bool GamePaused {get; set;}
        [HideInInspector] public bool GameOver {get; set;}

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
