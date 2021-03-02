using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Towers{
    [CreateAssetMenu(fileName = "Tower", menuName = "TowerDefenseConfig/Towers", order = 3)]
    public class BaseTower : ScriptableObject
    {
        public float fireRate;
        public Sprite towerSprite;
        public GameObject towerProjectile;
        public int towerCost;
        public int towerSell;

        //public List<TowerLevel> levels;
    }

    // [System.Serializable]
    // public class TowerLevel
    // {
    //     public int towerCost;
    //     public int towerSell;
    //     public float fireRate;
    //     public GameObject towerSprite;
    //     public GameObject towerProjectile;
    // }
}