using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Projectile{
    public class ProjectileBehavior : MonoBehaviour
    {
        public float speed = 10;
        public int damage = 1;
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
                    //Deduct from enemy health
                    //Then if enemy is at 0 add gold to the player
                }
                Destroy(gameObject);
            }
        }
    }
}
