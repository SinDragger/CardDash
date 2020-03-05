using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using wyc;
public class Basic_CardContainer : MonoBehaviour
{
    public GameObject panel;//卡牌区域

    public CardContainerType cardContainerType;

    public int equipPointer;//装备的指向
    public int weaponPointer;//武器的指向

    public int equipFlag;//当前偏移卡牌的数值
    public int weaponFlag;//当前偏移卡牌的数值

    public float paddingT;
    public float paddingB;
    public float paddingR;
    public float paddingL;
    //四边的空余

    public List<GameObject> equipsCards;//实际的改装卡牌们
    public List<GameObject> weaponsCards;//实际的武器卡牌们
    public List<int> equips;//武器列表-id
    public List<int> weapons;//改装列表-id

    public float cardHeight;
    public float cardWidth;

    public virtual bool NextPointer(CardContainerType cardContainerType)
    {
        //被Input或操作调用，自己变化后，修改UI内容
        return false;
    }

    public virtual bool FrontPointer(CardContainerType cardContainerType)
    {
        return false;
    }


    public virtual bool SetPointer(CardContainerType cardContainerType,int flag)
    {
        return false;
    }

    public virtual void RenewSlotData()
    {
        //激活状态、不可使用状态、
    }
}
