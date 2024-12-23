using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();

        // Keep this object even when we go to a new scene
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            ApplySavedVolume(); // Terapkan volume yang disimpan
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }

    // Fungsi untuk mengatur volume musik dan menyimpannya
    public void SetMusicVolume(float volume)
    {
        if (source != null)
        {
            source.volume = Mathf.Clamp01(volume); // Pastikan nilai berada di rentang 0-1
            PlayerPrefs.SetFloat("MusicVolume", volume); // Simpan volume ke PlayerPrefs
            PlayerPrefs.Save(); // Pastikan pengaturan disimpan
        }
    }

    // Fungsi untuk mendapatkan volume musik saat ini dari PlayerPrefs
    public float GetMusicVolume()
    {
        return PlayerPrefs.HasKey("MusicVolume") ? PlayerPrefs.GetFloat("MusicVolume") : 1f; // Default 1 jika belum ada data
    }

    // Fungsi untuk menerapkan volume yang disimpan
    public void ApplySavedVolume()
    {
        if (source != null)
        {
            source.volume = GetMusicVolume(); // Terapkan volume yang disimpan
        }
    }

    // Fungsi untuk memainkan musik latar
    public void PlayBackgroundMusic(AudioClip musicClip)
    {
        if (source != null && source.clip != musicClip)
        {
            source.clip = musicClip;
            source.loop = true;
            source.Play();
        }
    }

    // Fungsi untuk menghentikan musik latar
    public void StopMusic()
    {
        if (source != null && source.isPlaying)
        {
            source.Stop();
        }
    }
}
