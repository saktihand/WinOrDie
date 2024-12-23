using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private int maxJumps = 2; // Maksimal jumlah lompatan

    [Header("Sounds")]
    [SerializeField] private AudioClip jumpSound;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    private int jumpCount; // Jumlah lompatan yang telah dilakukan

    private bool isGameOver = false;  // Menyimpan status game over

    private void Awake()
    {
        // Ambil referensi untuk rigidbody dan animator
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        // Cek apakah game sedang pause atau game over
        if (PauseManager.instance != null && PauseManager.instance.IsGamePaused() || isGameOver)
        {
            return; // Jika game sedang pause atau game over, abaikan input dan pergerakan
        }

        horizontalInput = Input.GetAxis("Horizontal");

        // Flip player saat bergerak kiri/kanan
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // Set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        // Wall jump logic
        if (wallJumpCooldown > 0.2f)
        {
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.linearVelocity = Vector2.zero;
            }
            else
                body.gravityScale = 7;

            // Lompat menggunakan Space (keyboard) atau Jump (joystick)
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
                Jump();
        }
        else
            wallJumpCooldown += Time.deltaTime;

        // Reset jump count ketika di tanah
        if (isGrounded())
            jumpCount = 0;
    }

    private void Jump()
    {
        if (isGrounded() || jumpCount < maxJumps)
        {
            SoundManager.instance.PlaySound(jumpSound);
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            anim.SetTrigger("jump");
            jumpCount++;
        }
        else if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

            wallJumpCooldown = 0;
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }

    // Fungsi untuk menetapkan status game over
    public void SetGameOver(bool status)
    {
        isGameOver = status;

        if (isGameOver)
        {
            // Setel kecepatan menjadi nol jika game over
            body.linearVelocity = Vector2.zero;
            anim.SetBool("run", false);  // Hentikan animasi pergerakan
        }
    }
}
