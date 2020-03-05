using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using wyc;
public class UI_Text : MonoBehaviour, DataReceiver
{
    public UI_DISPLAYABLE target;
    public string front;
    public string back;
    Text self;

    public void RenewData()
    {
        UpdateText();
    }

    public void RenewData(List<string> temp)
    {
        Debug.Log("False Using");
    }

    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<Text>();
        InitRenewDataReceive();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void InitRenewDataReceive()
    {
        switch (target)
        {
            case UI_DISPLAYABLE.PLAYER_ACC:
                UIController.instance.attachRenewList(this, DataChangeType.RealPlayer );
                break;
            case UI_DISPLAYABLE.PLAYER_DEC:
                UIController.instance.attachRenewList(this, DataChangeType.RealPlayer);
                break;
            case UI_DISPLAYABLE.PLAYER_DIF:
                UIController.instance.attachRenewList(this, DataChangeType.RealPlayer);
                break;
            case UI_DISPLAYABLE.RACE_STATE:
                UIController.instance.attachRenewList(this, DataChangeType.State);
                break;
            case UI_DISPLAYABLE.RACE_TURN:
                UIController.instance.attachRenewList(this, DataChangeType.Turn);
                break;
            case UI_DISPLAYABLE.RACE_PLAYER_COUNT:
                UIController.instance.attachRenewList(this, DataChangeType.Turn);
                break;
            case UI_DISPLAYABLE.RACE_CONFIRM_PLAYERS:
                UIController.instance.attachRenewList(this, DataChangeType.Turn);
                break;
            case UI_DISPLAYABLE.RACE_LEFT_TIME:
                UIController.instance.attachRenewList(this, DataChangeType.Time);
                break;
        }
    }

    void UpdateText()
    {
        string temp = "";
        switch (target)
        {
            case UI_DISPLAYABLE.PLAYER_ACC:
                //self.text = gameController.RealPlayer.accelerateSpeed.ToString();
                break;
            case UI_DISPLAYABLE.PLAYER_DEC:
                //self.text = gameController.RealPlayer.slowSpeed.ToString();
                break;

            case UI_DISPLAYABLE.PLAYER_DIF:
                //self.text = gameController.player..ToString();
                break;

            case UI_DISPLAYABLE.RACE_STATE:
                int state = UIController.instance.GetRaceState();
                switch (state)
                {
                    case (int)STATE.ACCOUNT_STATE:
                        temp = "结算阶段";
                        break;
                    case (int)STATE.ACCELERATE_STATE:
                        temp = "变速阶段";
                        break;
                    case (int)STATE.MOTION_STATE:
                        temp = "行动阶段";
                        break;
                    case (int)STATE.PREMEDITATE_STATE:
                        temp = "蓄谋阶段";
                        break;
                    case (int)STATE.COMBAT_STATE:
                        temp = "战斗阶段";
                        break;
                    case (int)STATE.END_STATE:
                        temp = "结束阶段";
                        break;
                }
                break;
            case UI_DISPLAYABLE.RACE_TURN:
                temp = UIController.instance.GetRaceTurn().ToString();
                break;
            case UI_DISPLAYABLE.RACE_PLAYER_COUNT:
                temp = UIController.instance.GetRaceMaxPlayers().ToString();
                break;
            case UI_DISPLAYABLE.RACE_CONFIRM_PLAYERS:
                temp = UIController.instance.GetRaceConfirmPlayers().ToString();
                break;
            case UI_DISPLAYABLE.RACE_LEFT_TIME:
                temp = string.Format("{0:f2}", UIController.instance.GetRaceLeftTime());
                break;
        }
        self.text = front + temp + back;
    }

    public enum UI_DISPLAYABLE//UI显示模式，适用于不同的选择情况
    {
        PLAYER_ACC,
        PLAYER_DEC,
        PLAYER_DIF,
        PLAYER_ALM,
        RACE_STATE,
        RACE_GAMESTATE,
        RACE_TURN,
        RACE_PLAYER_COUNT,
        RACE_CONFIRM_PLAYERS,
        RACE_LEFT_TIME
    }
}