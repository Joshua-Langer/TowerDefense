using System.Collections;
using System.Collections.Generic;
using TowerDefense.Managers;
using TowerDefense.Units;
using UnityEngine;

namespace TowerDefense.Waves{
    public class WaveSpawner : MonoBehaviour
    {
        public WaveBase waveConfig;
        public float timeBetweenSpawns = 0f;
        public float timeBetweenWaves = 5f;
        public GameObject[] waypoints;

        private float lastSpawnTime = 0f;
        private int enemiesSpawned = 0;
        
        void Start()
        {
            lastSpawnTime = Time.time;
        }

        void Update()
        {
            var currentWave = 0;
            if(currentWave < waveConfig.waveLimit)
            {
                var timeInterval = Time.time - lastSpawnTime;
                var spawnInterval = timeBetweenSpawns;
                
                if(((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) || timeInterval > spawnInterval) && enemiesSpawned < waveConfig.unitCount)
                {
                    lastSpawnTime = Time.time;
                    var newEnemy = Instantiate(waveConfig.waveUnits[Random.Range(0, waveConfig.waveUnits.Length)]) as GameObject;
                    newEnemy.GetComponent<UnitMovement>().waypoints = waypoints;
                    enemiesSpawned++;
                }
                if(enemiesSpawned == waveConfig.unitCount && GameObject.FindGameObjectWithTag("Enemy") == null)
                {
                    GameManager.Instance.Wave++;
                    //Add gold to Player Manager
                    enemiesSpawned = 0;
                    lastSpawnTime = Time.time;
                }
            }
            else
            {
                GameManager.Instance.GameOver = true;
                Debug.Log("Player Wins");
            }
        }



    }
}
