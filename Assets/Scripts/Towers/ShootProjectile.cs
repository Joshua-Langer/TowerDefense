using System.Collections.Generic;
using TowerDefense.Projectile;
using TowerDefense.Units;
using UnityEngine;

namespace TowerDefense.Towers{
    public class ShootProjectile : MonoBehaviour
    {
        private float lastShotTime;
        private TowerController towerController;
        public List<GameObject> enemiesInRange;

        void Start()
        {
            lastShotTime = Time.time;
            towerController = gameObject.GetComponentInChildren<TowerController>();
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
                if(Time.time - lastShotTime > towerController.CurrentLevel.fireRate)
                {
                    Shoot(target.GetComponent<Collider2D>());
                    lastShotTime = Time.time;
                }
                Vector3 direction = gameObject.transform.position - target.transform.position;
                gameObject.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI, new Vector3(0, 0, 1)); //TODO: Fix the sprite rotations to be all the same regardless of sprite. Likely needs to face Left.
            }
        }

        void Shoot(Collider2D target)
        {
            var bulletPrefab = towerController.CurrentLevel.projectile;

            var startPos = towerController.levels[towerController.currentLevel].firePoint.transform.position;
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
    }
}
