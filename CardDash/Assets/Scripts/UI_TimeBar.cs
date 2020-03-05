using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace wyc
{
    public class UI_TimeBar : MonoBehaviour, DataReceiver
    {
        public Image target;
        public void RenewData()
        {
            if (target != null)
            {
                float maxTime = UIController.instance.GetRaceStateTime();
                float nowTime = UIController.instance.GetRaceNowTime();
                if (nowTime != 0f)
                {
                    target.fillAmount = 1f-nowTime/maxTime;
                }
                else
                {
                    target.fillAmount = 0f;
                }

            }
            //修改自己的Image长度
        }

        public void RenewData(List<string> temp)
        {
            Debug.Log("False Using");
        }

        void Start()
        {
            //self = GetComponent<Image>();
            UIController.instance.attachRenewList(this,DataChangeType.Time);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}