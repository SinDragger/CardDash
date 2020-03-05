using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Equipment_Information : MonoBehaviour
{
    public GameObject prefab;
    List<UI_EquipmentBlock_MINI> list;
    Image self;
    public float offset;//间隙长度
    float targetHeight;
    float selfHeight;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init(wyc.RuntimePlayer player)
    {
        self = GetComponent<Image>();
        targetHeight = prefab.GetComponent<RectTransform>().rect.height;
        selfHeight = GetComponent<RectTransform>().rect.height;
        list = new List<UI_EquipmentBlock_MINI>();
        GameObject temp;
        for (int i = 0; i < player.player.equips.Count; i++)
        {
            temp = Instantiate(prefab, transform);
            temp.SetActive(true);
            var e = temp.GetComponent<UI_EquipmentBlock_MINI>();
            e.Init(player.player.equips[i]);
            list.Add(e);
        }
        //Debug.Log(player.player.weapons.Count);
        for (int i = 0; i < player.player.weapons.Count; i++)
        {
            temp = Instantiate(prefab, transform);
            temp.SetActive(true);
            var e = temp.GetComponent<UI_EquipmentBlock_MINI>();
            e.Init(player.player.weapons[i]);
            list.Add(e);
        }
        positionRenew();
    }
    public void positionRenew()
    {
        float totalHeight;//设定总长度
        float nextPosition;
        for (int i = 0; i < list.Count; i++)
        {
            nextPosition = selfHeight / 2 - targetHeight / 2 - (targetHeight + offset) * i;
            
            list[i].GetComponent<RectTransform>().localPosition = new Vector3(list[i].GetComponent<RectTransform>().localPosition.x, nextPosition);
        }
        totalHeight = targetHeight + (targetHeight + offset) * (list.Count - 1);
        float y = transform.position.y + selfHeight / 2;//上方长度
        RectTransform r = GetComponent<RectTransform>();
        GetComponent<RectTransform>().sizeDelta = new Vector2(r.rect.width, totalHeight);
        //r.offsetMin= new Vector2(GetComponent<RectTransform>().offsetMin.x, totalHeight);
        //r.rect.Set(r.rect.x,y-totalHeight/2 ,r.rect.width,10);
    }
}
