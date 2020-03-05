using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using wyc;
public class UI_PlayerData : MonoBehaviour, DataReceiver
{
    public PlayerCard realPlayer;
    public Text playerAccelerateSpeedText;
    public Text playerSlowSpeedText;
    public Text playerWantedText;
    public Text playerChangeSpeedText;
    public Text playerSpeedText;
    public Text playerDistanceText;
    public Text LandDifficultText;

    public UI_PlayerData_SpeedPoint pointer;

    public int playerChangeSpeed;//用于辅助修改Text颜色
    public int playerSpeed;//用于辅助修改Text颜色
    public int playerDistance;//用于辅助修改Text颜色
    public int playerAccelerateSpeed;
    public int playerSlowSpeed;
    public int playerMiniGame;
    public int playerWanted;

    public int playerDrive;
    public int LandDifficult;
    public Color landDifficultBasicColor;
    public Color landDifficultGoodColor;
    public Color landDifficultBadColor;

    public Color landDifficultWorstColor;
    public Color landDifficultBestColor;
    //0 Best 1~2 Good 3标准 4~5bad 6

    public Color AccNormalColor;
    public Color AccChooseColor;
    public Color DecNormalColor;
    public Color DecChooseColor;

    public void RenewData(List<string> temp)
    {
        Debug.Log("False Using");
    }

    public void RenewData()
    {
        List<string> temp=UIController.instance.GetRealPlayerData();
        if (temp != null)
        {
            //更新内部数据表
            //temp.Add(p.accelerateSpeed.ToString());
            //temp.Add(p.slowSpeed.ToString());
            //temp.Add(p.playMINIgame.ToString());
            //temp.Add(p.changeSpeed.ToString());
            //temp.Add(p.wanted.ToString());
            //temp.Add(p.speed.ToString());
            playerAccelerateSpeed = int.Parse(temp[0]);
            playerSlowSpeed = int.Parse(temp[1]);
            playerMiniGame = int.Parse(temp[2]);
            playerChangeSpeed = int.Parse(temp[3]);
            playerWanted = int.Parse(temp[4]);
            playerSpeed = int.Parse(temp[5]);
            playerDrive = int.Parse(temp[6]);
            LandDifficult = int.Parse(temp[7]);
            


            if (playerAccelerateSpeedText != null) { playerAccelerateSpeedText.text = temp[0]; }
            if (playerSlowSpeedText != null) { playerSlowSpeedText.text = temp[1]; }
            //Debug.Log(playerChangeSpeed);
            ChangeSpeedColor(playerChangeSpeed);
            //if (playerMiniGame != null) { playerMiniGame.text = temp[2]; }
            //if (playerChangeSpeedText != null) { playerChangeSpeedText.text = temp[3]; }
            if (playerWantedText != null) { playerWantedText.text = temp[4]; }
            if (playerSpeedText != null) { playerSpeedText.text = temp[5]+ " km/h"; }
            if (LandDifficultText != null)
            {
                int landColorlevel = LandDifficult - playerDrive;
                Color landColor = landDifficultBasicColor;
                if (landColorlevel <= 1)
                {
                    landColor = landDifficultBestColor;
                }
                else if (landColorlevel > 6)
                {
                    landColor = landDifficultWorstColor;
                }
                else if (landColorlevel <= 3)
                {
                    landColor = Color.Lerp(landDifficultBasicColor, landDifficultGoodColor, (float)(3-landColorlevel) / 3f);
                }
                else
                {
                    landColor = Color.Lerp(landDifficultBasicColor, landDifficultGoodColor, (float)( landColorlevel-3) / 3f);
                }

                LandDifficultText.text = temp[7];
                LandDifficultText.color = landColor;
            }

            if (pointer != null)
            {
                pointer.SetNewRotation(playerSpeed);
            }

            if (realPlayer != null)
            {
                realPlayer.UpdateData();
            }
        }
        //请求玩家数据列表
    }

    public void ChangeSpeedColor(int speedMode)
    {
        switch (speedMode)
        {
            case -1:
                playerAccelerateSpeedText.color = AccNormalColor;
                playerSlowSpeedText.color = DecChooseColor;
                break;
            case 0:
                playerAccelerateSpeedText.color = AccNormalColor;
                playerSlowSpeedText.color = DecNormalColor;
                break;
            case 1:
                playerAccelerateSpeedText.color = AccChooseColor;
                playerSlowSpeedText.color = DecNormalColor;
                break;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        UIController.instance.attachRenewList(this, DataChangeType.RealPlayer);
        //理论上应该还会查找表格
    }

    // Update is called once per frame
    void Update()
    {
    }
}
