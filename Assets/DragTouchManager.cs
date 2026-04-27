using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragTouchManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction touchPressAction;
    private InputAction touchPositionAction;
    private Transform selectedObject;
    
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchPressAction = playerInput.actions.FindAction("TouchPress");
        touchPositionAction = playerInput.actions.FindAction("TouchPosition");
    }

    private void OnEnable()
    {
        touchPressAction.started += OnFingerDown;
        touchPressAction.canceled += OnFingerUp;
        touchPositionAction.performed += OnFingerMove;
    }

    private void OnDisable()
    {
        touchPressAction.started -= OnFingerDown;
        touchPressAction.canceled -= OnFingerUp;
        touchPositionAction.performed -= OnFingerMove;
    }

    private void OnFingerDown(InputAction.CallbackContext context)
    {
        Vector2 positionInScreen = Touchscreen.current.primaryTouch.position.value;
        //Debug.Log("OnFingerDown: " + Touchscreen.current.primaryTouch.position.value);
        Ray ray = Camera.main.ScreenPointToRay(positionInScreen);
        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, 100f,
                LayerMask.GetMask("Targets")))
        {
            selectedObject = hit.transform;
            Color color = selectedObject.GetComponent<Renderer>().material.color;
            color.a = 0.5f;
            selectedObject.GetComponent<Renderer>().material.color = color;
        }
    }

    private void OnFingerUp(InputAction.CallbackContext context)
    {
        if (selectedObject == null) return;
        //Debug.Log("OnFingerUp");
        Color color = selectedObject.GetComponent<Renderer>().material.color;
        color.a = 1f;
        selectedObject.GetComponent<Renderer>().material.color = color;
        selectedObject = null;
    }

    private void OnFingerMove(InputAction.CallbackContext context)
    {
        if (selectedObject == null) return;

        Vector2 fingerPosition = context.ReadValue<Vector2>();
        
        Ray ray = Camera.main.ScreenPointToRay(fingerPosition);
        RaycastHit hitData;

        if (Physics.Raycast(ray, out hitData, 100, LayerMask.GetMask("Plane")))
        {
            Vector3 worldPosition = hitData.point;
            selectedObject.position = worldPosition;
        }
    }
}
