using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int poolSize;
    [SerializeField] private float spawnInterval;
    [SerializeField] private GridSystem gridSystem;
    [SerializeField] private int waveSize; 

    private List<GameObject> enemyPool;
    private float spawnTimer;
    private int enemiesSpawnedInWave; 

    void Start()
    {
        // create the pool and fill it with enemy objects
        enemyPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            enemyPool.Add(enemy);
        }
    }

    void Update()
    {
        // increment the spawn timer
        spawnTimer += Time.deltaTime;

        // if enough time has passed and we haven't spawned a full wave yet, spawn enemies
        if (spawnTimer >= spawnInterval && enemiesSpawnedInWave < waveSize)
        {
            SpawnEnemy();
            enemiesSpawnedInWave++;
            spawnTimer = 0;
        }

        // if we've spawned a full wave, reset the wave counter
        if (enemiesSpawnedInWave == waveSize)
        {
            enemiesSpawnedInWave = 0;
        }
    }

    private void SpawnEnemy()
    {
        // find waveSize inactive enemy objects in the pool
        List<GameObject> waveEnemies = new List<GameObject>();
        for (int i = 0; i < waveSize; i++)
        {
            GameObject enemy = null;
            foreach (GameObject obj in enemyPool)
            {
                if (!obj.activeInHierarchy)
                {
                    enemy = obj;
                    break;
                }
            }

            // if no inactive enemy was found, return
            if (enemy == null)
            {
                return;
            }

            waveEnemies.Add(enemy);
        }

        // find unoccupied cells in the grid system and assign them to the enemies
        List<Vector2Int> unoccupiedCells = new List<Vector2Int>();
        for (int x = 0; x < gridSystem.GridSize; x++)
        {
            for (int z = 0; z < gridSystem.GridSize; z++)
            {
                Vector2Int cellIndex = new Vector2Int(x, z);
                Vector2 cellPosition = gridSystem.GetCellPosition(cellIndex.x, cellIndex.y);
                Collider2D[] colliders = Physics2D.OverlapCircleAll(cellPosition, 0.5f);
                if (colliders.Length == 0)
                {
                    unoccupiedCells.Add(cellIndex);
                }
            }
        }

        // shuffle the list of unoccupied cells
        for (int i = 0; i < unoccupiedCells.Count; i++)
        {
            Vector2Int temp = unoccupiedCells[i];
            int randomIndex = Random.Range(i, unoccupiedCells.Count);
            unoccupiedCells[i] = unoccupiedCells[randomIndex];
            unoccupiedCells[randomIndex] = temp;
        }

        // assign unoccupied cells to the wave enemies
        for (int i = 0; i < waveEnemies.Count; i++)
        {
            if (i >= unoccupiedCells.Count)
            {
                break;
            }

            Vector2Int cellIndex = unoccupiedCells[i];
            Vector2 cellCenter = gridSystem.GetCellPosition(cellIndex.x, cellIndex.y);
            waveEnemies[i].transform.position = cellCenter;
            waveEnemies[i].SetActive(true);
        }
    }
    //[SerializeField] private GameObject enemyPrefab;
    //[SerializeField] private int poolSize;
    //[SerializeField] private float spawnInterval;
    //[SerializeField] private GridSystem gridSystem;

    //private List<GameObject> enemyPool;
    //private float spawnTimer;

    //void Start()
    //{
    //    // create the pool and fill it with enemy objects
    //    enemyPool = new List<GameObject>();
    //    for (int i = 0; i < poolSize; i++)
    //    {
    //        GameObject enemy = Instantiate(enemyPrefab);
    //        enemy.SetActive(false);
    //        enemyPool.Add(enemy);
    //    }
    //}

    //void Update()
    //{
    //    // increment the spawn timer
    //    spawnTimer += Time.deltaTime;

    //    // if enough time has passed, spawn an enemy
    //    if (spawnTimer >= spawnInterval)
    //    {
    //        SpawnEnemy();
    //        spawnTimer = 0;
    //    }
    //}

    //private void SpawnEnemy()
    //{
    //    // find an inactive enemy object in the pool
    //    GameObject enemy = null;
    //    foreach (GameObject obj in enemyPool)
    //    {
    //        if (!obj.activeInHierarchy)
    //        {
    //            enemy = obj;
    //            break;
    //        }
    //    }

    //    // if no inactive enemy was found, return
    //    if (enemy == null)
    //    {
    //        return;
    //    }

    //    // find an unoccupied cell in the grid system
    //    Vector2Int cellIndex = Vector2Int.zero;
    //    bool cellFound = false;
    //    while (!cellFound)
    //    {
    //        cellIndex = new Vector2Int(Random.Range(0, gridSystem.gridSize), Random.Range(0, gridSystem.gridSize));
    //        Vector3 cellPosition = gridSystem.GetCellPosition(cellIndex.x, cellIndex.y);
    //        Collider[] colliders = Physics.OverlapSphere(cellPosition, 0.5f);
    //        cellFound = colliders.Length == 0;
    //    }

    //    // set the enemy's position to the center of the cell and activate it
    //    Vector3 cellCenter = gridSystem.GetCellPosition(cellIndex.x, cellIndex.y);
    //    enemy.transform.position = cellCenter;
    //    enemy.SetActive(true);
    //}

}
