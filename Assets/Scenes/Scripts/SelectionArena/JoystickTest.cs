using UnityEngine;

public class JoystickTest : MonoBehaviour
{
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        Debug.Log("Vertical Axis Value: " + verticalInput);

        if (Input.GetButtonDown("Submit"))
        {
            Debug.Log("Submit button pressed!");
        }
    }
}
