﻿using System.Collections;
using System.Collections.Generic;
using TowerDefense.Managers;
using TowerDefense.Projectile;
using TowerDefense.Units;
using UnityEngine;

namespace TowerDefense.Towers{
    [System.Serializable]
    public class TowerLevel{
        public int cost;
        public float fireRate;
        public GameObject towerGO;
        public GameObject projectile;
    }

    public class TowerController : MonoBehaviour
    {
        public List<TowerLevel> levels;
        private TowerLevel towerLevel;

        public GameObject fullPrefab;

        public TowerLevel CurrentLevel
        {
            get
            {
                return towerLevel;
            }
            set
            {
                towerLevel = value;
                var towerLevelIndex = levels.IndexOf(towerLevel);
                var towerVisual = levels[towerLevelIndex].towerGO;

                for(var i = 0; i < levels.Count; i++)
                {
                    if(i == towerLevelIndex)
                    {
                        levels[i].towerGO.SetActive(true);
                    }
                    else
                    {
                        levels[i].towerGO.SetActive(false);
                    }
                }
            }
        }

        public TowerLevel GetNextLevel()
        {
            var towerLevelIndex = levels.IndexOf(towerLevel);
            var maxLevelIndex = levels.Count - 1;
            if(towerLevelIndex < maxLevelIndex)
            {
                return levels[towerLevelIndex + 1];
            }
            else
            {
                return null;
            }
        }

        private bool CanUpgradeTower()
        {
            TowerLevel nextTowerLevel = GetNextLevel();
            if(nextTowerLevel != null)
            {
                return PlayerManager.Instance.Gold >= nextTowerLevel.cost;
            }
            return false;
        }

        void OnEnable()
        {
            CurrentLevel = levels[0];
            Debug.Log(levels.Count);
        }

        public void IncreaseLevel()
        {
            var towerLevelIndex = levels.IndexOf(CurrentLevel);
            if(towerLevelIndex < levels.Count - 1)
            {
                CurrentLevel = levels[towerLevelIndex + 1];
            }
        }

        private void OnMouseUp()
        {
            if(CanUpgradeTower())
            {
                IncreaseLevel();
                PlayerManager.Instance.Gold -= CurrentLevel.cost;
            }
        }

        
    }
}
