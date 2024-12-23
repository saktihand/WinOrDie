using UnityEngine;
using UnityEngine.UI; // Tambahkan namespace untuk UI

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public GameObject gameOverPanel; // Panel untuk Game Over
    public Text gameOverMessageText; // Tambahan: Teks untuk menampilkan pesan pemenang

    private bool gameOver;

    private PlayerMovement[] playerMovements;
    private Player2Movement[] player2Movements;
    private PlayerAttack[] playerAttacks;
    private Player2Attack[] player2Attacks;
    private Player1MeleeAttack[] player1MeleeAttacks;
    private Player2MeleeAttack[] player2MeleeAttacks;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        playerMovements = FindObjectsOfType<PlayerMovement>();
        player2Movements = FindObjectsOfType<Player2Movement>();
        playerAttacks = FindObjectsOfType<PlayerAttack>();
        player2Attacks = FindObjectsOfType<Player2Attack>();
        player1MeleeAttacks = FindObjectsOfType<Player1MeleeAttack>();
        player2MeleeAttacks = FindObjectsOfType<Player2MeleeAttack>();
    }

    public void PlayerDied(string playerTag)
    {
        if (!gameOver)
        {
            gameOver = true;
            Debug.Log($"{playerTag} has died, Game Over!");

            string winnerMessage = DetermineWinner(playerTag);
            DisplayGameOverMessage(winnerMessage);

            DisablePlayerControls();

            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }

            Time.timeScale = 0f;
        }
    }

    private void DisablePlayerControls()
    {
        foreach (var playerMovement in playerMovements)
        {
            playerMovement.enabled = false;
        }

        foreach (var player2Movement in player2Movements)
        {
            player2Movement.enabled = false;
        }

        foreach (var playerAttack in playerAttacks)
        {
            playerAttack.enabled = false;
        }

        foreach (var player2Attack in player2Attacks)
        {
            player2Attack.enabled = false;
        }

        foreach (var meleeAttack in player1MeleeAttacks)
        {
            meleeAttack.enabled = false;
        }

        foreach (var meleeAttack in player2MeleeAttacks)
        {
            meleeAttack.enabled = false;
        }
    }

    private string DetermineWinner(string loserTag)
    {
        // Tentukan pemenang berdasarkan pemain yang kalah
        if (loserTag == "Player1")
        {
            return "Player 2 Menang!";
        }
        else if (loserTag == "Player2")
        {
            return "Player 1 Menang!";
        }
        else
        {
            return "Draw!";
        }
    }

    private void DisplayGameOverMessage(string message)
    {
        // Tampilkan pesan pemenang di Game Over Panel
        if (gameOverMessageText != null)
        {
            gameOverMessageText.text = message;
        }
    }

    public void ResetGameOver()
    {
        gameOver = false;

        foreach (var playerMovement in playerMovements)
        {
            playerMovement.enabled = true;
        }

        foreach (var player2Movement in player2Movements)
        {
            player2Movement.enabled = true;
        }

        foreach (var playerAttack in playerAttacks)
        {
            playerAttack.enabled = true;
        }

        foreach (var player2Attack in player2Attacks)
        {
            player2Attack.enabled = true;
        }

        foreach (var meleeAttack in player1MeleeAttacks)
        {
            meleeAttack.enabled = true;
        }

        foreach (var meleeAttack in player2MeleeAttacks)
        {
            meleeAttack.enabled = true;
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if (gameOverMessageText != null)
        {
            gameOverMessageText.text = ""; // Kosongkan pesan
        }
    }

    public bool IsGameOver()
    {
        return gameOver;
    }
}
