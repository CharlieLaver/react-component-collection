using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
   public int waveNumber = 0;
   public int enemySpawnAmount = 0;
   public int enemiesKilled = 0;

   public GameObject[] spawners;
   public GameObject enemy;

   private void Start()
   {
       spawners = new GameObject[2];

       for(int i = 0; i < spawners.Length; i++)
       {
          spawners[i] = transform.GetChild(i).gameObject; 
       }

       StartWave();
   }

   private void Update()
   {
      
       if (enemiesKilled >= enemySpawnAmount)
       {
           NextWave();
       }
   }

   private void SpawnEnemy()
   {
       int spawnerID = Random.Range(0, spawners.Length);
      Instantiate(enemy, spawners[spawnerID].transform.position, spawners[spawnerID].transform.rotation);
   }

   private void StartWave()
   {
      waveNumber = 1;
      enemySpawnAmount = enemySpawnAmount;
      enemiesKilled = 0;

      for (int i = 0; i < enemySpawnAmount; i++)
      {
          SpawnEnemy();
      }

   }

   public void NextWave()
   {
       waveNumber++;
       enemySpawnAmount += 5;
       enemiesKilled = 0;

       for (int i = 0; i < enemySpawnAmount; i++)
       {
           SpawnEnemy();
       }
   }

   public void IncEnemiesKilled()
   {
       Debug.Log("IncEnemiesKilled");
       enemiesKilled++;
   }

 
}
