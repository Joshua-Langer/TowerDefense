using System.Collections;
using System.Collections.Generic;
using TowerDefense.Towers;
using UnityEngine;

namespace TowerDefense.Managers
{
    public class BuildManager : MonoBehaviour
    {
        public static BuildManager Instance;

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

        private TowerController towerToBuild;
        private TowerPlacement selectedBuildPoint;

        public bool CanBuild {get { return towerToBuild != null;}}
        public bool HasGold {get { return PlayerManager.Instance.Gold >= towerToBuild.GetComponent<TowerController>().levels[0].cost;}}

        public void SelectPoint(TowerPlacement point)
        {
            if(selectedBuildPoint == point)
            {
                DeselectPoint();
                return;
            }
            selectedBuildPoint = point;
            towerToBuild = null;
        }

        public void DeselectPoint()
        {
            selectedBuildPoint = null;
        }

        public void SelectTowerToBuild(TowerController tower)
        {
            towerToBuild = tower;
            DeselectPoint();
        }

        public TowerController GetTowerToBuild()
        {
            return towerToBuild;
        }
    }
}
