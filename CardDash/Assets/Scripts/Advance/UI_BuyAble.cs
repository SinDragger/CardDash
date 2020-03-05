using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_BuyAble : MonoBehaviour,IPointerClickHandler
{
    public UI_CardSlot_2 card;//自己
    public int num;//剩余数量
    public wyc.CardContainerType type;
    public GameObject emptyImage;
    public bool empty;
    // Start is called before the first frame update
    void Start()
    {
        if (!card)
        {
            card = GetComponent<UI_CardSlot_2>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckEmpty()
    {
        if (num <= 0)
        {
            emptyImage.SetActive(true);
        }
    }

    bool buyRequest()//购买申请
    {
        if (num < 1) return false;
        //Debug.Log("WantToBuy" + card.Id);
        if (Game_CE_Manager.instance.BuyItem(type, card.Id))
        {
            num--;
            CheckEmpty();
            return true;
        }
        return false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //向前端发送购买情况
        buyRequest();
    }
}
