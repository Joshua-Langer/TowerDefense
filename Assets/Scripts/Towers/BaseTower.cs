using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Towers{
    [CreateAssetMenu(fileName = "Tower", menuName = "TowerDefenseConfig/Towers", order = 3)]
    public class BaseTower : ScriptableObject
    {
        public float fireRate;
        public Sprite towerSprite;
        public Sprite towerProjectile;
        public int towerCost;
        public int towerSell;
    }
}