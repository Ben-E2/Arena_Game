using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    [Space]
    [SerializeField] private float cooldown;
    private bool debounce;
    private float lastShotTime;

    [Space]
    [SerializeField] private CharacterSettings characterSettings;

    [Space]
    [SerializeField] private Transform playerTransform;

    private void Shoot(Vector3 direction)
    {
        if (debounce) return;

        lastShotTime = Time.time;
        debounce = true;

        GameObject bullet = GameManager.Instance.GetObjectFromPool(GameManager.Instance.bulletPool);

        bullet.transform.position = transform.position;
        bullet.GetComponent<Rigidbody>().position = transform.position;

        bullet.GetComponent<Bullet>().owner = transform;
        bullet.GetComponent<Bullet>().direction = direction;

        bullet.SetActive(true);
    }

    private Vector3 GetCursorToWorldPosition()
    {
        Vector3 cursorPosition = Input.mousePosition;

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(cursorPosition.x, cursorPosition.y, Camera.main.transform.position.y));
        worldPosition = new Vector3(worldPosition.x, gameObject.transform.position.y, worldPosition.z);

        return worldPosition;
    }

    private Vector3 GetBulletDirection(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - gameObject.transform.position).normalized;

        return direction;
    }

    private bool HandleCooldown()
    {
        if (debounce)
        {
            if (Time.time - lastShotTime > cooldown)
            {
                debounce = false;
                return true;
            }
            return false;
        }
        return true;
    }

    private bool CanShoot()
    {
        if (!playerTransform) return false;

        return true;
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

    private void Update()
    {
        if (GameManager.Instance.gameState != GameManager.GameState.Active) return;

        if (!HandleCooldown() && !CanShoot()) return;

        if (characterSettings.isPlayer && Input.GetMouseButton(0)) {
            Shoot(GetBulletDirection(GetCursorToWorldPosition()));

            return;
        }

        if (!characterSettings.isPlayer && characterSettings.isRanged) {
            if (!characterSettings.isPredictive) {
                Shoot(GetBulletDirection(playerTransform.position));
            }
            else {
                Shoot(GetBulletDirection(gameObject.GetComponent<EnemyMovementController>().predictedPosition));
            }

            return;
        }
    }
}
