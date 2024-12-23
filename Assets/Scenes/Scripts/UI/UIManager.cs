using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
    }

    #region Game Over Functions
    // Game over function
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    // Restart level
    public void Restart()
    {
        // Reset Time.timeScale to ensure game resumes correctly
        Time.timeScale = 1f;

        // Reset game state in GameManager
        if (GameManager.instance != null)
        {
            ResetGameState();
        }

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ResetGameState()
    {
        GameManager.instance.ResetGameOver();
    }

    // Activate game over screen
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Quit game/exit play mode if in Editor
    public void Quit()
    {
        Application.Quit(); // Quits the game (only works in build)

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Exits play mode
#endif
    }
    #endregion
}
