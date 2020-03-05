using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_ChoosAbleSlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    //public UI_CardArea_2 cardArea;
    bool active;
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("In");
        active = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        active = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }
    void CheckInput() {
        if (active)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("Choosed");
                Game_CC_Manager.instance.thugChoose(GetComponent<UI_CardSlot_2>().Id);
            }
        }
    }
}
