using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballSound;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // Cek apakah game sedang pause
        if (PauseManager.instance != null && PauseManager.instance.IsGamePaused())
        {
            return;
        }

        // Filter input hanya untuk Joystick 1
        if (IsJoystick1() && Input.GetButtonDown("Fire1") && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private bool IsJoystick1()
    {
        string[] joysticks = Input.GetJoystickNames();
        return joysticks.Length > 0 && joysticks[0] != null; // Pastikan Joystick 1 ada
    }

    private void Attack()
    {
        // Mainkan suara serangan
        SoundManager.instance.PlaySound(fireballSound);

        // Jalankan animasi serangan
        anim.SetTrigger("attack");

        // Reset cooldown timer
        cooldownTimer = 0;

        // Tentukan fireball yang akan digunakan
        GameObject fireball = fireballs[FindFireball()];

        // Tempatkan fireball di posisi firePoint
        fireball.transform.position = firePoint.position;

        // Tentukan arah berdasarkan orientasi karakter
        float direction = Mathf.Sign(transform.localScale.x);

        // Debugging arah karakter dan fireball
        Debug.Log($"Player Direction: {transform.localScale.x}");
        Debug.Log($"Fireball Direction: {direction}");

        // Set arah fireball
        fireball.GetComponent<Projectile>().SetDirection(direction);
    }



    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
