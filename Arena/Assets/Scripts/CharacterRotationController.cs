using UnityEngine;

public class CharacterRotationController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform playerTransform;

    [Header("Variables")]
    [SerializeField] private CharacterSettings settings;

    private Vector3 GetCursorToWorldPosition()
    {
        Vector3 cursorPosition = Input.mousePosition;

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(cursorPosition.x, cursorPosition.y, Camera.main.transform.position.y));
        worldPosition = new Vector3(worldPosition.x, playerTransform.position.y, worldPosition.z);

        return worldPosition;
    }

    private void RotateCharacter(Vector3 targetPosition)
    {
        targetPosition = new Vector3(targetPosition.x, gameObject.transform.position.y, targetPosition.z);
        gameObject.transform.LookAt(targetPosition);
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
        if (!playerTransform || GameManager.Instance.gameState != GameManager.GameState.Active) return;

        Vector3 target;
        if (settings.isPlayer) { target = GetCursorToWorldPosition(); } else { target = playerTransform.position; }

        RotateCharacter(target);
    }
}
