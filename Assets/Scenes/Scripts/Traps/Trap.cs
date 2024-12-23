using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private Transform respawnPoint; // Tambahkan respawn point sebagai referensi

    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    private void Start()
    {
       
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingLeft = false;
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingLeft = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Memeriksa apakah objek yang terkena adalah Player
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            // Mengurangi health Player
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // Respawn Player ke posisi respawn point
            if (respawnPoint != null)
            {
                collision.transform.position = respawnPoint.position;
                Debug.Log($"{collision.name} respawned at {respawnPoint.position}");
            }
            else
            {
                Debug.LogWarning("Respawn point is not assigned!");
            }
        }
    }
}
