using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool isPaused = false;

    public static PauseManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
    }

    private void Update()
    {
        // Jika game over, tidak perlu cek pause
        if (GameManager.instance.IsGameOver())
            return;

        // Deteksi tombol keyboard Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        // Deteksi tombol Start (joystick button 7)
        if (Input.GetButtonDown("Pause"))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }
    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
    }

    public bool IsGamePaused()
    {
        return isPaused;
    }
}
