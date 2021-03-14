using TowerDefense.Managers;
using UnityEngine;

namespace TowerDefense.Towers{
    public class TowerPlacement : MonoBehaviour
    {
        public Transform towerSpawnPoint;
        [SerializeField]private GameObject tower;
        
        public Color hoverColor;
        public Color notEnoughGoldColor;
        public Color startColor;
        public SpriteRenderer pointSprite;

        void OnMouseDown()
        {
            if(tower != null)
            {
                BuildManager.Instance.SelectPoint(this);
                return;
            }

            if(!BuildManager.Instance.CanBuild)
            {
                return;
            }
            BuildTower(BuildManager.Instance.GetTowerToBuild()); 
        }

        void BuildTower(TowerController towerToBuild)
        {
            if(!BuildManager.Instance.HasGold)
            {
                Debug.Log("Not enough gold");
                return;
            }

            PlayerManager.Instance.Gold -= towerToBuild.levels[0].cost;

            GameObject _tower = (GameObject)Instantiate(towerToBuild.fullPrefab, towerSpawnPoint);
            _tower.SetActive(true);
            tower = _tower;
        }

        void OnMouseEnter()
        {
            if(!BuildManager.Instance.CanBuild)
            {
                return;
            }

            if(BuildManager.Instance.HasGold)
            {
                pointSprite.color = hoverColor;
            }
            else
            {
                pointSprite.color = notEnoughGoldColor;
            }
        }

        void OnMouseExit()
        {
            pointSprite.color = startColor;
        }


    }
}
