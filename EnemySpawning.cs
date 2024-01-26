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
        for(i = usedDifficulty; i > 0; i -= usedDifficulty){
            int currentChecked = difficulty / i;
            if(usedDifficulty > currentChecked && Random.Range(1,3) == 2){
                waveCode.Add(currentChecked);
            }else{
                i += usedDifficulty;
            }
        }
    }

    //Every enemy in the Enemies[] array has an index.
    //Place them into the array from least dangerous(1) to most (n)
    //Every wave spawn, make the waveCode<List> have the full code of the wave,
    //only then handle spawning.
    //
    //Wave writing script:
    //Check maximum enemy difficulty we can spawn
    //If we can spawn, check a 50% chance randomizer, if we should spawn.
    //Then substract the enemy value from usedDifficulty
    //Go to another enemy value (for loop)
    //
    //If at the end we didnt use all difficulty, run loop again.
    //waveCode list should be ready.

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