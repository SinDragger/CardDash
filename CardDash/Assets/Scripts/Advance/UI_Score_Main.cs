using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using wyc;
public class UI_Score_Main : MonoBehaviour, DataReceiver
{
    public UI_Score score;
    public GameObject bg;

    public void RenewData()
    {
        Debug.Log("游戏界面登场");
        bg.SetActive(true);
        score.gameObject.SetActive(true);
        score.Init(RaceController.instance.GetRuntimePlayerRank(false), RaceController.instance.realPlayer);
    }

    public void RenewData(List<string> temp)
    {
        //throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        UIController.instance.attachRenewList(this, DataChangeType.GameComplete);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
