using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform parent; //для красивой иерархии в инспекторе
    [SerializeField] float timeDelayBetweenSpawns = 2f;
    [SerializeField] int enemyCount; // устанавливаем кол-во врагов на уровне через инспектор!
    [SerializeField] AudioClip spawnSound;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        for(int i = 0; i<enemyCount; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            newEnemy.transform.parent = parent; //засунуть в инспекторе в "подпапку"
            GetComponent<AudioSource>().PlayOneShot(spawnSound);
            yield return new WaitForSecondsRealtime(timeDelayBetweenSpawns);
        }
        
    }
}
