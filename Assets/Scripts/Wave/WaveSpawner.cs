﻿using TowerDefense.Managers;
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
        public static int currentWave = 0;
        private bool firstWave = true;
        
        void Start()
        {
            lastSpawnTime = Time.time;
        }

        void Update()
        {
            if(currentWave < waveConfig.waveLimit)
            {
                var timeInterval = Time.time - lastSpawnTime;
                var spawnInterval = timeBetweenSpawns;
                if(((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) || timeInterval > spawnInterval) && enemiesSpawned < waveConfig.unitCount)
                {
                    firstWave = false;
                    lastSpawnTime = Time.time;
                    var newEnemy = Instantiate(waveConfig.waveUnits[Random.Range(0, waveConfig.waveUnits.Length)]) as GameObject;
                    newEnemy.GetComponent<UnitMovement>().waypoints = waypoints;
                    enemiesSpawned++;
                }
                if(firstWave)
                {
                    currentWave = 1;
                    AudioManager.Instance.WaveStartSound(waypoints[0].transform.position);
                }
                if(enemiesSpawned == waveConfig.unitCount && GameObject.FindGameObjectWithTag("Enemy") == null)
                {
                    currentWave++;
                    PlayerManager.Instance.Gold += 125;
                    enemiesSpawned = 0;
                    lastSpawnTime = Time.time;
                    if(waveConfig.bossWave && GameManager.Instance.GameOver)
                    {
                        GameManager.Instance.LevelComplete = false;
                    }
                    else
                    {
                        GameManager.Instance.LevelComplete = true;
                    }
                }
            }
        }
    }
}
