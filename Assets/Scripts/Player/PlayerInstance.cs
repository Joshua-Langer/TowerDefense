using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Player
{
    public static class PlayerInstance
    {
        private static int currentGold = 0;
        private static int currentHealth = 0;

        public static int CurrentGold
        {
            get {return currentGold;}
            set {currentGold = value;}
        }

        public static int CurrentHealth
        {
            get {return currentHealth;}
            set {currentHealth = value;}
        }
    }
}
