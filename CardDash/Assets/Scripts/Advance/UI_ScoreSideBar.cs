using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ScoreSideBar : MonoBehaviour
{
    public Text nickname;
    public Text kills;
    public Text bonus;
    public Image bg;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetToTitle()
    {
        if (nickname) { nickname.text = "昵称"; }
        if (kills) { kills.text = "杀人数"; }
        if (bonus) { bonus.text = "奖金"; }

    }
    public void HighLightSelf()
    {
        bg.color = Color.yellow;
    }
    public void SetDead(bool passout)
    {
        if (passout)
        {
            nickname.color = Color.gray;

        }
        else
        {
            nickname.color = Color.red;
        }
    }
    public void SetDetail(string name,int count,int money)
    {
        if (nickname) { nickname.text =name; }
        if (kills) { kills.text = count.ToString(); }
        if (bonus) { bonus.text = money.ToString() +",000"; }
    }
}
