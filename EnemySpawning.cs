using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public float enemySpawnInterval;
    public GameObject[] enemies;
    public int enemyAmmount;

    public int difficulty;
    public int waveNumber;

    public List<int> waveCode = new List<int>();

    public void StartWaveGen2(){
        int usedDiff = difficulty;
        for(i = 0; i < usedDiff; i += )
    }


    public void StartWave(){
        int usedDifficulty;
        //Here manage whole wave stats
        for(i = difficulty; i > 0; i -= ){
            int currentIndex = difficulty / enemies.Length;
            if(usedDifficulty > currentIndex && Random.Range(1,2) == 2){
                waveCode.Add(currentIndex);
            }else{
                
            }
        }
    }

    IEnumerator SendWave(){
        //Here wait in beetween enemy spawns
    }

    public void SpawnEnemy(string enemyType, Vector3 spawnPosition){
        //Here manage spawning singular enemies
    }
}