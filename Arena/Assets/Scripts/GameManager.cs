using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Pooling")]
    public List<GameObject> bulletPool = new List<GameObject>();

    [Header("Prefabs")]
    [SerializeField] private GameObject bulletPrefab;

    [Header("Variables")]
    [SerializeField] public int score {get; private set;} = 0;
    [SerializeField] public GameState gameState {get; private set;} = GameState.Active;

    public enum GameState
    {
        Active,
        Paused,
        GameOver
    }

    public void ChangeGameState(GameState state)
    {
        if (gameState == state)
        {
            Debug.LogWarning($"Couldn't change gameState. Already in state: {gameState}");
            return;
        }

        Debug.Log($"GameState changed. {gameState} -> {state}");
        gameState = state;

        if (gameState == GameState.GameOver) OnGameOverState();
    }

    private void OnGameOverState()
    {
        UIManager.Instance.ToggleGameOverUI(true);
    }

    private void OnPauseState()
    {
        Debug.LogWarning("Not implemented");
    }

    private void OnActiveState()
    {
        UIManager.Instance.ToggleGameOverUI(false);
    }

    private void CreateBulletPool()
    {
        bulletPool.Clear();

        for (int i = 0; i < 50; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);

            bulletPool.Add(bullet);

            bullet.SetActive(false);
        }
    }

    public GameObject AddBulletToPool()
    {
        GameObject bullet = Instantiate(bulletPrefab);

        bulletPool.Add(bullet);

        bullet.SetActive(false);

        return bullet;
    }

    public GameObject GetObjectFromPool(List<GameObject> pool)
    {
        for (int i = 0; i < pool.Count; i++)
        {
            GameObject obj = pool[i];

            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        return AddBulletToPool();
    }

    public void IncrementScore()
    {
        score++;

        UIManager.Instance.UpdateScoreText();
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    private void Start()
    {
        CreateBulletPool();
    }

    private void Awake()
    {
        gameState = GameState.Active;
        OnActiveState();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}
