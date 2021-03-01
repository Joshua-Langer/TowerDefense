using System.Collections;
using System.Collections.Generic;
using TowerDefense.Projectile;
using TowerDefense.Units;
using UnityEngine;

namespace TowerDefense.Towers{
    public class TowerController : MonoBehaviour
    {
        public BaseTower towerData;
        [HideInInspector] public int towerCost;
        private float lastShotTime = 0f;
        public List<GameObject> enemiesInRange;

        private TowerLevel towerLevel;

        public TowerLevel CurrentLevel
        {
            get
            {
                return towerLevel;
            }
            set
            {
                towerLevel = value;
                var towerLevelIndex = towerData.levels.IndexOf(towerLevel);
                var towerVisual = towerData.levels[towerLevelIndex].towerSprite;

                for(var i = 0; i < towerData.levels.Count; i++)
                {
                    if(i == towerLevelIndex)
                    {
                        towerData.levels[i].towerSprite.SetActive(true);
                    }
                    else
                    {
                        towerData.levels[i].towerSprite.SetActive(false);
                    }
                }
            }
        }

        public TowerLevel GetNextLevel()
        {
            var towerLevelIndex = towerData.levels.IndexOf(towerLevel);
            var maxLevelIndex = towerData.levels.Count - 1;
            if(towerLevelIndex < maxLevelIndex)
            {
                return towerData.levels[towerLevelIndex + 1];
            }
            else
            {
                return null;
            }
        }

        public void IncreaseLevel()
        {
            var towerLevelIndex = towerData.levels.IndexOf(CurrentLevel);
            if(towerLevelIndex < towerData.levels.Count - 1)
            {
                CurrentLevel = towerData.levels[towerLevelIndex + 1];
            }
        }

        void Awake()
        {
            towerCost = towerData.towerCost;
        }

        void Start()
        {
            lastShotTime = Time.time;
            enemiesInRange = new List<GameObject>();
        }

        void Update()
        {
            GetTarget();
        }

        void GetTarget()
        {
            GameObject target = null;
            var minimalEnemyDistance = float.MaxValue;
            foreach(var enemy in enemiesInRange)
            {
                float distanceToGoal = enemy.GetComponent<UnitMovement>().DistanceToGoal();
                if(distanceToGoal < minimalEnemyDistance)
                {
                    target = enemy;
                    minimalEnemyDistance = distanceToGoal;
                }
            }

            if(target != null)
            {
                if(Time.time - lastShotTime > towerData.fireRate)
                {
                    Shoot(target.GetComponent<Collider2D>());
                    lastShotTime = Time.time;
                }
                Vector3 direction = gameObject.transform.position - target.transform.position;
                gameObject.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI, new Vector3(0, 0, 1));
            }
        }

        void OnEnemyDestroy(GameObject enemy)
        {
            enemiesInRange.Remove(enemy);
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag.Equals("Enemy"))
            {
                enemiesInRange.Add(collision.gameObject);
                UnitDestructionDelegate del = collision.gameObject.GetComponent<UnitDestructionDelegate>();
                del.unitDelegate += OnEnemyDestroy;
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.gameObject.tag.Equals("Enemy"))
            {
                enemiesInRange.Remove(collision.gameObject);
                UnitDestructionDelegate del = collision.gameObject.GetComponent<UnitDestructionDelegate>();
                del.unitDelegate -= OnEnemyDestroy;
            }
        }

        void Shoot(Collider2D target)
        {
            var bulletPrefab = towerData.towerProjectile;

            var startPos = gameObject.transform.position;
            var targetPos = target.transform.position;
            startPos.z = bulletPrefab.transform.position.z;
            targetPos.z = bulletPrefab.transform.position.z;

            var newBullet = Instantiate(bulletPrefab) as GameObject;
            newBullet.transform.position = startPos;
            var projectile = newBullet.GetComponent<ProjectileBehavior>();
            projectile.target = target.gameObject;
            projectile.startPos = startPos;
            projectile.targetPos = targetPos;

            //Animations and SFX
        }
    }
}
