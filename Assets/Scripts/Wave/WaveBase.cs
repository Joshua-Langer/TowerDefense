using System.Collections;
using System.Collections.Generic;
using TowerDefense.Units;
using UnityEngine;

namespace TowerDefense.Waves{
    [CreateAssetMenu(fileName = "Wave", menuName = "TowerDefenseConfig/Waves", order = 1)]
    public class WaveBase : ScriptableObject
    {
        public int unitCount;
        public int waveLimit;
        public bool bossWave;
        public GameObject[] waveUnits; 
    }
}
