using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    [Header("Sound Settings")]
    [SerializeField] private AudioClip sawSound;   // Tambahkan AudioClip untuk suara saw
    [SerializeField][Range(0, 1)] private float sawVolume = 0.5f; // Atur volume suara saw (default 50%)

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
        // Mainkan suara saw dengan volume yang disesuaikan
        if (sawSound != null)
        {
            AudioSource.PlayClipAtPoint(sawSound, transform.position, sawVolume);
        }
        else
        {
            Debug.LogWarning("Saw sound is not assigned!");
        }
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
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
