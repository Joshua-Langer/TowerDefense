using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Towers{
    public class TowerController : MonoBehaviour
    {
        private int currentLevel;
        private int maxLevel = 4;
        public int TowerLevel { get { return currentLevel;} set{ currentLevel += TowerLevel;}}
        public BaseTower towerData;

        [HideInInspector] public int towerCost;

        void Awake()
        {
            towerCost = towerData.towerCost;
        }
    }
}
