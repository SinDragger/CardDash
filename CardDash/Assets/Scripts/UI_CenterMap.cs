using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using wyc;

public class UI_CenterMap : MonoBehaviour, DataReceiver
{
    public PlayerCard realPlayer;
    public LandCard land_first;
    public LandCard land_second;
    public Text enemyCountFront;
    public Text enemyCountBack;


    public void RenewData(List<string> temp)
    {
    }

    public void RenewData()
    {
        //根据回合判断更新的数据项？
        if (Init())
        {
            List<int> temp = UIController.instance.GetCenterMapData(realPlayer.runtimePlayerID);
            int landCount=temp[0];
            RectTransform rect;
            switch (landCount)
            {
                case 1://1张卡牌导向
                    land_first.gameObject.SetActive(true);
                    land_second.gameObject.SetActive(false);
                    rect = land_first.GetComponent<RectTransform>();
                    rect.localPosition = new Vector3(rect.localPosition.x, 0f, rect.localPosition.z);
                    land_first.landID = temp[1];
                    land_first.UpdateData();
                    break;
                case 0:
                    land_first.gameObject.SetActive(false);
                    land_second.gameObject.SetActive(false);

                    break;
                default://2张以上卡牌导向
                    land_first.gameObject.SetActive(true);
                    land_second.gameObject.SetActive(true);


                    rect = land_first.GetComponent<RectTransform>();
                    rect.localPosition = new Vector3(rect.localPosition.x, -GetComponent<RectTransform>().rect.height/2, rect.localPosition.z);
                    rect = land_second.GetComponent<RectTransform>();
                    rect.localPosition = new Vector3(rect.localPosition.x, GetComponent<RectTransform>().rect.height/2, rect.localPosition.z);

                    land_first.landID = temp[1];
                    land_first.UpdateData();
                    land_second.landID = temp[2];
                    land_second.UpdateData();
                    break;
            }


            List<int> temp2 = UIController.instance.GetRealPlayerLandFBStatus();
            if (enemyCountFront != null)
            {
                enemyCountFront.text = temp2[1].ToString();
            }
            if (enemyCountBack != null)
            {
                enemyCountBack.text = temp2[0].ToString();
            }

        }
    }

    // 2个未来分支导向的map,双击地形卡出全景
    //每阶段更新（是否感知自己是否被攻击选中？可以有——感知玩家被攻击？可以有）
    // Start is called before the first frame update
    void Start()
    {
        //Init();
        UIController.instance.attachRenewList(this, DataChangeType.State);
    }

    bool Init()
    {
        if (realPlayer != null && realPlayer.runtimePlayerID == -1)
        {
            int temp = UIController.instance.GetRealPlayerID();
            if (temp != -1)
            {
                realPlayer.runtimePlayerID = temp;
                realPlayer.UpdateData();
                return true;
            }
            return false;
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        //Init();
    }
}
