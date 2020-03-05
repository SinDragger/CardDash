using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using wyc;
public class UI_CardArea_MainGame : MonoBehaviour,DataReceiver
{
    //public List<RectTransform> targ;//所有下属卡牌
    public Image speedUpButton;
    public Image speedDownButton;

    public GameObject equipArea;
    public GameObject weaponArea;
     

    public GameObject equipPrefab;
    public GameObject weaponPrefab;

    public UI_MainGame_CardPromt cardPromt;

    List<UI_CardSlot_MainGame> weapons;
    List<UI_CardSlot_MainGame> equips;



    public int weapon_line;//行数?页?
    public int equip_line;//行数?页?
    public int precardsMax;//每行的卡牌数量
    public int precardsNow;//当前的卡牌数量

    public float selfLength;
    public float selfHeight;
    public float targetLength;
    public float targetHeight;

    public float jianxi;
    public float popDistance;
    // Start is called before the first frame update
    void Start()
    {
        weapon_line = 0;
        equip_line = 0;
        weapons = new List<UI_CardSlot_MainGame>();
        equips = new List<UI_CardSlot_MainGame>();
        selfLength = equipArea.GetComponent<RectTransform>().rect.width;
        selfHeight = GetComponent<RectTransform>().rect.height;
        targetLength = equipPrefab.GetComponent<RectTransform>().rect.width;
        targetHeight = equipPrefab.GetComponent<RectTransform>().rect.height;
        UIController.instance.attachRenewList(this, DataChangeType.Init);
        UIController.instance.attachRenewList(this, DataChangeType.RealPlayer);
        //precardsMax = (int)(selfLength / targetLength);
        //jianxi = (selfLength - targetLength * precardsMax) / (precardsMax - 1);(每次自行演算)
        
    }

    public void InitData(int playerId)
    {
        InitCards(); 
        RenewEquips(UIController.instance.GetRuntimePlayerWeaponsIds(playerId), UIController.instance.GetRuntimePlayerEquipsIds(playerId));
        RenewPosition();
    }

    void InitCards() {

        weapons.Add(weaponPrefab.GetComponent<UI_CardSlot_MainGame>());
        GameObject temp;
        while (weapons.Count < precardsMax)
        {
            //Debug.Log(weapons.Count);
            temp = Instantiate(weaponPrefab, weaponArea.transform);
            weapons.Add(temp.GetComponent<UI_CardSlot_MainGame>());
            //增殖
        }

        equips.Add(equipPrefab.GetComponent<UI_CardSlot_MainGame>());
        while (equips.Count < precardsMax)
        {
            temp = Instantiate(equipPrefab, equipArea.transform);
            equips.Add(temp.GetComponent<UI_CardSlot_MainGame>());
            //增殖
        }
    }

    public void RenewEquips(List<int> w, List<int> e)
    {
        List<int> showList = new List<int>();
        int target = (weapon_line + 1) * precardsMax;
        target = target > w.Count ? w.Count : target;
        for (int i = weapon_line * precardsMax; i < target; i++)
        {
            showList.Add(w[i]);
        }
        for (int i = 0; i < precardsMax; i++)
        {
            if (i > showList.Count - 1)
            {
                weapons[i].gameObject.SetActive(false);
            }
            else
            {
                weapons[i].RenewData(RaceController.instance.GetRealPlayerWeapon(showList[i]));
            }
        }

        showList.Clear();
        target = (equip_line + 1) * precardsMax;
        target = target > e.Count ? e.Count : target;
        for (int i = equip_line * precardsMax; i < target; i++)
        {
            showList.Add(e[i]);
        }
        //Debug.Log(showList.Count - 1);
        for (int i = 0; i < precardsMax; i++)
        {
            if (i > showList.Count - 1)
            {
                equips[i].gameObject.SetActive(false);
            }
            else
            {
                equips[i].RenewData(RaceController.instance.GetRealPlayerEquip(showList[i]));
            }
        }

    }

    public void ShowCardPromt(UI_CardSlot_2 card)
    {
        Vector3 offsetPosition = new Vector3(0, 160, 0);
        if (card.GetCardType() == wyc.CardContainerType.Equip)
        {
            offsetPosition.x = 70;
        }
        else
        {
            offsetPosition.x = -70;

        }
        cardPromt.showCard(transform.position+ offsetPosition, card.GetCardType(), card.Id);
        //cardPromt.showCard(Vector3.zero, card.GetCardType(), card.Id);
    }
    public void CloseCardPromt()
    {
        cardPromt.CloseCard();
    }

    public void DeactiveOtherCards(wyc.CardContainerType type, UI_CardSlot_MainGame target)
    {
        if (type == wyc.CardContainerType.Equip)
        {
            foreach(var i in equips)
            {
                if (i != target&&i.isChoosing)
                {
                    i.SetChoose(false);
                }
            }
        }
        else
        {
            foreach (var i in weapons)
            {
                if (i != target && i.isChoosing)
                {
                    i.SetChoose(false);
                }
            }
        }
    }

    public void RenewPosition()//推导卡牌的状态并进行
    {
        Vector3 popOffset = new Vector3(0, popDistance, 0);
        Vector3 initPosition = new Vector3(0, 0, 0);
        float cardX;
        //武器从右到左
        //改装从左到右
        for (int i = 0; i < weapons.Count; i++)
        {
            //Debug.Log("Renew");
            cardX = -selfLength / 2 + targetLength / 2 + (targetLength + jianxi) * i;
            initPosition.x = -cardX;
            weapons[i].InitPosition(initPosition, initPosition + popOffset);
        }
        for (int i = 0; i < equips.Count; i++)
        {
            cardX = -selfLength / 2 + targetLength / 2 + (targetLength + jianxi) * i;
            initPosition.x = cardX;
            equips[i].InitPosition(initPosition, initPosition + popOffset);
        }
    }
    public void RenewData(List<string> temp)//通过序列直接初始化
    {

    }

    public void RenewData()//自己来初始化
    {
    }


    public void SetActiveToCards(bool flag, CardContainerType type)
    {
        if (type == CardContainerType.Weapon)
        {
            foreach(var i in weapons)
            {
                i.SetAvaliable(flag);
            }
        }
        else if (type == CardContainerType.Equip)
        {
            foreach (var i in equips)
            {
                i.SetAvaliable(flag);
            }
        }
        //Debug.Log("设定完成"+flag);
    }


    public void ChangeSpeed(int i)
    {
        UIController.instance.ChangePlayerSpeed(i);
        if (i > 0)
        {
            speedUpButton.color = Color.yellow;
            speedDownButton.color = Color.white;
        }
        else
        {
            speedUpButton.color = Color.white;
            speedDownButton.color = Color.yellow;
        }
    }

}
