using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody rb;

    [Space]
    [SerializeField] private CharacterSettings settings;

    [Header("Variables")]
    [SerializeField] private Vector3 movement;

    private void GetMovement() {
        movement = new Vector3(Input.GetAxis("Horizontal") * settings.movementSpeed, 0, Input.GetAxis("Vertical") * settings.movementSpeed);
    }

    private void Update()
    {
        if (GameManager.Instance.gameState != GameManager.GameState.Active)
        {
            rb.linearVelocity = Vector3.zero;

            return;
        }
        
        GetMovement();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.gameState != GameManager.GameState.Active) return;

        rb.linearVelocity = movement;
    }
}
