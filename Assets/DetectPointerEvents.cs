using UnityEngine;
using UnityEngine.EventSystems;

public class DetectPointerEvents : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Color color = GetComponent<Renderer>().material.color;
        color.a = 0.5f;
        GetComponent<Renderer>().material.color = color;
        Debug.Log(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Color color = GetComponent<Renderer>().material.color;
        color.a = 1f;
        GetComponent<Renderer>().material.color = color;
        Debug.Log(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 fingerPosition = eventData.position;
        
        Ray ray = Camera.main.ScreenPointToRay(fingerPosition);
        RaycastHit hitData;

        if (Physics.Raycast(ray, out hitData, 100, LayerMask.GetMask("Plane")))
        {
            Vector3 worldPosition = hitData.point;
            transform.position = worldPosition;
        }
    }
}
