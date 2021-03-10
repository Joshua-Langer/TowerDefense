using System.Collections;
using System.Collections.Generic;
using TowerDefense.Managers;
using TowerDefense.Units;
using UnityEngine;

namespace TowerDefense.Projectile{
    public class ProjectileBehavior : MonoBehaviour
    {
        public float speed = 10;
        public int damage = 5;
        public GameObject target;
        public Vector3 startPos;
        public Vector3 targetPos;

        private float distance;
        private float startTime;

        void Start()
        {
            startTime = Time.time;
            distance = Vector2.Distance(startPos, targetPos);
        }

        void Update()
        {
            var timeInterval = Time.time - startTime;
            gameObject.transform.position = Vector3.Lerp(startPos, targetPos, timeInterval * speed / distance);

            if(gameObject.transform.position.Equals(targetPos))
            {
                if(target != null)
                {
                    Transform unitHealthBarTransform = target.transform.Find("HealthBar");
                    HealthBar healthBar = unitHealthBarTransform.gameObject.GetComponent<HealthBar>();
                    healthBar.currentHealth -= Mathf.Max((damage - target.GetComponent<UnitMovement>().armor), 0);

                    if(healthBar.currentHealth <= 0)
                    {
                        PlayerManager.Instance.Gold += target.GetComponent<UnitMovement>().goldRewardOnDeath;
                        Destroy(target);
                        //SFX and VFX
                    }
                }
                Destroy(gameObject);
            }
        }
    }
}
