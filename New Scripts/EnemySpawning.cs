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

    private void Start()
    {
        StartWaveGen3();
    }

    public void StartWaveGen3()
    {
        int usedDiff = difficulty;
        while(usedDiff > 0)
        {
            for (int i = usedDiff; i >= 0; i--)
            {
                if (usedDiff >= i)
                {
                    if (Random.Range(1, 15) == 2)
                    {
                        waveCode.Add(i);
                        usedDiff -= i;
                    }
                }
            }
        }
        Debug.Log("Start Difficulty: " + difficulty.ToString());
        Debug.Log("Used Difficulty: " + usedDiff.ToString());

        foreach (int item in waveCode)
        {
            Debug.Log(item);
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

    /* public void StartWave(){
        int usedDifficulty;
        //Here manage whole wave stats
        for(i = difficulty; i > 0; i -= ){
            int currentIndex = difficulty / enemies.Length;
            if(usedDifficulty > currentIndex && Random.Range(1,2) == 2){
                waveCode.Add(currentIndex);
            }else{
                
            }
        }
    } */

    IEnumerator SendWave(){
        //Here wait in beetween enemy spawns
        yield return new WaitForSeconds(1f);
    }

    public void SpawnEnemy(string enemyType, Vector3 spawnPosition){
        //Here manage spawning singular enemies
    }
}