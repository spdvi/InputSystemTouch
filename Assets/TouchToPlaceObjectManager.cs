using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchToPlaceObjectManager : MonoBehaviour
{
    public GameObject objectToPlace;
    public TextMeshProUGUI text;
    private Vector3 worldPosition;
    
    private void OnTouchPress(InputValue value)
    {
        if (value.isPressed)
        {
            Vector2 positionInScreen = Touchscreen.current.primaryTouch.position.value;
            // Vector3 positionInScreen2 = new Vector3(positionInScreen.x, positionInScreen.y, 15);
            // Vector3 position = Camera.main.ScreenToWorldPoint(positionInScreen2);
            //position.z = player.transform.position.z;
            Ray ray = Camera.main.ScreenPointToRay(positionInScreen);
            RaycastHit hitData;

            if (Physics.Raycast(ray, out hitData, 1000))
            {
                worldPosition = hitData.point;
                Instantiate(objectToPlace, worldPosition, Quaternion.identity);
                text.text = "Screen Position: " + Mathf.Round(positionInScreen.x) + "," + Mathf.Round(positionInScreen.y) +
                            "  World Position: " + worldPosition.ToString();
            }
        }
    }
}
