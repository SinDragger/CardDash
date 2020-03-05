using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_CC_Manager : MonoBehaviour
{
    public float countDown;//计时
    public bool isStop;//是否暂停

    public wyc.ThugSource choosed;

    public UI_CardArea_2 cardArea;
    public UI_Choosing_2 cardChoosing;
    public UI_CardSlot_2 mainSlot;

    public List<wyc.ThugSource> chooseAbleIds;

    // Start is called before the first frame update
    public static Game_CC_Manager instance;
    void Start()//初始化时去获取最新数据
    {
        instance = this;
        chooseAbleIds =wyc.GameController.instance.ChooseAblePlayers(8);
        choosed = chooseAbleIds[0];
        //更新卡牌域显示
        if (cardArea)
        {
            cardArea.Init(chooseAbleIds);
        }
        if (mainSlot)
        {
            mainSlot.RenewData(chooseAbleIds[0]);
        }
    }
    public void thugChooseReal(int i)
    {
        if (i < 0 || i >= chooseAbleIds.Count) return;
        //进行处理
        cardChoosing.ChoosePosition(i);
        //Debug.Log("Choosed" + i);
        choosed = chooseAbleIds[i];
        if (mainSlot)
        {
            mainSlot.RenewData(chooseAbleIds[i]);
        }
    }
    public void thugChoose(int flag)
    {
        int i = 0;
        for (; i < chooseAbleIds.Count; i++)
        {
            if (chooseAbleIds[i].orderNumber == flag)
            {
                thugChooseReal(i);
                //然后更新选中者的数据
                return;
            }
        }
    }

    public void NextScence()
    {
        wyc.GameController.instance.playerThugId=choosed.orderNumber;
        wyc.GameController.instance.playerThug = choosed;
        wyc.GameController.instance.PrepareNPCRace(4);
        wyc.GameController.instance.GameScenceChange(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
