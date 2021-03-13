using System.Collections;
using System.Collections.Generic;
using TowerDefense.UI;
using UnityEngine;

namespace TowerDefense.Managers{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance = null;

        private int gold;
        private int health;

        public int Gold
        {
            get { return gold;}
            set { gold = value;}
        }

        public int Health
        {
            get { return health;}
            set
            { 
                 //Camera Shake?
                 health = value;
                 if(health <=0 && !GameManager.Instance.GameOver)
                 {
                     GameManager.Instance.GameOver = true;
                 }
            }
        }

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

        void Start()
        {
            Gold = 100;
            Health = 15;
        }
    }
}
