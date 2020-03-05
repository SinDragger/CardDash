using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using wyc;
public class UI_EnemySlotBar : Basic_EnemySlotBar
{
    //List<UI_EnemySlot> slots;
    float startedPositionFix;
    public float borderfix;
    public float unFlodSpeed;
    public float movingSpeed;
    //UI_EnemySlot target;

    //public void RenewData(List<string> temp)
    //{
    //}
    //public void RenewData()
    //{
    //    //用于初始化
    //    Init();
    //}

    public override bool SelectSlot(UI_EnemySlot slot)
    {
        if (target==slot) return false;

        if (target == null)
        {
            target = slot;
            target.isSelect = true;
            target.UpdateStateColor();

        }
        else if (target != slot)
        {
            target.isSelect = false;
            target.UpdateStateColor();
            target = slot;
            target.isSelect = true;
            target.UpdateStateColor();
        }
        List<int> temp = new List<int>();
        temp.Add((int)UI_Code.SELECT_TARGET);
        temp.Add(target.playerId);
        UIController.instance.TargetSelect(target.playerId);
        return true;
    }

    public override void Init() {

        int temp=UIController.instance.GetRaceMaxPlayers();
        Debug.Log("InitStart");
        GameObject target;
        for(int i = 0; i < temp; i++)
        {

            target = CreateSlot(slotPrefab, i);
            slots.Add( target.GetComponent<UI_EnemySlot>());
            slots[i].playerId = i;
            slots[i].slotBar = this;
            //更新该slots数据
        }
        //获取所有player数据并进行实例化

        //排序

    }
    GameObject CreateSlot(GameObject g,int i)
    {
        return Instantiate(g, slotBar.transform);
    }
    public bool CheckTargetGoingOn(UI_EnemySlot checkingTarget)
    {
        if (target == checkingTarget)
        {
            target = null;
            checkingTarget.isSelect = false;
            List<int> temp=new List<int>();
            temp.Add((int)UI_Code.SELECT_TARGET);
            temp.Add(-1);
            UIController.instance.RecieveUICode(temp);
            checkingTarget.UpdateStateColor();
            return true;
        }
        return false;
    }
    //public bool Deselect(UI_EnemySlot target)
    //{
        
    //}
    public bool SetSlotToBack(UI_EnemySlot target)
    {
        bool findFlag=false;
        UI_EnemySlot temp;
        for (int i = 0; i < slots.Count-1; i++)
        {
            if (slots[i] == target)
            {
                slots[i].transform.SetAsFirstSibling();
                //取消选中，更新颜色
                CheckTargetGoingOn(slots[i]);


                findFlag = true;
            }
            if (findFlag)
            {
                //if (slots[i + 1].health <= 0)
                //{
                //    break;
                //}
                temp = slots[i+1];
                slots[i+1]= slots[i];
                slots[i] = temp;
            }
        }
        return findFlag;
    }
    public override void UpdateSlotsPosition()//瞬间改成切换移动
    {
        RectTransform target;
        float  positionCount= startedPositionFix;
        for (int i = 0; i < slots.Count; i++)
        {
            target = slots[i].GetComponent<RectTransform>();
            positionCount += target.rect.width / 2;
            slots[i].targetX = positionCount;
            target.localPosition = new Vector3(MoveSlot(positionCount, target.localPosition.x), target.localPosition.y, target.localPosition.z);
            positionCount += target.rect.width / 2;
            positionCount += borderfix;
            //更新该slots数据
        }
    }
    private float MoveSlot(float target,float begin)
    {
        float add = Mathf.Abs(target - begin);
        if (begin != target)
        {
            if (begin < target)
            {
                begin += add*movingSpeed * Time.deltaTime;
                if (begin > target)
                {
                    begin = target;
                }
            }
            else
            {
                begin -= add * movingSpeed * Time.deltaTime;
                if (begin < target)
                {
                    begin = target;
                }
            }
        }
        return begin;
    }
    // Start is called before the first frame update
    void Awake()
    {
    }

    void Start()
    {
        UIController.instance.attachRenewList(this, DataChangeType.Init);
        //Debug.Log("AttachStart" + UIController.instance.InitList.Count);

        slots = new List<UI_EnemySlot>();
        startedPositionFix = -this.GetComponent<RectTransform>().rect.width / 2;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlotsPosition();
        //数据化更新所有的内容的位置
    }

    public void BarScroll(float change)
    {
        startedPositionFix += change;
    }

}
