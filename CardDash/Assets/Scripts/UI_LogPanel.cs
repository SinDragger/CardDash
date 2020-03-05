using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using wyc;
public class UI_LogPanel : MonoBehaviour, DataReceiver
{
    public RectTransform panel;
    public Text iconText;
    public RectTransform self;

    public Text panelText;
    //public Vector3 targetPosition;
    bool isflod;

    public int maxLog;
    // Start is called before the first frame update
    void Start()
    {
        UIController.instance.attachRenewList(this, DataChangeType.Log);
        isflod = true;
        self = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeFlod()
    {
        isflod = !isflod;
        if (isflod)
        {
            iconText.text = "<";
            self.localPosition += new Vector3(panel.rect.width, 0f, 0f);
        }
        else
        {
            iconText.text = ">";
            self.localPosition -= new Vector3(panel.rect.width, 0f, 0f);
        }
    }

    public void RenewData(List<string> temp)
    {
        Debug.Log("False Using");
    }

    public void RenewData()
    {
        if (panelText == null) return;
        panelText.text = "";
        List<string> temp=UIController.instance.logPool;
        //if (temp == null) return;
        int i= temp.Count;
        int max = temp.Count;
        if (i >= maxLog)
        {
            i -= maxLog;
        }
        else
        {
            i = 0;
        }

        for(; i < temp.Count; i++)
        {
            panelText.text += temp[i] + '\n';
        }

        //Debug.Log("数据更新");
        //foreach(string s in temp)
        //{
        //    panelText.text += s + '\n';
        //}
    }
}
