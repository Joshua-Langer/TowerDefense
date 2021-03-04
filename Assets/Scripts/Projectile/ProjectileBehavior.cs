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
                    //TEMP, will use a bar for health later. Still very temp for the armor.
                    target.GetComponent<UnitMovement>().health -= Mathf.Max((damage - target.GetComponent<UnitMovement>().armor), 0);

                    if(target.GetComponent<UnitMovement>().health <= 0)
                    {
                        Destroy(target);
                        PlayerManager.Instance.Gold += 50;
                    }
                }
                Destroy(gameObject);
            }
        }
    }
}
