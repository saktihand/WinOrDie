using UnityEngine;

public class Projectile2 : MonoBehaviour
{
    [SerializeField] private float speed; // Kecepatan peluru
    private float direction;             // Arah peluru (1 atau -1)
    private bool hit;                    // Apakah peluru sudah mengenai sesuatu
    private float lifetime;              // Umur peluru untuk otomatis non-aktif

    private Animator anim;
    private BoxCollider2D boxCollider;

    [Header("Owner Info")]
    [SerializeField] private string ownerTag; // Tag untuk menentukan pemilik peluru (Player1, Player2, dll.)

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;

        // Gerakan peluru
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        // Batasi lifetime peluru
        lifetime += Time.deltaTime;
        if (lifetime > 5)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Jangan menyerang pemilik peluru
        if (collision.CompareTag(ownerTag)) return;

        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");
    }

    public void SetDirection(float _direction, string _ownerTag)
    {
        // Reset state peluru
        lifetime = 0;
        direction = _direction;
        ownerTag = _ownerTag;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        // Atur arah sprite berdasarkan arah peluru
        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    // Method ini dipanggil di animasi untuk menonaktifkan peluru setelah animasi selesai
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
