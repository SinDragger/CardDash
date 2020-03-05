using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using wyc;
public class CardSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject targetCard;
    bool reach = false;
    public CardContainer cardContainer;
    bool dragging = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (reach&&Input.GetMouseButtonDown(0))
        {
            if (cardContainer != null)
            {
                //cardContainer.ClickBySlot(gameObject);
                cardContainer.NextPointer(cardContainer.cardContainerType);
            }
            DraggingOn();
            Debug.Log("点击了卡槽");
        }
        if (dragging)
        {
            bool ExChange = false ;
            if (!reach)
            {
                Debug.Log("拖动到外面");
                ExChange = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                //
                Debug.Log("鼠标抬起");
                DraggingOff();
            }
        }
        if (targetCard != null)
        {
            targetCard.GetComponent<RectTransform>().position = this.GetComponent<RectTransform>().position;
        }
    }

    private void DraggingOn()
    {
        dragging = true;
        //通知UIController，拖拽单卡开始；
    }

    private void DraggingOff()
    {
        dragging = false;
    }

    public void ResetPosition(float scale)
    {
        if (targetCard != null)
        {
            targetCard.GetComponent<Scale>().scale = scale;
            targetCard.GetComponent<RectTransform>().position = this.GetComponent<RectTransform>().position;
            //targetCard.GetComponent<RectTransform>().SetParent(transform.GetComponent<RectTransform>());
        }
    }
    public void SetVisible(bool flag)
    {
        if (flag)
        {
            if (targetCard != null)
            {
                targetCard.SetActive(true);
            }
        }
        else
        {
            if (targetCard != null)
            {
                targetCard.SetActive(false);
            }
        }
        this.gameObject.SetActive(flag);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
//        Debug.Log($"鼠标移入了一个Slot");
        reach = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        reach = false;
    }
}
