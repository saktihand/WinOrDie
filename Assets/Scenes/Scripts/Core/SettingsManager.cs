using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Text volumeText; // Text untuk menampilkan nilai volume

    private void Start()
    {
        if (SoundManager.instance != null)
        {
            float savedVolume = SoundManager.instance.GetMusicVolume();
            volumeSlider.value = savedVolume * 100; // Inisialisasi slider
            UpdateVolumeText(savedVolume * 100); // Perbarui teks
        }

        // Tambahkan listener ke slider
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged); // Tambahkan Listener lewat script
    }

    public void OnVolumeChanged(float value) // Fungsi harus public dan memiliki parameter float
    {
        if (SoundManager.instance != null)
        {
            float normalizedVolume = value / 100f; // Ubah nilai slider (0-100) ke skala 0-1
            SoundManager.instance.SetMusicVolume(normalizedVolume);
            UpdateVolumeText(value); // Perbarui teks volume
        }
    }

    private void UpdateVolumeText(float value)
    {
        if (volumeText != null)
        {
            volumeText.text = $"Volume: {Mathf.RoundToInt(value)}%"; // Tampilkan angka volume
        }
    }
}
