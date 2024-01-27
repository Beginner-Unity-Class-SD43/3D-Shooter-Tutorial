using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject zombiePrefab;

    [SerializeField]
    float spawnInterval = 5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(zombiePrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(spawnInterval);
        StartCoroutine(SpawnEnemy());
    }

}
