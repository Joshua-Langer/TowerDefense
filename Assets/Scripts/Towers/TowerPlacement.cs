using System.Collections;
using System.Collections.Generic;
using TowerDefense.Managers;
using UnityEngine;

namespace TowerDefense.Towers{
    public class TowerPlacement : MonoBehaviour
    {
        public Transform towerSpawnPoint;
        [SerializeField]private GameObject tower;
        public GameObject towerPrefab; //TESTING ONLY

        private bool CanPlaceTower()
        {
            var cost = towerPrefab.GetComponent<TowerController>().levels[0].cost;
            return tower == null && PlayerManager.Instance.Gold >= cost;
        }

        private void OnMouseUp()
        {
            if(CanPlaceTower())
            {
                tower = Instantiate(towerPrefab, towerSpawnPoint.position, Quaternion.identity) as GameObject;
                PlayerManager.Instance.Gold -= tower.GetComponent<TowerController>().CurrentLevel.cost;
            }
        }
    }
}
