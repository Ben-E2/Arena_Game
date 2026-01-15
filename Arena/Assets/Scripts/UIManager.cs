using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Game UI")]
    [SerializeField] private GameObject GameUI;

    [Space]
    [SerializeField] private TMP_Text WaveText_GameUI;
    [SerializeField] private TMP_Text ScoreText_GameUI;
    [SerializeField] private TMP_Text ActiveEnemiesText_GameUI;
    [SerializeField] private TMP_Text SpawnPointsText_GameUI;

    [Header("Game Over UI")]
    [SerializeField] private GameObject GameOverUI;

    [Space]
    [SerializeField] private TMP_Text WaveText_GameOverUI;
    [SerializeField] private TMP_Text ScoreText_GameOverUI;

    public void UpdateWaveText()
    {
        WaveText_GameUI.text = "Wave " + WaveManager.Instance.waveNumber.ToString();
    }

    public void UpdateScoreText()
    {
        ScoreText_GameUI.text = "Score: " + GameManager.Instance.score.ToString();
    }

    public void UpdateSpawnPointsText()
    {
        SpawnPointsText_GameUI.text = "SP: " + EnemySpawner.Instance.spawnPoints.ToString();
    }

    public void UpdateActiveEnemiesText()
    {
        ActiveEnemiesText_GameUI.text = "Enemies Remaining: " + EnemySpawner.Instance.activeEnemies.ToString();
    }

    private void UpdateGameOverUI()
    {
        WaveText_GameOverUI.text = "Wave " + WaveManager.Instance.waveNumber.ToString();
        ScoreText_GameOverUI.text = "Score: " + GameManager.Instance.score.ToString();
    }

    public void ToggleGameOverUI(bool toggle)
    {
        if (toggle) {
            UpdateGameOverUI();

            GameUI.SetActive(false);
            GameOverUI.SetActive(true);
        }
        else {
            GameUI.SetActive(true);
            GameOverUI.SetActive(false);
        }
        
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}
