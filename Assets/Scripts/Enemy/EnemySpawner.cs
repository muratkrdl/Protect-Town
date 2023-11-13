using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy[] enemyPrefabs;

    [SerializeField] float minSpawnRate;
    [SerializeField] float maxSpawnRate;

    [SerializeField] Transform startPos;
    [SerializeField] Transform enemiesParent;

    [SerializeField] WaveController waveController;

    float currentSpawnRate;
    List<Enemy> currentEnemies = new List<Enemy>();

    public void FirstWave()
    {
        for (int i = 0; i < 4; i++)
        {
            var enemy = Instantiate(
            enemyPrefabs[0],
            transform.position,
            Quaternion.identity,
            enemiesParent
            );
            currentEnemies.Add(enemy);
        }
        StartWave();
    }

    IEnumerator SpawnEnemies()
    {
        foreach (Enemy enemy in currentEnemies)
        {
            currentSpawnRate = Random.Range(minSpawnRate,maxSpawnRate + .01f);
            enemy.gameObject.SetActive(true);
            enemy.enabled = true;
            enemy.GetComponent<Collider2D>();
            enemy.transform.position = startPos.position;
            enemy.GetComponent<EnemyHealth>().SetHitPoints();
            enemy.Move();
            yield return new WaitForSeconds(currentSpawnRate);
        }
        waveController.StartCheckWaveClearedCoroutine();
        StopCoroutine(SpawnEnemies());
    }

    public void StartWave()
    {
        StartCoroutine(SpawnEnemies());
    }

    public bool CheckWaveCleared()
    {
        foreach (Enemy enemy in currentEnemies)
        {
            if(enemy.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }

    public void AddEnemy(int howMany)
    {
        Debug.Log(howMany);
        int listLength = 0;
        if(howMany/2 <= 2) { listLength = 2; }
        else if(howMany/2 <= 4) { listLength = 3; }
        else if(howMany/2 <= 6) { listLength = 4; }
        else if(howMany/2 <= 8) { listLength = 5; }
        else if(howMany/2 <= 10) { listLength = 6; }

        if(howMany > 10) { listLength = enemyPrefabs.Length; }

        for (int i = 0; i < howMany; i++)
        {
            var enemy = Instantiate(
            enemyPrefabs[Random.Range(0,listLength)],
            transform.position,
            Quaternion.identity,
            enemiesParent
            );
            currentEnemies.Add(enemy);
        }
        foreach (Enemy enemy in currentEnemies)
        {
            enemy.transform.position = startPos.position;
        }
    }

}
