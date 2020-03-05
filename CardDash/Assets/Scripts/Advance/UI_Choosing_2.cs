using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Choosing_2 : MonoBehaviour
{
    public UI_CardArea_2 cardArea;
    public int position;
    public float targetX;
    // Start is called before the first frame update
    void Start()
    {
        targetX = transform.position.x;
    }

    public void ChoosePosition(int flag)
    {
        if (!cardArea) return;
        float x=cardArea.GetXbyPosition(flag);
        if (x == 0f) return;
        position = flag;
        targetX = x;
        //并触发一次人物更新
    }
    public void ChangePosition(int i)
    {
        Game_CC_Manager.instance.thugChooseReal(position + i);

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangePosition(1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangePosition(-1);
            //ChoosePosition(position - 1);
        }
        RenewPosition();
    }
    void RenewPosition()
    {

        transform.position =Vector3.Lerp(new Vector3(targetX, transform.position.y, transform.position.z),transform.position,0.8f);
    }
}
