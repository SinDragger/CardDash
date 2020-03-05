using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using wyc;
public class Game_CE_Manager : MonoBehaviour
{
    public List<WeaponSource> weaponSources;
    public List<EquipSource> equipSources;

    public Text moneyText;

    int money;//可用资金

    int weaponMaxBuyAble;//最大购买数量
    int equipMaxBuyAble;//最大购买数量

    List<WeaponSource> buyedWeapons;
    List<EquipSource> buyedEquips;

    //接受后方消息

    public UI_CardSlot_2 mainSlot;
    public UI_BuyedCardArea cardArea;
    public static Game_CE_Manager instance;
    // Start is called before the first frame update
    void Start()
    {
        //weaponSources = new List<WeaponSource>();
        weaponSources = GameController.instance.BuyAbleRandomWeapons(11);
        //equipSources = new List<EquipSource>();
        equipSources = GameController.instance.BuyAbleRandomEquips(6);

        buyedWeapons = new List<WeaponSource>();
        buyedEquips = new List<EquipSource>();
        instance = this;
        money = wyc.GameController.instance.playerThug.wealth;
        RenewMoney();
        if (mainSlot)
        {
            mainSlot.RenewData(wyc.GameController.instance.playerThug);
        }
        //载入所有卡牌数据 卡牌可呈现复数叠加 
        if (cardArea)
        {
            cardArea.Init(weaponSources);
            cardArea.Init(equipSources);
        }
        //更新表示
    }

    void RenewMoney()
    {
        if (moneyText)
        {
            moneyText.text = "$" + money + ",000";
        }
    }

    public bool BuyItem(wyc.CardContainerType type,int id)
    {
        if (type == wyc.CardContainerType.Weapon)
        {
            foreach (var i in weaponSources)
            {
                if (i.orderNumber == id)
                {
                    //Debug.Log("Find");
                    if (money >= i.price)
                    {
                        money -= i.price;
                        RenewMoney();
                        buyedWeapons.Add(i);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        else
        {
            foreach (var i in equipSources)
            {
                if (i.orderNumber == id)
                {
                    if (money >= i.price)
                    {
                        money -= i.price;
                        RenewMoney();
                        buyedEquips.Add(i);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        return false;
    }

    void LoadBuyedItemsToPlayer()
    {
        List<int> temp = new List<int>();
        foreach(var i in buyedWeapons)
        {
            temp.Add(i.orderNumber);
        }
        Debug.Log(temp.Count);
        wyc.GameController.instance.playerThug.weapons =temp;
        temp = new List<int>();
        foreach (var i in buyedEquips)
        {
            temp.Add(i.orderNumber);
        }
        //Debug.Log(temp.Count);
        wyc.GameController.instance.playerThug.equips = temp;
    }

    public void NextScence()
    {
        LoadBuyedItemsToPlayer();
        wyc.GameController.instance.GameScenceChange(3);
    }
}
