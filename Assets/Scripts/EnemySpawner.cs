using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnDelay;
    public float spawnDelayDecrease;

    public GameObject enemyPrefab;

    public Player player;

    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= spawnDelay)
        {
            currentTime = 0;
            if (spawnDelay > spawnDelayDecrease + 0.2f)
            {
                spawnDelay -= spawnDelayDecrease;
            }
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        float x = Random.Range(-50, 50);
        Vector3 offset = new Vector3(x, 0, 100);
        Instantiate(enemyPrefab, offset, Quaternion.Euler(0,180,0)).GetComponent<Enemy>().player = player;
    }
}
