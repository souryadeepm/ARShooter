using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[RequireComponent(typeof(AudioSource))]
public class WaveManager : MonoBehaviour
{
    public List<EnemyWave> enemyWaves = new List<EnemyWave>();
    public List<GameObject> spawnedEnemies = new List<GameObject>();

    private int enemiesKilledThisWave;


    public event Action<EnemyWave> OnWaveChanged;

    public int CurrentWave { get; set; }

    public void SpawnWave(int index)
    {

        if (index >= enemyWaves.Count)
            return;


        if(OnWaveChanged!=null)
        {
            OnWaveChanged.Invoke(enemyWaves[index]);
        }
        StartCoroutine(SpawnWaveRoutine(index));
    }

    private IEnumerator SpawnWaveRoutine(int index)
    {
        Debug.Log("Wave" + index.ToString() + "spawning ..." + enemyWaves[index].waveName);

        foreach (var enemy in enemyWaves[index].enemies)
        {
            var currentEnemy = GameObject.Instantiate(enemy, transform);
            spawnedEnemies.Add(currentEnemy);

            currentEnemy.GetComponent<Health>().OnDeath += Enemydied;

            yield return new WaitForSeconds(enemyWaves[index].enemySpawnTime);
        }
        
    }

    private void Enemydied()
    {
        enemiesKilledThisWave++;
        GetComponent<AudioSource>().Play();
        if(enemiesKilledThisWave == enemyWaves[CurrentWave].enemies.Count)
        {
            if(CurrentWave == enemyWaves.Count-1)
            {
                Debug.Log("Game Over, you WON!");               
                GameManager.instance.StartGameOver();

            }
            else
            {
                enemiesKilledThisWave = 0;
                SpawnWave(++CurrentWave);
            }
        }
    }

    public void CleanUpEnemies()
    {
        CurrentWave = 0;
        foreach (var enemy in spawnedEnemies)
        {
            Destroy(enemy);
        }
        {

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            SpawnWave(CurrentWave++);   
    }

}
