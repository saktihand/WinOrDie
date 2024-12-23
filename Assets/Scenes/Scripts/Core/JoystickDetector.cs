using UnityEngine;

public class JoystickDetector : MonoBehaviour
{
    void Start()
    {
        string[] joysticks = Input.GetJoystickNames();
        for (int i = 0; i < joysticks.Length; i++)
        {
            Debug.Log($"Joystick {i + 1}: {joysticks[i]}");
        }
    }
}
