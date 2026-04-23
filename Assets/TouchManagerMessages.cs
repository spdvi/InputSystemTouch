using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManagerMessages : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI text;
    
    private void OnTouchPress(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("TouchScreen Pressed");
        }

        Vector2 positionInScreen = Touchscreen.current.primaryTouch.position.value;
        Vector3 position = Camera.main.ScreenToWorldPoint(positionInScreen);
        position.z = player.transform.position.z;
        text.text = "Screen Position: " + Mathf.Round(positionInScreen.x) + "," + Mathf.Round(positionInScreen.y) +
                    "World Position: " + position.ToString();
        player.transform.position = position;
    }
}
