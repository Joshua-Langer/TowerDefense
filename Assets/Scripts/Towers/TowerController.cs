using System.Collections;
using System.Collections.Generic;
using TowerDefense.Projectile;
using TowerDefense.Units;
using UnityEngine;

namespace TowerDefense.Towers{
    public class TowerController : MonoBehaviour
    {
        private int currentLevel;
        private int maxLevel = 4;
        public int TowerLevel { get { return currentLevel;} set{ currentLevel += TowerLevel;}}
        public BaseTower towerData;
        [HideInInspector] public int towerCost;

        private float lastShotTime = 0f;
        public List<GameObject> enemiesInRange;

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
                //Setup Delegate - Add Listener
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.gameObject.tag.Equals("Enemy"))
            {
                enemiesInRange.Remove(collision.gameObject);
                //Setup Delegate - Remove Listener
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
