using System.Collections;
using System.Collections.Generic;
using TowerDefense.Managers;
using TowerDefense.Towers;
using UnityEngine;

namespace TowerDefense.UI
{
    public class Shop : MonoBehaviour
    {
        public TowerController standardTower;
        public TowerController machineGunTower;

        public void SelectStandardTower()
        {
            BuildManager.Instance.SelectTowerToBuild(standardTower);
        }

        public void SelectMachineGunTower()
        {
            BuildManager.Instance.SelectTowerToBuild(machineGunTower);
        }
    }
}
