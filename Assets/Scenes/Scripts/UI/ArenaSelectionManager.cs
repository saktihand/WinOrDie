using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaSelectionManager : MonoBehaviour
{
    public void LoadArena(string arenaName)
    {
        SceneManager.LoadScene(arenaName);
    }
}
