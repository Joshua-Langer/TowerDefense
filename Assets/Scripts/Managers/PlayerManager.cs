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
            set { health = value;}
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
            Health = 500; //DEBUG TESTING ONLY. Otherwise health is between 5 and 10, not decided on final number yet, subject to tuning and testing.
        }
    }
}
