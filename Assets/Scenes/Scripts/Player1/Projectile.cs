using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;

    private Animator anim;
    private BoxCollider2D boxCollider;

    [SerializeField] private float damage = 10f; // Tambahkan damage jika perlu

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;

        // Menggerakkan projectile
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        // Menghitung lifetime dan menonaktifkan projectile setelah 5 detik
        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Cek apakah projectile mengenai Player1 atau Player2
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Kurangi health pemain yang tertabrak
            }
        }

        // Setelah terkena, nonaktifkan collider dan mainkan animasi ledakan
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");
    }

    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        // Menyesuaikan arah (flip) sesuai dengan arah yang diberikan
        float localScaleX = Mathf.Abs(transform.localScale.x) * Mathf.Sign(_direction);
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);

        // Debugging arah dan scale fireball
        Debug.Log($"Fireball Moving Direction: {direction}");
        Debug.Log($"Fireball Scale: {transform.localScale.x}");
    }



    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
