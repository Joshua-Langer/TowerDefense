using System.Collections;
using System.Collections.Generic;
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
            set { gold = value; Debug.Log("Gold is at " + gold);}
        }

        public int Health
        {
            get { return health;}
            set { health = value; Debug.Log("Health is at " + health);}
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
            Gold = 150;
            Health = 5;
        }
    }
}
