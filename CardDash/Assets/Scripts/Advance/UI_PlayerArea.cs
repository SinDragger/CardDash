using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PlayerArea : MonoBehaviour
{
    public List<UI_RuntimePlayerControl> sectors;
    public UI_RuntimePlayerControl realPlayer;
    public Material deadEffect;

    public Color chooseAbleColor;
    public Color defaultColor;
    public Color targetColor;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void ClearAllHighLightColor()
    {
        foreach (var s in sectors)
        {
            s.highLight.SetActive(false);
        }

    }

    public void RenewChoosableEnemies(List<int> enemylist)
    {
        //先清空
        ClearAllHighLightColor();
        foreach (int i in enemylist)
        {
            foreach(var s in sectors)
            {
                if (s.realCard.Id == i)
                {
                    s.highLight.SetActive(true);
                    SetPlayerType(s, HighLightColorType.Choosable);
                    break;
                }
            }
        }
    }
    public void ClearChooseAble()
    {
        foreach (var s in sectors)
        {
            if (s.type == HighLightColorType.Choosable)
            {
                SetPlayerType(s, HighLightColorType.None);
            }
        }
    }
    public void ClearAll()
    {
        foreach (var s in sectors)
        {
            SetPlayerType(s, HighLightColorType.None);
        }
    }
    public void TargetChoosed(int id)
    {
        //目标改成红色,先还原颜色
        for(int i = 0; i < sectors.Count; i++)
        {
            if (sectors[i].realCard.Id == id)
            {
                SetPlayerType(sectors[i], HighLightColorType.Target);
            }
            else
            {
                if (sectors[i].type == HighLightColorType.Target)
                {
                    SetPlayerType(sectors[i], HighLightColorType.Choosable);
                }
            }
        }
    }
    public void SetPlayerType(UI_RuntimePlayerControl target,HighLightColorType type)
    {
        target.type = type;
        Color c=Color.clear;
        switch (type)
        {
            case HighLightColorType.Choosable:c = chooseAbleColor;break;
            case HighLightColorType.Target:c = targetColor;break;
            case HighLightColorType.None:c = defaultColor;break;
        }
        target.SetHighLightColor(c);
    }
    public void InitData(wyc.RuntimePlayer player,List<wyc.RuntimePlayer> enemies)
    {
        sectors = new List<UI_RuntimePlayerControl>(GetComponentsInChildren<UI_RuntimePlayerControl>());
        //foreach (var i in sectors)
        //{
        //    i.realCard.SetToSpecialEffect(true, deadEffect);
        //}
        realPlayer = sectors[0];
        if (player!=null)
        {
            //Debug.Log("啊");
        }
        realPlayer.InitData(player);
        for(int i = 1; i < sectors.Count; i++)
        {
            sectors[i].InitData(enemies[i - 1]);
        }
    }
    public void PlayerDead(int orderNum)
    {
        foreach(var i in sectors)
        {
            if (i.realCard.Id == orderNum)
            {
                i.realCard.SetToSpecialEffect(true, deadEffect);
                break;
            }
        }
    }
}
