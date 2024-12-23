using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel; // Panel untuk Settings
    [SerializeField] private GameObject menuPanel; // Panel untuk Menu Utama

    private void Start()
    {
        // Pastikan hanya MenuPanel yang aktif saat memulai
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false); // Sembunyikan SettingsPanel
        }

        if (menuPanel != null)
        {
            menuPanel.SetActive(true); // Tampilkan MenuPanel
        }
    }

    public void StartGame()
    {
        StopAllAudio(); // Hentikan semua audio
        Time.timeScale = 1f;
        SceneManager.LoadScene("SelectArena");
    }

    public void OpenTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void CloseTutorial()
    {
        // Tampilkan MenuPanel dan sembunyikan SettingsPanel
        SceneManager.LoadScene("MainMenu");
    }
    public void OpenSettings()
    {
        // Sembunyikan MenuPanel dan tampilkan SettingsPanel
        if (menuPanel != null)
        {
            menuPanel.SetActive(false);
        }

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }
    }


    public void CloseSettings()
    {
        // Tampilkan MenuPanel dan sembunyikan SettingsPanel
        if (menuPanel != null)
        {
            menuPanel.SetActive(true);
        }

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }

    public void QuitGame()
    {
        StopAllAudio(); // Hentikan semua audio
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void StopAllAudio()
    {
        // Cari semua AudioSource di scene
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in allAudioSources)
        {
            audio.Stop(); // Hentikan semua AudioSource
        }
    }
}
