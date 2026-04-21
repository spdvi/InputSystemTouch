using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI text;
    
    private PlayerInput playerInput;

    private InputAction touchPositionAction;
    private InputAction touchPressAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchPressAction = playerInput.actions.FindAction("TouchPress");
        touchPositionAction = playerInput.actions.FindAction("TouchPosition");
    }

    private void OnEnable()
    {
        //touchPressAction.performed += TouchPressed;
    }

    private void OnDisable()
    {
        //touchPressAction.performed -= TouchPressed;
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            Debug.Log("TouchScreen Pressed");
        }

        Vector2 positionInScreen = touchPositionAction.ReadValue<Vector2>();
        Vector3 position = Camera.main.ScreenToWorldPoint(positionInScreen);
        position.z = player.transform.position.z;
        text.text = "Screen Position: " + Mathf.Round(positionInScreen.x) + "," + Mathf.Round(positionInScreen.y) +
                    "World Position: " + position.ToString();
        player.transform.position = position;
    }

    private void Update()
    {
        if (touchPressAction.IsPressed())
        {
            Vector2 positionInScreen = touchPositionAction.ReadValue<Vector2>();
            Vector3 position = Camera.main.ScreenToWorldPoint(positionInScreen);
            position.z = player.transform.position.z;
            text.text = "Screen Position: " + Mathf.Round(positionInScreen.x) + "," + Mathf.Round(positionInScreen.y) +
                        "World Position: " + position.ToString();
            player.transform.position = position;
        }
    }
}


