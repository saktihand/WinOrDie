using UnityEngine;
using UnityEngine.UI;

public class SelectionMenu : MonoBehaviour
{
    [Header("Button Settings")]
    [SerializeField] private RectTransform[] buttons; // Semua tombol menu
    [SerializeField] private AudioClip changeSound; // Suara saat memilih
    [SerializeField] private AudioClip interactSound; // Suara saat menekan tombol pilihan
    private RectTransform arrow; // Panah pemilih
    private int currentPosition; // Indeks posisi tombol yang dipilih

    [Header("Input Settings")]
    private float inputCooldown = 0.2f; // Cooldown untuk input navigasi
    private float lastInputTime = 0f; // Waktu terakhir input navigasi

    private void Awake()
    {
        arrow = GetComponent<RectTransform>(); // Ambil referensi RectTransform untuk arrow
    }

    private void OnEnable()
    {
        currentPosition = 0; // Set posisi awal panah
        ChangePosition(0); // Pastikan panah berada di posisi awal
    }

    private void Update()
    {
        HandleNavigation(); // Navigasi menggunakan analog
        HandleInteraction(); // Pilihan tombol menggunakan O
    }

    private void HandleNavigation()
    {
        // Dapatkan input dari analog gamepad
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Navigasi ke atas
        if (verticalInput > 0.5f && Time.unscaledTime > lastInputTime + inputCooldown)
        {
            ChangePosition(1); // Navigasi ke bawah
            lastInputTime = Time.unscaledTime;
        }
        else if (verticalInput < -0.5f && Time.unscaledTime > lastInputTime + inputCooldown)
        {
            ChangePosition(-1); // Navigasi ke atas
            lastInputTime = Time.unscaledTime;
        }

    }

    private void HandleInteraction()
    {
        // Pilih dengan tombol O pada gamepad (Submit)
        if (Input.GetButtonDown("Submit"))
        {
            Interact();
        }
    }

    private void ChangePosition(int _change)
    {
        currentPosition += _change; // Update posisi berdasarkan input

        // Pastikan posisi tetap dalam batas array tombol
        if (currentPosition < 0)
            currentPosition = buttons.Length - 1; // Kembali ke tombol terakhir
        else if (currentPosition >= buttons.Length)
            currentPosition = 0; // Kembali ke tombol pertama

        AssignPosition(); // Update posisi panah

        // Mainkan suara saat berpindah (opsional)
        if (_change != 0 && SoundManager.instance != null)
        {
            SoundManager.instance.PlaySound(changeSound);
        }
    }

    private void AssignPosition()
    {
        // Validasi array tombol
        if (buttons == null || buttons.Length == 0)
        {
            Debug.LogError("Buttons array is not assigned or empty!");
            return;
        }

        // Pastikan indeks valid
        if (currentPosition < 0 || currentPosition >= buttons.Length)
        {
            Debug.LogError($"Invalid currentPosition: {currentPosition}. Must be between 0 and {buttons.Length - 1}");
            return;
        }

        // Geser panah ke posisi tombol yang dipilih
        arrow.position = new Vector3(arrow.position.x, buttons[currentPosition].position.y);

        // Debug posisi panah
        Debug.Log($"Arrow moved to button: {buttons[currentPosition].name}, Position: {currentPosition}");
    }

    private void Interact()
    {
        if (SoundManager.instance != null)
        {
            SoundManager.instance.PlaySound(interactSound); // Mainkan suara saat memilih
        }

        // Panggil fungsi tombol yang dipilih
        Button button = buttons[currentPosition].GetComponent<Button>();
        if (button != null)
        {
            button.onClick.Invoke(); // Eksekusi fungsi yang terhubung dengan tombol
        }
        else
        {
            Debug.LogWarning($"Button component not found on object: {buttons[currentPosition].name}");
        }
    }
}
