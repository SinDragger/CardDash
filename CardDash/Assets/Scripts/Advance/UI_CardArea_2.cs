using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CardArea_2 : MonoBehaviour
{
    public List<RectTransform> targ;
    public float selfLength;
    public float targetLength;
    public float jianxi;
    // Start is called before the first frame update
    void Start()
    {
        //targets=GetComponentsInChildren<RectTransform>();
    }
    public float GetXbyPosition(int flag)
    {
        if(flag>=targ.Count||flag<0) return 0f;
        return targ[flag].transform.position.x;
    }

    public void Init(List<wyc.ThugSource> data)
    {
        selfLength = GetComponent<RectTransform>().rect.width;
        targ = new List<RectTransform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            targ.Add(transform.GetChild(i).GetComponent<RectTransform>());
        }
        if (targ.Count > 0)
        {
            targetLength = targ[0].rect.width;
        }
        jianxi = (selfLength - targetLength * targ.Count) / (targ.Count - 1);
        for (int i = 0; i < targ.Count; i++)
        {
            float y = -selfLength / 2 + targetLength / 2 + targetLength * i + jianxi * i;
            targ[i].localPosition = new Vector3(y, 0f, 0f);
            targ[i].GetComponent<UI_CardSlot_2>().RenewData(data[i]);
        }
    }

    void Update()
    {
        
    }
}
