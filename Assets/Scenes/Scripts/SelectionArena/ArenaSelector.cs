using UnityEngine;
using UnityEngine.UI; // Tambahkan untuk UI
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems; // Tambahkan untuk EventSystem

public class ArenaSelector : MonoBehaviour
{
    public RectTransform skull; // Skull yang dipindahkan
    public RectTransform[] arenaPositions; // Posisi untuk setiap arena
    public Button[] buttons; // Array untuk tombol UI
    public string[] arenaSceneNames; // Nama scene untuk setiap arena

    private int currentSelection = 0; // Indeks posisi arena yang dipilih
    private bool inputLocked = false;

    void Start()
    {
        UpdateSkullPosition();
        SetButtonFocus();
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0.5f && !inputLocked) // Geser ke kanan
        {
            currentSelection = Mathf.Min(currentSelection + 1, arenaPositions.Length - 1);
            inputLocked = true;
            UpdateSkullPosition();
            SetButtonFocus();
        }
        else if (horizontalInput < -0.5f && !inputLocked) // Geser ke kiri
        {
            currentSelection = Mathf.Max(currentSelection - 1, 0);
            inputLocked = true;
            UpdateSkullPosition();
            SetButtonFocus();
        }

        if (Mathf.Abs(horizontalInput) < 0.1f)
            inputLocked = false;

        if (Input.GetButtonDown("Submit"))
        {
            Debug.Log("Selected Arena: " + arenaSceneNames[currentSelection]);
            SceneManager.LoadScene(arenaSceneNames[currentSelection]);
        }
    }

    void UpdateSkullPosition()
    {
        skull.position = arenaPositions[currentSelection].position;
    }

    void SetButtonFocus()
    {
        if (buttons[currentSelection] != null)
        {
            buttons[currentSelection].Select();
            Debug.Log("Button selected: " + buttons[currentSelection].name);
        }
    }
}
