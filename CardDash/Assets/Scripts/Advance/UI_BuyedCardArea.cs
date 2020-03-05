using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BuyedCardArea : MonoBehaviour
{
    //public List<RectTransform> targ;//所有下属卡牌
    public Text weaponText;
    public Text equipText;
    

    public GameObject equipArea;
    public GameObject weaponArea;


    public GameObject equipPrefab;
    public GameObject weaponPrefab;

    List<UI_BuyAble> weapons;
    List<UI_BuyAble> equips;

    public float cardSpacing;//卡牌间行间隙

    public float wFix;//偏移量
    public float eFix;
    //用于显示
    public bool first;

    public int precards;//每行的卡牌数量
    public float selfLength;
    public float selfHeight;
    public float targetLength;
    public float targetHeight;
    public float jianxi;
    // Start is called before the first frame update
    void Start()
    {
        weapons = new List<UI_BuyAble>();
        equips = new List<UI_BuyAble>();
        selfLength = GetComponent<RectTransform>().rect.width;
        selfHeight = GetComponent<RectTransform>().rect.height;
        targetLength = equipPrefab.GetComponent<RectTransform>().rect.width;
        targetHeight = equipPrefab.GetComponent<RectTransform>().rect.height;
        precards = (int)(selfLength / targetLength);
        jianxi = (selfLength - targetLength * precards) / (precards - 1);
        weapons.Add(weaponPrefab.GetComponent<UI_BuyAble>());
        equips.Add(equipPrefab.GetComponent<UI_BuyAble>());
    }
    public void switchArea(bool flag)//切换显示
    {
        first = flag;
        if (!first)
        {
            weaponText.color = Color.white;
            equipText.color = Color.yellow;
            equipArea.SetActive(true);
            weaponArea.SetActive(false);
        }
        else
        {
            weaponText.color = Color.yellow;
            equipText.color = Color.white;
            equipArea.SetActive(false);
            weaponArea.SetActive(true);
        }
    }

    public void RenewData()
    {
        //float hfix=;//绝对高度
    }

    public void Init(List<wyc.WeaponSource> data)
    {
        GameObject temp;
        //增殖与减少
        while(data.Count > weapons.Count)
        {
            temp = Instantiate(weaponPrefab, weaponArea.transform);
            weapons.Add(temp.GetComponent<UI_BuyAble>());
        }
        //记录原有Position，然后移动
        selfHeight = weaponArea.GetComponent<RectTransform>().rect.height;
        float moveY= (weapons.Count / precards+1) * targetHeight+ (weapons.Count / precards) * cardSpacing - selfHeight;
        var t = weaponArea.GetComponent<RectTransform>();
        t.localPosition=t.localPosition+new Vector3(0,-moveY/2,0);
        t.sizeDelta = t.sizeDelta + new Vector2(0, moveY);
        selfHeight += moveY;
        int line = -1;
        for (int i = 0; i < weapons.Count; i++)
        {
            //寻找之前的序列，有重复则将其加1，否则新增*****

            if (i % precards == 0) line++;
            weapons[i].transform.localPosition = new Vector3(-selfLength / 2+targetLength /2 + (targetLength + jianxi) * (i % precards), selfHeight / 2 - targetHeight / 2 - (targetHeight + cardSpacing) * line, 0f);
            weapons[i].card.RenewData(data[i]);
            weapons[i].num++;
        }

    }

    public void Init(List<wyc.EquipSource> data)
    {
        GameObject temp;
        //增殖与减少
        while (data.Count > equips.Count)
        {
            temp = Instantiate(equipPrefab, equipArea.transform);
            equips.Add(temp.GetComponent<UI_BuyAble>());
        }
        //记录原有Position，然后移动
        selfHeight = equipArea.GetComponent<RectTransform>().rect.height;
        float moveY = (equips.Count / precards + 1) * targetHeight + (equips.Count / precards) * cardSpacing - selfHeight;
        var t = equipArea.GetComponent<RectTransform>();
        t.localPosition = t.localPosition + new Vector3(0, -moveY / 2, 0);
        t.sizeDelta = t.sizeDelta + new Vector2(0, moveY);
        selfHeight += moveY;
        int line = -1;
        for (int i = 0; i < equips.Count; i++)
        {
            if (i % precards == 0) line++;
            equips[i].transform.localPosition = new Vector3(-selfLength / 2 + targetLength / 2 + (targetLength + jianxi) * (i % precards), selfHeight / 2 - targetHeight / 2 - (targetHeight + cardSpacing) * line, 0f);
            equips[i].card.RenewData(data[i]);
            equips[i].num++;
        }
    }
}