using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Units{
    [CreateAssetMenu(fileName = "Unit", menuName = "TowerDefenseConfig/Units", order = 2)]
    public class UnitBase : ScriptableObject
    {
        public int health;
        public Sprite unitSprite;
        public int damage;
        public float speed;
    }
}
