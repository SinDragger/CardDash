using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using wyc;

public class UI_TimeText : MonoBehaviour, DataReceiver
{
    Text time;
    // Start is called before the first frame update
    void Start()
    {
        time = GetComponent<Text>();
        UIController.instance.attachRenewList(this,DataChangeType.Time);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void RenewData()
    {
        float maxTime = UIController.instance.GetRaceStateTime();
        float nowTime = UIController.instance.GetRaceNowTime();
        time.text = ((int)(maxTime - nowTime)).ToString();
        //修改自己的Image长度
    }

    public void RenewData(List<string> temp)
    {
    }
}
