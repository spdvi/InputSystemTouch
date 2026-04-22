using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickRayToPlaneManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction mousePressedAction;
    public GameObject prefab;
    
    public Vector3 worldPosition;
    Plane plane = new Plane(Vector3.up, 0);

    private void OnMousePress(InputValue value)
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Debug.Log(mousePosition);
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            worldPosition = ray.GetPoint(distance);
            Debug.Log(worldPosition);
            Instantiate(prefab,  worldPosition, Quaternion.identity);
        }
    }
    
}
