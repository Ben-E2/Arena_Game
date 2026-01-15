using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [SerializeField] public bool waveActive = false;
    [SerializeField] private float roundCooldown = 2f;

    public int waveNumber {get; private set;} = 0;
    [SerializeField] private int startSpawnPoints = 5;

    private void StartNewWave()
    {
        if (waveActive) return;

        waveActive = true;

        EnemySpawner.Instance.spawnPoints = startSpawnPoints + (waveNumber * 5);
        EnemySpawner.Instance.canSpawn = true;

        waveNumber++;

        UIManager.Instance.UpdateWaveText();
    }

    IEnumerator BetweenRoundCooldown()
    {
        yield return new WaitForSeconds(roundCooldown);
        StartNewWave();
    }

    private void Start()
    {
        StartCoroutine(BetweenRoundCooldown());
    }

    private void Update()
    {
        if (GameManager.Instance.gameState != GameManager.GameState.Active) return;

        if (waveActive && EnemySpawner.Instance.spawnPoints <= 0 && EnemySpawner.Instance.activeEnemies <= 0)
        {
            waveActive = false;

            StartCoroutine(BetweenRoundCooldown());
        }
    }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}
