using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform parent;
    [SerializeField] float timeDelayBetweenSpawns = 2f;
    [SerializeField] int enemyCount;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        for(int i = 0; i<enemyCount; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            newEnemy.transform.parent = parent;
            yield return new WaitForSecondsRealtime(timeDelayBetweenSpawns);
        }
        
    }
}
