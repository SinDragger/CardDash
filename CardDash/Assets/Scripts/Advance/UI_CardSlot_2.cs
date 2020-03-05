using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using wyc;
public class UI_CardSlot_2 : MonoBehaviour
{
    public int Id;
    public Image cardPicture;
    public string cardType;//定位不同的结尾
    CardContainerType type;
    public Text tempText_1;
    public Text tempText_2;
    public Text tempText_3;
    public Text tempText_4;
    public Text tempText_5;
    public Text tempText_6;
    public Text tempText_7;
    public Text tempText_8;
    //public Sprite specialEffect;
    // Start is called before the first frame update
    public void RenewData(wyc.ThugSource target)//快速载入
    {
        type = CardContainerType.Thug;
        Id = target.orderNumber;
        if (tempText_1) { tempText_1.text = target.name; }
        if (tempText_2) { tempText_2.text = target.strength.ToString(); }
        if (tempText_3) { tempText_3.text = target.drive.ToString(); }
        if (tempText_4) { tempText_4.text = target.health.ToString(); }
        if (tempText_5) { tempText_5.text = "$"+target.wealth.ToString()+",000"; }
        if (tempText_6) {
            var c = DataController.instance.GetCharacteristicSource(target.characteristics[0]);
            tempText_6.text = c.GetName();
            //再加一个
            var charc = tempText_6.GetComponent<UI_CharacShow_ActiveAble>();
            if (charc)//把它初始化了
            {
                charc.Init(target.orderNumber, c.orderNumber, false);
            }
        }
        if (tempText_7) { tempText_7.text = DataController.instance.GetCharacteristicSource(target.characteristics[0]).Describe;}
        if (tempText_8) { tempText_8.text = CharacteristicsController.PrintCharacteristicDescirbe(DataController.instance.GetCharacteristicSource(target.characteristics[0])); }
        if (cardPicture)
        {
           Sprite temp = (Sprite)Resources.Load("Pictures/Thug_2/"+target.name+cardType, typeof(Sprite));
           cardPicture.sprite = temp;
        }
    }
    public void RenewData(RuntimePlayer target)//快速载入
    {
        type = CardContainerType.Thug;
        Id = target.runtimePlayerID;
        if (tempText_1) { tempText_1.text = target.player.name; }
        if (tempText_2) { tempText_2.text = target.player.strength.ToString(); }
        if (tempText_3) { tempText_3.text = target.player.drive.ToString(); }
        if (tempText_4) { tempText_4.text = target.player.health.ToString(); }
        if (tempText_5) { tempText_5.text = "$"+target.player.wealth.ToString()+",000"; }
        if (tempText_6) { tempText_6.text = target.player.characteristics[0].name;
            var charc = tempText_6.GetComponent<UI_CharacShow_ActiveAble>();
            if (charc)//把它初始化了
            {
                charc.Init(target.runtimePlayerID,target.player.characteristics[0].playerOrderNumber, true);
            }
        }
        if (tempText_7) {
            if (tempText_7.font.fontSize > 64)
            {
                tempText_7.text = target.speed.ToString();
            }
            else
            {
                tempText_7.text = target.speed.ToString() + "km/h";
            }
             }//显示速度
        //if (tempText_8) { tempText_8.text = CharacteristicsController.PrintCharacteristicDescirbe(DataController.instance.GetCharacteristicSource(target.characteristics[0])); }
        if (cardPicture)
        {
           Sprite temp = (Sprite)Resources.Load("Pictures/Thug_2/"+target.player.source.name+cardType, typeof(Sprite));
           cardPicture.sprite = temp;
        }
    }
    public CardContainerType GetCardType()
    {
        return type;
    }

