using System.Collections;
using System.Collections.Generic;
using TowerDefense.Managers;
using UnityEngine;

namespace TowerDefense.UI{
    public class LevelCompletePanel : MonoBehaviour
    {
        public GameObject nextLevel;
        
        public void NextLevel()
        {
            UIManager.Instance.NextLevel();
        }

        void Awake()
        {
            nextLevel.SetActive(false);
        }
    }
}
