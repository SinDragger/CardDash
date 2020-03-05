using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using wyc;

public class UI_EnemySlotBar_BG : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    public UI_EnemySlotBar slotBar;
    Vector2 startPosition;
    Vector2 offset;
    bool reach;
    bool isDraging;

    public void OnDrag(PointerEventData eventData)
    {
        offset = eventData.position - startPosition;
        startPosition = eventData.position;
        slotBar.BarScroll(offset.x);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = eventData.position;
        isDraging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDraging = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("TheReal");
        reach = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        reach = false;
    }
}
