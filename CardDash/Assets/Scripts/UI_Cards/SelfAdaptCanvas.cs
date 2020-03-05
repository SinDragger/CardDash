using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfAdaptCanvas : MonoBehaviour
{

    public float screenX;//屏幕分辨率X
    public float screenY;//屏幕分辨率Y
    public float canvasX;
    public float canvasY;
    public float fromDistanceY;//所用那端侧边栏的原长度
    public float endDistanceY;//到末尾的的原长度
    public RectTransform shell;//外壳
    public RectTransform back;//内部背景
    public RectTransform canvas;//全画布

    // Start is called before the first frame update
    void Start()
    {
        canvas = this.transform.parent.GetComponent<RectTransform>();
        shell = this.transform.GetComponent<RectTransform>();
        back = this.transform.GetChild(0).GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetScreen())
        {
            FitScreen();
        }
        
    }

    void FitScreen()
    {
                back.sizeDelta = new Vector2(canvasX - (fromDistanceY + endDistanceY) * canvasY, back.sizeDelta.y);
                shell.position = new Vector3(back.sizeDelta.x / 2/canvasY*screenY + (endDistanceY) * screenY, shell.position.y, shell.position.z);
    }

    bool GetScreen()
    {
        if(canvasX!= canvas.sizeDelta.x|| canvasY!= canvas.sizeDelta.y)
        {
            canvasX = canvas.sizeDelta.x;
            canvasY = canvas.sizeDelta.y;
            screenX = Screen.width;
            screenY = Screen.height;
            return true;
        }
        return true;
    }

}

public enum ADAPT_MODE{
    LEFT,
    RIGHT,
    TOP,
    BOTTOM
    
}