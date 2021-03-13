using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Units{
    [CreateAssetMenu(fileName = "Unit", menuName = "TowerDefenseConfig/Units", order = 2)]
    public class UnitBase : ScriptableObject
    {
        public int health;
        public Sprite unitSprite;
        public float speed;
        public int armor;
        public int goldReward;
        public bool isBoss;
    }
}
