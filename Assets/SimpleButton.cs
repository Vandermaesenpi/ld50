using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class SimpleButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public SpriteRenderer rend;
    public Sprite idle, hover, down;
    public UnityEvent onClick;
    bool hovered;

    private void Update() {
        if(Input.GetButton("Submit")){
            onClick.Invoke();
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        rend.sprite = down;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovered = true;
        rend.sprite = hover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
        rend.sprite = idle;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rend.sprite = idle;
        onClick?.Invoke();
    }

}
