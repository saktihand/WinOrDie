using UnityEngine;

public class Player1MeleeAttack : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private int damage = 10;
    [SerializeField] private LayerMask enemyLayer;

    [Header("References")]
    [SerializeField] private Transform attackPoint;

    [Header("Attack Sound")]
    [SerializeField] private AudioClip attackSound;

    private Animator anim;
    private float attackTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (PauseManager.instance != null && PauseManager.instance.IsGamePaused()) return;
        if (GameManager.instance != null && GameManager.instance.IsGameOver()) return;

        attackTimer += Time.deltaTime;

        // Filter input hanya untuk Joystick 1
        if (IsJoystick1() && Input.GetButtonDown("Melee1") && attackTimer >= attackCooldown)
        {
            attackTimer = 0;
            anim.SetTrigger("2_Attack");
            Invoke("DamageEnemy", 0.3f);
            if (SoundManager.instance != null)
            {
                SoundManager.instance.PlaySound(attackSound);
            }
        }
    }

    private bool IsJoystick1()
    {
        string[] joysticks = Input.GetJoystickNames();
        return joysticks.Length > 0 && joysticks[0] != null; // Pastikan Joystick 1 ada
    }

    private void DamageEnemy()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(
            attackPoint.position,
            new Vector2(attackRange, attackRange),
            0, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(attackPoint.position, new Vector3(attackRange, attackRange, 1));
        }
    }
}
