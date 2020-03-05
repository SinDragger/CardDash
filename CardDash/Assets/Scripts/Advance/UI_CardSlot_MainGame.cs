using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CardSlot_MainGame : MonoBehaviour,IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public UI_CardSlot_2 realCard;
    public GameObject black;//灰幕
    public GameObject HighLight;//高亮
    public wyc.CardContainerType type;
    public bool isChoosing;//选中·黄边弹出
    public bool isAvaliable;//可用 明亮状态
    public Vector3 initPosition;//原始位置
    public Vector3 popPosition;//弹出位置
    Vector3 targetPosition;

    bool isIn;
    float timeCount;
    public static float maxCountDown = 0.5f;//倒计时1秒
    public UI_CardArea_MainGame manager;
    // Start is called before the first frame update
    void Start()
    {
        isIn = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isIn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isIn = false;
        timeCount = 0f;
        manager.CloseCardPromt();
    }

    public void InitPosition(Vector3 _init,Vector3 _pop)
    {
        initPosition = _init;
        popPosition = _pop;
        targetPosition= transform.localPosition = _init;
    }
    public void RenewData(wyc.Equip e)
    {
        realCard.RenewData(e);
        //用实体的卡牌更新
    }
    public void RenewData(wyc.Weapon w)
    {
        realCard.RenewData(w);
        //用实体的卡牌更新
    }

    public void SetChoose(bool flag)
    {
        if (HighLight)
        {
            HighLight.SetActive(flag);
        }
        isChoosing = flag;
        if (isChoosing)
        {
            targetPosition = popPosition;
            //通知UI
            if (realCard.GetCardType() == wyc.CardContainerType.Weapon)
            {
                Debug.Log("CheckWeapon");
                UIController.instance.ChangeUsingEquipment(realCard.Id, realCard.GetCardType());
            }
        }
        else
        {
            targetPosition = initPosition;
        }
    }
    public void SetAvaliable(bool flag)
    {
        if (HighLight)
        {
            black.SetActive(!flag);
        }
        isAvaliable = flag;
    }

    // Update is called once per frame
    void Update()
    {
        PositionFix();
        CheckIn();
    }
    void CheckIn()
    {
        if (isIn)
        {
            timeCount += Time.deltaTime;
            if (timeCount > maxCountDown)
            {
                manager.ShowCardPromt(realCard);
                timeCount = -999f;
            }
        }
    }

    void PositionFix()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, 0.3f); 
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isAvaliable) return;
        //在申请成功的情况下
        manager.DeactiveOtherCards(type, this);
        SetChoose(!isChoosing);
    }
}
