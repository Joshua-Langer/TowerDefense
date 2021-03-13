using TowerDefense.Managers;
using UnityEngine;

namespace TowerDefense.Units{
    public class UnitMovement : MonoBehaviour
    {
        [HideInInspector] public GameObject[] waypoints;
        private int currentWaypoint = 0;
        private float lastWaypointSwitchTime = 0;
        public UnitBase unitBase;
        [HideInInspector] public int health;
        [HideInInspector] public int armor;
        [HideInInspector] public int goldRewardOnDeath;
        Sprite sprite;
        SpriteRenderer _unitSprite;

        void Awake()
        {
            sprite = unitBase.unitSprite;
            _unitSprite = GetComponent<SpriteRenderer>();
            health = unitBase.health;
            armor = unitBase.armor;
            goldRewardOnDeath = unitBase.goldReward;
        }
        void Start()
        {
            _unitSprite.sprite = sprite;
            lastWaypointSwitchTime = Time.time;
        }

        void Update()
        {
            var startPos = waypoints[currentWaypoint].transform.position;
            var endPos = waypoints[currentWaypoint + 1].transform.position;

            var pathLength = Vector3.Distance(startPos, endPos);
            var totalTimeForPath = pathLength / unitBase.speed;
            var currentTimeOnPath = Time.time - lastWaypointSwitchTime;

            gameObject.transform.position = Vector2.Lerp(startPos, endPos, currentTimeOnPath / totalTimeForPath);

            if(gameObject.transform.position.Equals(endPos))
            {
                if(currentWaypoint < waypoints.Length - 2)
                {
                    currentWaypoint++;
                    lastWaypointSwitchTime = Time.time;
                    RotateIntoMoveDirection();
                }
                else
                {
                    if(unitBase.isBoss)
                    {
                        Destroy(gameObject);
                        PlayerManager.Instance.Health = 0;
                    }
                    else
                    {
                        Destroy(gameObject);
                        PlayerManager.Instance.Health--;
                    }
                }
            }
        }

        void RotateIntoMoveDirection()
        {
            var newStartPos = waypoints[currentWaypoint].transform.position;
            var newEndPos = waypoints[currentWaypoint + 1].transform.position;
            var newDirection = newEndPos - newStartPos;

            var x = newDirection.x;
            var y = newDirection.y;
            var rotationAngle = Mathf.Atan2(y, x) * 180 / Mathf.PI;

            transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
        }

        public float DistanceToGoal()
        {
            var distance = 0f;
            distance += Vector2.Distance(gameObject.transform.position, waypoints[currentWaypoint + 1].transform.position);
            for(var i = currentWaypoint; i < waypoints.Length - 1; i++)
            {
                var startPos = waypoints[i].transform.position;
                var endPos = waypoints[i + 1].transform.position;
                distance = Vector2.Distance(startPos, endPos);
            }
            return distance;
        }
    }
}
