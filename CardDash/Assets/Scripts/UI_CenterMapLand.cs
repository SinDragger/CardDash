using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CenterMapLand : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static Vector3 FIXED_TRANS =new Vector3(-80f,0f,0f);
    public static float ScaleFixed = 2.0f;
    public static float PositionFixed = 1000f;
    RectTransform self;
    public bool reach;
    public bool locked;
    // Start is called before the first frame update
    void Start()
    {
        reach = false;
        locked = false;
        self = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (reach&&Input.GetMouseButtonDown(1))
        {
            locked = !locked;
        }    
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!locked)
        {
            //Debug.Log(transform.parent.GetComponent<RectTransform>().rect.x);
            self.localPosition += FIXED_TRANS;
            self.localScale *= ScaleFixed;
        }
        reach = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!locked)
        {
            self.localPosition -= FIXED_TRANS;
            self.localScale /= ScaleFixed;
        }
        reach = false;
    }
}
