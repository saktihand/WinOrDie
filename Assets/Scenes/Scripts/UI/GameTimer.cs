using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float gameDuration = 300f;
    private float timer;
    [SerializeField] private Text timerText;

    private void Start()
    {
        timer = gameDuration;
        UpdateTimerUI();
    }

    private void Update()
    {
        if (GameManager.instance.IsGameOver() || PauseManager.instance.IsGamePaused())
            return;

        timer -= Time.deltaTime;
        UpdateTimerUI();

        if (timer <= 0f)
        {
            timer = 0f;
            GameManager.instance.PlayerDied("TimeOut");
        }
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);

        if (timerText != null)
        {
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }
}
