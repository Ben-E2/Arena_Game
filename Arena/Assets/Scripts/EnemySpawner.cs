using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    public int spawnPoints = 10;
    [SerializeField] private float spawnCooldown = 1f;

    public bool canSpawn = false;

    public int activeEnemies = 0;

    [SerializeField] private Bounds spawnBounds = new Bounds();

    [SerializeField] private List<CharacterSettings> EnemyTypes;

    private void SpawnEnemy()
    {
        if (spawnPoints <= 0) return;

        canSpawn = false;

        Vector3 spawnPosition = GetRandomSpawnPoint();
        CharacterSettings enemyData = ChooseRandomEnemy();

        GameObject newEnemy = Instantiate(enemyData.Prefab);
        newEnemy.transform.position = newEnemy.GetComponent<Rigidbody>().position = spawnPosition;

        activeEnemies++;
        spawnPoints -= enemyData.spawnCost;

        UIManager.Instance.UpdateSpawnPointsText();
        UIManager.Instance.UpdateActiveEnemiesText();

        StartCoroutine(SpawnCooldown());
    }

    private Vector3 GetRandomSpawnPoint()
    {
        return Vector3.zero;
    }

    private CharacterSettings ChooseRandomEnemy()
    {
        int loopCount = 0;
        while (loopCount < 100)
        {
            loopCount++;

            CharacterSettings randomEnemy = EnemyTypes[Random.Range(0, EnemyTypes.Count)];

            if (randomEnemy.spawnCost > spawnPoints) continue;

            return randomEnemy;
        }

        Debug.LogWarning("Enemy couldn't be chosen.");
        return null;
    }

    IEnumerator SpawnCooldown()
    {
        yield return new WaitForSeconds(spawnCooldown);
        canSpawn = true;
    }

    private void Update()
    {
        if (canSpawn && GameManager.Instance.gameState == GameManager.GameState.Active) SpawnEnemy();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            
            return;
        }

        Instance = this;
    }
}
