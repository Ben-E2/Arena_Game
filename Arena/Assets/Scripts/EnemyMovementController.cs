using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform playerTransform;
    public Vector3 predictedPosition;

    [Header("Variables")]
    [SerializeField] private CharacterSettings settings;

    private float GetDistanceToPlayer()
    {
        if (!playerTransform) return -1f;

        return Vector3.Distance(transform.position, playerTransform.position);
    }

    private Vector3 GetDirectionToPlayer()
    {
        if (!playerTransform) return Vector3.zero;

        return (playerTransform.position - transform.position).normalized;
    }

    private void GetPredictedPosition()
    {
        if (!playerTransform) return;

        float travelTime = GetDistanceToPlayer() / 30f; // 30 is the bullet speed
        predictedPosition = playerTransform.position + (playerTransform.GetComponent<Rigidbody>().linearVelocity * travelTime);

        travelTime = Vector3.Distance(predictedPosition, transform.position) / 30f;
        predictedPosition = playerTransform.position + (playerTransform.GetComponent<Rigidbody>().linearVelocity * travelTime);
    }

    private void Start()
    {
        if (!playerTransform)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
                playerTransform = player.transform;
            else return;
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.gameState != GameManager.GameState.Active){
            rb.linearVelocity = Vector3.zero;

            return;
        }

        if (!playerTransform) return;

        float distance = GetDistanceToPlayer();

        Vector3 direction = GetDirectionToPlayer();
        direction = new Vector3(direction.x, 0f, direction.z);

        GetPredictedPosition();

        if (!settings.isRanged) {
            rb.linearVelocity = direction * settings.movementSpeed;

            return;
        }

        if (distance < settings.minDistanceFromPlayer) { rb.linearVelocity = -direction * settings.movementSpeed; }

        else if (distance > settings.maxDistanceFromPlayer){ rb.linearVelocity = direction * settings.movementSpeed; }

        else { rb.linearVelocity = Vector3.zero; }
    }
}
