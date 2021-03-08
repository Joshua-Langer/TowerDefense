using System.Collections;
using System.Collections.Generic;
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
            Debug.Log("Standard Tower Selected");
        }

        public void SelectMachineGunTower()
        {
            Debug.Log("MachineGun Tower Selected");
        }
    }
}
