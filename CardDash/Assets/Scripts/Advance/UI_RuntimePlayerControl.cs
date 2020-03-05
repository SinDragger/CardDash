using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using wyc;
public class UI_RuntimePlayerControl : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,DataReceiver
{
    public UI_CardSlot_2 realCard;
    public bool isMain;//是否是主角
    public HighLightColorType type;
    public GameObject highLight;//高亮区
    public GameObject information;//详细信息
    public UI_Equipment_Information equipmentInformation;
    bool isIn;
    bool isLock;
    public void OnPointerEnter(PointerEventData eventData)
    {
        isIn = true;
        if (!isMain)
        {
            //if (information) { information.SetActive(true); }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isIn = false;
        if (!isMain&&!isLock)
        {
            //if (information) { information.SetActive(false); }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        CheckInput();
    }
    void CheckInput()
    {
        if (isMain||!isIn) return;
        if (Input.GetMouseButtonDown(1))
        {
            if (information) { information.SetActive(!isLock); }
            isLock = !isLock;
        }
        if (Input.GetMouseButtonDown(0))//左键
        {
            if (type == HighLightColorType.Choosable)
            {
                UIController.instance.TargetSelect(realCard.Id);
            }
        }
    }
    public void InitData(wyc.RuntimePlayer player)
    {
        realCard.RenewData(player);
        if (equipmentInformation)
        {
            equipmentInformation.Init(player);
        }
        UIController.instance.attachRenewList(this, DataChangeType.RealPlayer);
    }

    public void RenewData()
    {
        //var i = RaceController.instance.GetRuntimePlayerByID(realCard.Id);
        //if (i.player.health < 0)
        //{

        //}
        realCard.RenewData(RaceController.instance.GetRuntimePlayerByID(realCard.Id));
    }

    public void RenewData(List<string> temp)
    {
    }

    public void SetHighLightColor(HighLightColorType type) {
        //if(type)
    }

    public void SetHighLightColor(Color color) {
        //if(type)
        highLight.GetComponent<Image>().color = color;
    }


}
public enum HighLightColorType
{
    None,
    Choosable,
    Complete,
    Down,
    Target,

}