    public void RenewData(wyc.WeaponSource target)//快速载入
    {
        type = CardContainerType.Weapon;
        Id = target.orderNumber;
        if (tempText_1) { tempText_1.text = target.name; }
        if (tempText_2)
        {
            if (target.price != 0)
            {
                tempText_2.text = "$" + target.price.ToString() + ",000";
            }
            else
            {
                tempText_2.text = "免费提供";

            }
        }
        //if (tempText_3) { tempText_3.text = target..ToString(); }//描述
        if (tempText_4) { tempText_4.text = target.GetCardType(); }
        if (tempText_5) { tempText_5.text = target.damage.ToString(); }
        if (tempText_6) { tempText_6.text = target.wanted.ToString(); }
        //if (tempText_7) { tempText_7.text = DataController.instance.GetCharacteristicSource(target.characteristics[0]).Describe; }
        //if (tempText_8) { tempText_8.text = CharacteristicsController.PrintCharacteristicDescirbe(DataController.instance.GetCharacteristicSource(target.characteristics[0])); }
        if (cardPicture)
        {
            Sprite temp = (Sprite)Resources.Load("Pictures/Weapon_2/" + target.name + cardType, typeof(Sprite));
            cardPicture.sprite = temp;
        }
    }
    public void RenewData(Weapon target)//快速载入
    {
        type = CardContainerType.Weapon;
        Id = target.playerOrderNumber;
        Debug.Log("武器ID" + Id);
        if (tempText_1) { tempText_1.text = target.name; }
        if (tempText_2)
        {
            if (target.price != 0)
            {
                tempText_2.text = "$" + target.price.ToString() + ",000";
            }
            else
            {
                tempText_2.text = "免费提供";

            }
        }
        //if (tempText_3) { tempText_3.text = target..ToString(); }//描述
        if (tempText_4) { tempText_4.text = target.GetCardType(); }
        if (tempText_5) { tempText_5.text = target.damage.ToString(); }
        if (tempText_6) { tempText_6.text = target.wanted.ToString(); }
        //if (tempText_7) { tempText_7.text = DataController.instance.GetCharacteristicSource(target.characteristics[0]).Describe; }
        //if (tempText_8) { tempText_8.text = CharacteristicsController.PrintCharacteristicDescirbe(DataController.instance.GetCharacteristicSource(target.characteristics[0])); }
        if (cardPicture)
        {
            Sprite temp = (Sprite)Resources.Load("Pictures/Weapon_2/" + target.name + cardType, typeof(Sprite));
            cardPicture.sprite = temp;
        }
    }
    public void RenewData(wyc.EquipSource target)//快速载入
    {
        type = CardContainerType.Equip;
        Id = target.orderNumber;
        if (tempText_1) { tempText_1.text = target.name; }
        if (tempText_2) {
            if (target.price !=0) {
                tempText_2.text = "$" + target.price.ToString() + ",000";
            }
            else
            {
                tempText_2.text = "免费提供";

            } }
        //if (tempText_3) { tempText_3.text = target..ToString(); }//描述
        if (tempText_4) { tempText_4.text = target.GetCardType(); }
        //if (tempText_5) { tempText_5.text = target.damage.ToString(); }
        //if (tempText_6) { tempText_6.text = target.wanted.ToString(); }
        //if (tempText_7) { tempText_7.text = DataController.instance.GetCharacteristicSource(target.characteristics[0]).Describe; }
        //if (tempText_8) { tempText_8.text = CharacteristicsController.PrintCharacteristicDescirbe(DataController.instance.GetCharacteristicSource(target.characteristics[0])); }
        if (cardPicture)
        {
            Sprite temp = (Sprite)Resources.Load("Pictures/Equip_2/" + target.name + cardType, typeof(Sprite));
            cardPicture.sprite = temp;
        }
    }
    public void RenewData(Equip target)//快速载入
    {
        type = CardContainerType.Equip;
        Id = target.playerOrderNumber;
        if (tempText_1) { tempText_1.text = target.name; }
        if (tempText_2) {
            if (target.price !=0) {
                tempText_2.text = "$" + target.price.ToString() + ",000";
            }
            else
            {
                tempText_2.text = "免费提供";

            } }
        //if (tempText_3) { tempText_3.text = target..ToString(); }//描述
        if (tempText_4) { tempText_4.text = target.GetCardType(); }
        //if (tempText_5) { tempText_5.text = target.damage.ToString(); }
        //if (tempText_6) { tempText_6.text = target.wanted.ToString(); }
        //if (tempText_7) { tempText_7.text = DataController.instance.GetCharacteristicSource(target.characteristics[0]).Describe; }
        //if (tempText_8) { tempText_8.text = CharacteristicsController.PrintCharacteristicDescirbe(DataController.instance.GetCharacteristicSource(target.characteristics[0])); }
        if (cardPicture)
        {
            Sprite temp = (Sprite)Resources.Load("Pictures/Equip_2/" + target.name + cardType, typeof(Sprite));
            cardPicture.sprite = temp;
        }
    }
    public void SetToSpecialEffect(bool flag,Material special)
    {
        if (cardPicture)
        {
            if (flag)
            {
                cardPicture.material = special;
            }
            else
            {
                cardPicture.material = null;
            }
        }
    }
}