using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject zombiePrefab;

    [SerializeField]
    float spawnInterval = 5f;

    Enemy[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private void Update()
    {
        enemies = FindObjectsOfType<Enemy>();
    }

    IEnumerator SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(zombiePrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(spawnInterval);
        yield return new WaitUntil(() => enemies.Length < 25);

        StartCoroutine(SpawnEnemy());
    }

}
