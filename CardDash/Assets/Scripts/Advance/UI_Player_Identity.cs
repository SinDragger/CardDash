using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Player_Identity : MonoBehaviour
{
    bool Open;//开启
    List<RectTransform> childs;
    List<Image> childsImage;
    public Sprite defaultSprite;
    float height;
    public float speed;
    public float offset;
    // Start is called before the first frame update
    void Start()
    {
        childs = new List<RectTransform>();
        childsImage = new List<Image>();
        Open = false;
        for(int i=0;i< transform.childCount; i++){
            childs.Add(transform.GetChild(i).GetComponent<RectTransform>());
            childsImage.Add(transform.GetChild(i).GetComponent<Image>());
        }
        height = childs[0].rect.height;
    }

    // Update is called once per frame
    void Update()
    {
        renewPosition();
    }
    void renewPosition()
    {
        float targetY;
        if (Open)
        {
            targetY = 0f;
            for (int i = childs.Count-1; i >= 0; i--)
            {
                MoveToTargetPosition(targetY, childs[i]);
                targetY -= height+ offset;
            }
        }
        else
        {
            targetY = 0f;
            for (int i = 0; i < childs.Count; i++)
            {
                MoveToTargetPosition(targetY, childs[i]);
            }
        }
    }

    void MoveToTargetPosition(float targetY,RectTransform target)
    {
        target.localPosition = Vector3.Lerp(target.localPosition, new Vector3(0f, targetY, 0f), speed*Time.deltaTime);
    }

    public void BeClicked(GameObject child)
    {
        if (!Open)
        {
            if (child != transform.GetChild(transform.childCount - 1).gameObject) return;
        }
        Open = !Open;
        //下方的被点击
        if (Open)
        {
            //还原
            childsImage[childsImage.Count - 1].sprite = defaultSprite;
        }
        else
        {
            //改变最后一个
            childsImage[childsImage.Count - 1].sprite = child.GetComponent<Image>().sprite;
        }


    }
}
