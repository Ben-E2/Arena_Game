using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField] private CharacterSettings settings;

    [SerializeField] private float health;

    public void TakeDamage(float damage)
    {
        if (health - damage <= 0f) Die();
            
        else
            health -= damage;
    }

    private void Die()
    {
        if (settings.team != "Player")
        {
            EnemySpawner.Instance.activeEnemies -= 1;

            GameManager.Instance.IncrementScore();

            UIManager.Instance.UpdateActiveEnemiesText();
            UIManager.Instance.UpdateScoreText();
        }
        else GameManager.Instance.ChangeGameState(GameManager.GameState.GameOver);        

        gameObject.SetActive(false);
    }

    private void Awake()
    {
        health = settings.maxHealth;
    }
}
