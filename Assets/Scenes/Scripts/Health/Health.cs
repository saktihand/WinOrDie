using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; } // Exposed for GameTimer
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        Debug.Log($"{gameObject.name} took {_damage} damage.");
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            // Player masih hidup, trigger animasi hurt
            anim.SetTrigger("3_Damaged");

            // Mainkan efek suara jika ada
            if (SoundManager.instance != null)
            {
                SoundManager.instance.PlaySound(hurtSound);
            }

            // Aktifkan invulnerability (iFrames)
            StartCoroutine(Invulnerability());
        }
        else
        {
            if (!dead)
            {
                dead = true;
                anim.SetTrigger("4_Death");

                // Nonaktifkan pergerakan player
                if (GetComponent<PlayerMovement>() != null)
                {
                    GetComponent<PlayerMovement>().enabled = false;
                }

                // Beri tahu GameManager jika pemain mati
                GameManager.instance.PlayerDied(gameObject.tag);
            }
        }
    }

    public void AddHealth(float _value)
    {
        // Menambah health dengan batas maksimum
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true); // Sesuaikan dengan layer Anda
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f); // Setengah transparan merah
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white; // Kembali ke warna asli
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }

    // Check if player is still alive
    public bool IsAlive()
    {
        return currentHealth > 0;
    }
}
