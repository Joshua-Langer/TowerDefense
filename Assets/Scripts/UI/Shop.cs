using System.Collections;
using System.Collections.Generic;
using TowerDefense.Managers;
using TowerDefense.Towers;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.UI
{
    public class Shop : MonoBehaviour
    {
        public TowerController standardTower;
        public TowerController machineGunTower;
        public TowerController cannonTower;
        public TowerController rocketTower;
        public Text standardCost;
        public Text machineGunCost;
        public Text cannonCost;
        public Text rocketCost;

        void Awake()
        {
            standardCost.text = standardTower.levels[0].cost.ToString();
            machineGunCost.text = machineGunTower.levels[0].cost.ToString();
            cannonCost.text = cannonTower.levels[0].cost.ToString();
            rocketCost.text = rocketTower.levels[0].cost.ToString();
        }


        public void SelectStandardTower()
        {
            BuildManager.Instance.SelectTowerToBuild(standardTower);
        }

        public void SelectMachineGunTower()
        {
            BuildManager.Instance.SelectTowerToBuild(machineGunTower);
        }

        public void SelectCannonTower()
        {
            BuildManager.Instance.SelectTowerToBuild(cannonTower);
        }

        public void SelectRocketTower()
        {
            BuildManager.Instance.SelectTowerToBuild(rocketTower);

        }
    }
}
