using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_DragAble : MonoBehaviour,IDragHandler
{
    public RectTransform target;
    public RectTransform self;
    float parentHeight;
    //float parentDown;
    public void OnDrag(PointerEventData eventData)
    {
        PositionYChange(eventData.delta.y);
    }

    void PositionYChange(float deltaY)
    {
        float up = (target.rect.y - self.rect.y )- self.localPosition.y;
        float down = (target.rect.y - self.rect.y )+ self.localPosition.y;
        //Debug.Log(up + "/" + down+"/"+deltaY);
        if (deltaY > 0 && up - deltaY < 0)
        {
            self.localPosition = new Vector3(self.localPosition.x, (target.rect.y- self.rect.y), 0);
        }
        else if (deltaY < 0 && down + deltaY < 0)
        {
            self.localPosition = new Vector3(self.localPosition.x, -(target.rect.y - self.rect.y), 0);

        }
        else
        {
            self.localPosition += new Vector3(0, deltaY, 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //parentHeight = target.parent.GetComponent<RectTransform>().rect.y;//高
        if (!target) { GetComponent<RectTransform>(); }
        self=GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
       if( Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            PositionYChange(-Input.GetAxis("Mouse ScrollWheel")*70000f*Time.deltaTime);
        }
    }
}
