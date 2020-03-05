using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LoadingBar : MonoBehaviour
{
    float now;//当前载入进度
    public float minSet=0.42f;
    public float maxSet=0f;
    public float timeBarSpeed;
    public Text text;
    public Image left;
    public Image right;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Init(float t)
    {
        now = 0;
        timeBarSpeed = t;
    }
    void RenewBar()
    {
        if (now >= 1f) { Game_Main_Loading.instance.CloseSelf(); return; }
        now += Time.deltaTime*0.1f*timeBarSpeed ;
        if (now > 1f) { now = 1f; }
        Debug.Log(now);
        float last = (maxSet - minSet) * now + minSet;
        left.fillAmount = last;
        right.fillAmount = last;
        if (text)
        {
            text.text = (int)(now*100f) + "%";
        }
    }
    void RenewBar(float newNow)
    {
        now = newNow; 
        float last = (maxSet - minSet) * now + minSet;
        left.fillAmount = last;
        right.fillAmount = last;
        if (text)
        {
            text.text = (int)(now * 100f) + "%";
        }
    }
    // Update is called once per frame
    void Update()
    {
        RenewBar();
    }
}
