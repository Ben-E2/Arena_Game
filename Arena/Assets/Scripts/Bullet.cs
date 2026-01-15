using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private float speed = 30f;

    [Space]
    [SerializeField] private Rigidbody rb;

    
    [Space]
    [SerializeField] private float maxLifeTime;
    private float startTime;

    [Space]
    public Transform owner;
    public Vector3 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        startTime = Time.time;

        rb.linearVelocity = direction * speed;
    }

    private void Update()
    {
        if (Time.time - startTime >= maxLifeTime) gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" && other.tag != "Enemy") return;

        if (other.tag == owner.tag) return;

        other.gameObject.GetComponent<CharacterStatus>().TakeDamage(damage);

        gameObject.SetActive(false);
    }
}
