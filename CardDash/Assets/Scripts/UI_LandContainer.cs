using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wyc
{
    public class UI_LandContainer : MonoBehaviour,DataReceiver
    {
        public LandCard landCard;
        public RectTransform distanceBar;
        public RectTransform distanceBarFlag;
        public void RenewData()
        {
            //向UIController拿最新的Land数据，然后更新land卡
            landCard.landID= UIController.instance.GetRealPlayerLandID();
            if(distanceBar!=null&& distanceBarFlag != null)
            {
                int tempa = UIController.instance.GetRealPlayerLandLength();
                //Debug.Log(tempa);
                int tempb = UIController.instance.GetRealPlayerDistance();
                //Debug.Log(tempb);
                //Debug.Log(distanceBar.rect.width);
                //Debug.Log(distanceBar.rect.width * tempb / tempa);
                distanceBarFlag.localPosition = new Vector3(distanceBar.rect.width*tempb/tempa- distanceBar.rect.width / 2, 0f, 0f);
            }

            landCard.UpdateData();
        }

        public void RenewData(List<string> temp)
        {
            Debug.Log("False Using");
        }

        // Start is called before the first frame update
        void Start()
        {
            UIController.instance.attachRenewList(this,DataChangeType.Land);
        }

        // Update is called once per frame
        void Update()
        {

        }

        
    }
}